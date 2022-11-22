using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.repoADO
{
    public class VisitorRepoADO : IVisitorRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public VisitorRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool VisitorExists(Visitor visitor)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Visitor WHERE Lastname=@lastname AND Firstname=@firstname AND Email=@email AND Company=@company AND IsDeleted = 0";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@lastname", visitor.FirstName);
                    cmd.Parameters.AddWithValue("@firstname", visitor.LastName);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    cmd.Parameters.AddWithValue("@company", visitor.Company);

                    int count = (int)(long)cmd.ExecuteScalar();
                    if (count == 1) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("BestaatBezoeker", ex);
                }
                finally
                {
                    connection.Close();
                }
            }


        }

        public bool VisitorExists(int value)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Visitor WHERE VisitorId=@visitorId AND IsDeleted = 0";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", value);

                    int count = (int)(long)cmd.ExecuteScalar();
                    if (count == 1) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("BestaatBezoeker - met ID", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Visitor GetVisitor(int personId)
        {
            MySqlConnection connection = GetConnection();
            Visitor visitor = null;
            string query = "Select * FROM Visitor WHERE VisitorId = @visitorId AND IsDeleted = 0";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", personId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string LastName = (string)reader["LastName"];
                        string Firstnaam = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        visitor = new Visitor(personId, LastName, Firstnaam, Email, Company);
                    }
                    reader.Close();
                    return visitor;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("GeefBezoeker", ex);
                }
                finally
                {
                    connection.Close();
                }
            }

        }

        public List<Visitor> GetVisitors()
        {
            MySqlConnection connection = GetConnection();
            List<Visitor> visitors = new List<Visitor>();
            string query = "SELECT * FROM Visitor WHERE IsDeleted = 0";
            // TODO : select *
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitorId"];
                        string LastName = (string)reader["LastName"];
                        string FirstName = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        visitors.Add(new Visitor(Id, LastName, FirstName, Email, Company));
                    }
                    reader.Close();
                    return visitors;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("GeefBezoekers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Visitor> GetVisitors(string? lastname, string? firstname, string? email, string? company)
        {
            MySqlConnection connection = GetConnection();
            string query;
            List<Visitor> visitors = new List<Visitor>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    if (string.IsNullOrWhiteSpace(lastname) && string.IsNullOrWhiteSpace(firstname) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(company)) query = "SELECT * FROM Visitor";
                    else
                    {
                        query = "SELECT * FROM Visitor WHERE ";
                        if (!string.IsNullOrEmpty(lastname))
                        {
                            query += "LastName LIKE @lastname";
                            cmd.Parameters.AddWithValue("@lastname", "%" + lastname + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(firstname) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(firstname))
                        {
                            query += "FirstName LIKE @firstname";
                            cmd.Parameters.AddWithValue("@firstname", "%" + firstname + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            query += "Email LIKE @email";
                            cmd.Parameters.AddWithValue("@email", "%" + email + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(company) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(company))
                        {
                            query += "Company LIKE @company";
                            cmd.Parameters.AddWithValue("@company", "%" + company + "%");
                        }
                        query += " AND IsDeleted = 0";
                    }


                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["VisitorId"];
                        string LastName = (string)reader["LastName"];
                        string FirstName = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        visitors.Add(new Visitor(ID, LastName, FirstName, Email, Company));
                    }
                    reader.Close();
                    return visitors;
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("GeefBezoekers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateVisitor(int visitorid, string? lastname, string? firstname, string? email, string? company)
        {
            MySqlConnection connection = GetConnection();
            //string query = "UPDATE Visitor SET Name=@naam, Voornaam=@firstname, Email=@email, Company=@company WHERE VisitorId=@visitorId";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Visitor SET ";

                    if (!string.IsNullOrEmpty(lastname))
                    {
                        query += "LastName=@lastname";
                        cmd.Parameters.AddWithValue("@lastname", lastname);
                    }
                    if (!string.IsNullOrEmpty(firstname) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(firstname))
                    {
                        query += "FirstName=@firstname";
                        cmd.Parameters.AddWithValue("@firstname", firstname);
                    }
                    if (!string.IsNullOrEmpty(email) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(email))
                    {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrEmpty(company) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(company))
                    {
                        query += "Company=@company";
                        cmd.Parameters.AddWithValue(@"company", company);
                    }
                    query += " WHERE VisitorId=@visitorId";
                    cmd.Parameters.AddWithValue("@visitorId", visitorid);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("UpdateBezoekers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteVisitor(Visitor visitor)
        {
            MySqlConnection connection = GetConnection();
            string query = "UPDATE Visitor SET IsDeleted = 1 WHERE VisitorId =@visitorId OR Email=@email";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", visitor.PersonId);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);

                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("VerwijderBezoeker", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AddVisitor(Visitor visitor)
        {
            MySqlConnection connection = GetConnection();
            string query = "INSERT INTO Visitor (LastName, FirstName, Email, Company) VALUES (@lastname, @firstname,@email,@company); SELECT LAST_INSERT_ID()";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;


                    cmd.Parameters.AddWithValue("@lastname", visitor.LastName);
                    cmd.Parameters.AddWithValue("@firstname", visitor.FirstName);
                    cmd.Parameters.AddWithValue("@email", visitor.Email);
                    cmd.Parameters.AddWithValue("@company", visitor.Company);

                    int bezoekerId = (int)(long)(ulong)cmd.ExecuteScalar();
                    visitor.SetId(bezoekerId);
                }
                catch (Exception ex)
                {
                    throw new VisitorRepoADOException("VoegBezoekerToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }


        }

    }
}
