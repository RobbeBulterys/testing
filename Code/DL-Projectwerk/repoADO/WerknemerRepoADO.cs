using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.repoADO
{
    public class WerknemerRepoADO : IWerknemerRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public WerknemerRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool BestaatWerknemer(int werknemerId)
        {
            if (werknemerId <= 0) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - ongeldig werknemerId"); }
            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Employee WHERE EmployeeId=@employeeId AND IsDeleted=0";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@employeeId", MySqlDbType.Int32);
                    cmd.Parameters["@employeeId"].Value = werknemerId;
                    if ((int)(long)cmd.ExecuteScalar() > 0)
                    {
                        // 1 werknemer gevonden
                        return true;
                    }
                    else
                    {
                        // geen werknemer gevonden
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("BestaatWerknemer", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool BestaatWerknemer(string naam, string voornaam)
        {
            if (naam is null) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - naam is null"); }
            if (voornaam is null) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - voornaam is null"); }
            MySqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM Employee WHERE LastName=@lastname AND FirstName=@firstname AND IsDeleted=0;";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@lastname", naam);
                    cmd.Parameters.AddWithValue("@firstname", voornaam);

                    if ((int)(long)cmd.ExecuteScalar() > 0)
                    {
                        // 1 werknemer gevonden
                        return true;
                    }
                    else
                    {
                        // geen werknemer gevonden
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("BestaatWerknemer", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VoegWerknemerToe(Werknemer werknemer)
        {
            if (werknemer is null) { throw new WerknemerRepoADOException("WerknemerRepoADO - VoegWerknemerToe - werknemer is null"); }

            MySqlConnection connection = GetConnection();
            string sql = "INSERT INTO Employee (LastName, FirstName) VALUES (@lastname, @firstname); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@lastname", werknemer.Naam);
                    cmd.Parameters.AddWithValue("@firstname", werknemer.Voornaam);

                    int werknemerId = (int)(long)(ulong)cmd.ExecuteScalar();
                    werknemer.ZetId(werknemerId);

                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("VoegWerknemerToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Werknemer GeefWerknemer(int persoonId)
        {
            MySqlConnection connectie = GetConnection();
            Werknemer werknemer = null;
            string query = "SELECT * FROM Employee WHERE EmployeeId = @employeeId AND IsDeleted = 0";
            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", persoonId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int WerknemerID = (int)reader["EmployeeId"];
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        werknemer = new Werknemer(persoonId, Name, Voornaam);
                    }
                    reader.Close();
                    return werknemer;
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

        public List<Werknemer> GeefWerknemers()
        {

            MySqlConnection connectie = GetConnection();
            string query = "SELECT * FROM Employee WHERE IsDeleted=0";
            List<Werknemer> alleWerknemers = new List<Werknemer>();

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["EmployeeId"];
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        alleWerknemers.Add(new Werknemer(ID, Name, Voornaam));
                    }
                    reader.Close();
                    return alleWerknemers;
                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("GeefWerknemers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public IEnumerable<Werknemer> GeefWerknemers(string? naam, string? voornaam)
        {
            MySqlConnection connectie = GetConnection();
            string query = "SELECT * FROM Employee WHERE IsDeleted=0";
            List<Werknemer> werknemers = new List<Werknemer>();

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrEmpty(voornaam)) query = "SELECT * FROM Employee";
                    else
                    {
                        query = "SELECT * FROM Employee WHERE ";
                        if (!string.IsNullOrWhiteSpace(naam))
                        {
                            query += "LastName=@lastname";
                            cmd.Parameters.AddWithValue("@lastname", naam);
                        }
                        if (!string.IsNullOrWhiteSpace(voornaam) && query != "SELECT * FROM Employee WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(voornaam))
                        {
                            query += "FirstName=@firstname";
                            cmd.Parameters.AddWithValue("@firstname", voornaam);
                        }
                        query += " AND IsDeleted = 0";
                    }

                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["EmployeeId"];
                        string Name = (string)reader["LastName"];
                        string Voornaam = (string)reader["FirstName"];
                        werknemers.Add(new Werknemer(ID, Name, Voornaam));
                    }
                    reader.Close();
                    return werknemers;
                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("GeefWerknemers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }

        public void UpdateWerknemer(int werknemerId, string? naam, string? voornaam)
        {

            MySqlConnection connectie = GetConnection();
            string query = "Update Employee SET  ";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();

                    if (!string.IsNullOrWhiteSpace(naam))
                    {
                        query += "LastName=@lastname";
                        cmd.Parameters.AddWithValue("@lastname", naam);
                    }
                    if (!string.IsNullOrWhiteSpace(voornaam) && query != "Update Employee SET  ") query += ",";
                    if (!string.IsNullOrWhiteSpace(voornaam))
                    {
                        query += "FirstName=@firstname";
                        cmd.Parameters.AddWithValue("@firstname", voornaam);
                    }
                    query += " WHERE EmployeeId=@employeeId";
                    Console.WriteLine(query);
                    cmd.Parameters.AddWithValue("@employeeId", werknemerId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("UpdateWerknemer");
                }
                finally { connectie.Close(); }
            }
        }

        public void VerwijderWerknemer(int werknemerId)
        {
            MySqlConnection connectie = GetConnection();
            string queryWerknemer = "UPDATE Employee SET IsDeleted = 1 WHERE EmployeeId=@employeeId";
            string queryContract = "UPDATE Employeecontract SET IsDeleted = 1, ActiveWorkingAtCompany = 0 WHERE EmployeeId=@employeeId";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.Parameters.AddWithValue("@employeeId", werknemerId);

                    cmd.CommandText = queryWerknemer;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = queryContract;
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("VerwijderWerknemer", ex);
                }
                finally { connectie.Close(); }
            }
        }
    }
}
