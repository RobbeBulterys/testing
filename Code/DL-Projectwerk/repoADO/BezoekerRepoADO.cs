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
    public class BezoekerRepoADO : IBezoekerRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public BezoekerRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool BestaatBezoeker(Bezoeker bezoeker)
        {
            MySqlConnection connectie = GetConnection();
            string query = "SELECT COUNT(*) FROM Visitor WHERE Lastname=@lastname AND Firstname=@firstname AND Email=@email AND Company=@company AND IsDeleted = 0";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@lastname", bezoeker.Naam);
                    cmd.Parameters.AddWithValue("@firstname", bezoeker.Voornaam);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);
                    cmd.Parameters.AddWithValue("@company", bezoeker.Bedrijf);

                    int count = (int)(long)cmd.ExecuteScalar();
                    if (count == 1) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("BestaatBezoeker", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }


        }

        public bool BestaatBezoeker(int value)
        {
            MySqlConnection connectie = GetConnection();
            string query = "SELECT COUNT(*) FROM Visitor WHERE VisitorId=@visitorId AND IsDeleted = 0";
            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", value);

                    int count = (int)(long)cmd.ExecuteScalar();
                    if (count == 1) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("BestaatBezoeker - met ID", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public Bezoeker GeefBezoeker(int persoonId)
        {
            MySqlConnection connectie = GetConnection();
            Bezoeker bezoeker = null;
            string query = "Select * FROM Visitor WHERE VisitorId = @visitorId AND IsDeleted = 0";
            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", persoonId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        bezoeker = new Bezoeker(persoonId, Name, Voornaam, Email, Company);
                    }
                    reader.Close();
                    return bezoeker;
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoeker", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }

        }

        public List<Bezoeker> GeefBezoekers()
        {
            MySqlConnection connectie = GetConnection();
            List<Bezoeker> bezoekers = new List<Bezoeker>();
            string query = "SELECT * FROM Visitor WHERE IsDeleted = 0";
            // TODO : select *
            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitorId"];
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        bezoekers.Add(new Bezoeker(Id, Name, Voornaam, Email, Company));
                    }
                    reader.Close();
                    return bezoekers;
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoekers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public IEnumerable<Bezoeker> GeefBezoekers(string? naam, string? voornaam, string? email, string? bedrijf)
        {
            MySqlConnection connectie = GetConnection();
            string query;
            List<Bezoeker> bezoekers = new List<Bezoeker>();

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(voornaam) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(bedrijf)) query = "SELECT * FROM Visitor";
                    else
                    {
                        query = "SELECT * FROM Visitor WHERE ";
                        if (!string.IsNullOrEmpty(naam))
                        {
                            query += "LastName LIKE @lastname";
                            cmd.Parameters.AddWithValue("@lastname", "%" + naam + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(voornaam) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(voornaam))
                        {
                            query += "FirstName LIKE @firstname";
                            cmd.Parameters.AddWithValue("@firstname", "%" + voornaam + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            query += "Email LIKE @email";
                            cmd.Parameters.AddWithValue("@email", "%" + email + "%");
                        }
                        if (!string.IsNullOrWhiteSpace(bedrijf) && query != "SELECT * FROM Visitor WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(bedrijf))
                        {
                            query += "Company LIKE @company";
                            cmd.Parameters.AddWithValue("@company", "%" + bedrijf + "%");
                        }
                        query += " AND IsDeleted = 0";
                    }


                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["VisitorId"];
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        string Email = (string)reader["Email"];
                        string Company = (string)reader["Company"];
                        bezoekers.Add(new Bezoeker(ID, Name, Voornaam, Email, Company));
                    }
                    reader.Close();
                    return bezoekers;
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoekers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public void UpdateBezoeker(int bezoekerid, string? naam, string? voornaam, string? email, string? bedrijf)
        {
            MySqlConnection connectie = GetConnection();
            //string query = "UPDATE Visitor SET Name=@naam, Voornaam=@firstname, Email=@email, Company=@company WHERE VisitorId=@visitorId";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    string query = "UPDATE Visitor SET ";

                    if (!string.IsNullOrEmpty(naam))
                    {
                        query += "LastName=@lastname";
                        cmd.Parameters.AddWithValue("@lastname", naam);
                    }
                    if (!string.IsNullOrEmpty(voornaam) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(voornaam))
                    {
                        query += "FirstName=@firstname";
                        cmd.Parameters.AddWithValue("@firstname", voornaam);
                    }
                    if (!string.IsNullOrEmpty(email) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(email))
                    {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrEmpty(bedrijf) && query != "UPDATE Visitor SET ") query += ",";
                    if (!string.IsNullOrEmpty(bedrijf))
                    {
                        query += "Company=@company";
                        cmd.Parameters.AddWithValue(@"company", bedrijf);
                    }
                    query += " WHERE VisitorId=@visitorId";
                    cmd.Parameters.AddWithValue("@visitorId", bezoekerid);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("UpdateBezoekers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public void VerwijderBezoeker(Bezoeker bezoeker)
        {
            MySqlConnection connectie = GetConnection();
            string query = "UPDATE Visitor SET IsDeleted = 1 WHERE VisitorId =@visitorId OR Email=@email";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitorId", bezoeker.PersoonId);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);

                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("VerwijderBezoeker", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public void VoegBezoekerToe(Bezoeker bezoeker)
        {
            MySqlConnection connectie = GetConnection();
            string query = "INSERT INTO Visitor (LastName, FirstName, Email, Company) VALUES (@lastname, @firstname,@email,@company); SELECT LAST_INSERT_ID()";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;


                    cmd.Parameters.AddWithValue("@lastname", bezoeker.Naam);
                    cmd.Parameters.AddWithValue("@firstname", bezoeker.Voornaam);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);
                    cmd.Parameters.AddWithValue("@company", bezoeker.Bedrijf);

                    int bezoekerId = (int)(long)(ulong)cmd.ExecuteScalar();
                    bezoeker.ZetId(bezoekerId);
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("VoegBezoekerToe", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }


        }

    }
}
