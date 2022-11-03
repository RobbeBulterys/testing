using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk {
    public class WerknemerRepoADO : IWerknemerRepository {
        private string connectieString;
        public WerknemerRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool BestaatWerknemer(int werknemerId) {
            if (werknemerId <= 0) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - ongeldig werknemerId"); }
            SqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Werknemer WHERE WerknemerId=@werknemerid AND IsVerwijderd=0";
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@werknemerid", SqlDbType.Int);
                    cmd.Parameters["@werknemerid"].Value = werknemerId;
                    if ((int)cmd.ExecuteScalar() > 0) {
                        // 1 werknemer gevonden
                        return true;
                    } else {
                        // geen werknemer gevonden
                        return false;
                    }
                } catch (Exception ex) {
                    throw new WerknemerRepoADOException("BestaatWerknemer", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public bool BestaatWerknemer(string naam, string voornaam) {
            if (naam is null) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - naam is null"); }
            if (voornaam is null) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - voornaam is null"); }
            SqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM Werknemer WHERE Naam=@naam AND Voornaam=@voornaam  AND IsVerwijderd=0";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@naam", naam);
                    cmd.Parameters.AddWithValue("@voornaam", voornaam);

                    if ((int)cmd.ExecuteScalar() > 0) {
                        // 1 werknemer gevonden
                        return true;
                    } else {
                        // geen werknemer gevonden
                        return false;
                    }
                } catch (Exception ex) {
                    throw new WerknemerRepoADOException("BestaatWerknemer", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public void VoegWerknemerToe(Werknemer werknemer) {
            if (werknemer is null) { throw new WerknemerRepoADOException("WerknemerRepoADO - VoegWerknemerToe - werknemer is null"); }

            SqlConnection connection = GetConnection();
            string sql = "INSERT INTO Werknemer(Naam, Voornaam) output INSERTED.WerknemerId VALUES (@naam, @voornaam);";
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@naam", werknemer.Naam);
                    cmd.Parameters.AddWithValue("@voornaam", werknemer.Voornaam);

                    int werknemerId = (int)cmd.ExecuteScalar();
                    werknemer.ZetId(werknemerId);

                } catch (Exception ex) {
                    throw new WerknemerRepoADOException("VoegWerknemerToe", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public Werknemer GeefWerknemer(int persoonId) {
            SqlConnection connectie = GetConnection();
            Werknemer werknemer = null;
            string query = "Select * FROM Werknemer where WerknemerId = @werknemerId AND IsVerwijderd = 0";
            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@werknemerId", persoonId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int WerknemerID = (int)reader["WerknemerId"];
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        werknemer = new Werknemer(persoonId, Naam, Voornaam);
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

            SqlConnection connectie = GetConnection();
            string query = "SELECT * FROM Werknemer WHERE IsVerwijderd=0";
            List<Werknemer> alleWerknemers = new List<Werknemer>();

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["WerknemerId"];
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        alleWerknemers.Add(new Werknemer(ID, Naam, Voornaam));
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

        public IEnumerable<Werknemer> GeefWerknemers(string? naam, string? voornaam) {
            SqlConnection connectie = GetConnection();
            string query = "SELECT * FROM Werknemer WHERE IsVerwijderd=0";
            List<Werknemer> werknemers = new List<Werknemer>();

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrEmpty(voornaam)) query = "SELECT * FROM Werknemer";
                    else
                    {
                        query = "SELECT * FROM Werknemer WHERE ";
                        if (!string.IsNullOrWhiteSpace(naam))
                        {
                            query += "Naam=@naam";
                            cmd.Parameters.AddWithValue("@naam", naam);
                        }
                        if (!string.IsNullOrWhiteSpace(voornaam) && query != "SELECT * FROM Werknemer WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(voornaam))
                        {
                            query += "Voornaam =@voornaam";
                            cmd.Parameters.AddWithValue("@voornaam", voornaam);
                        }
                        query += " AND IsVerwijderd = 0";
                    }

                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["WerknemerId"];
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        werknemers.Add(new Werknemer(ID, Naam, Voornaam));
                    }
                    reader.Close();
                    return werknemers;
                } 
                catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("GeefWerknemers", ex);
                } finally
                {
                    connectie.Close();
                }
            }
        }

        public void UpdateWerknemer(int werknemerId, string? naam, string? voornaam) {

            SqlConnection connectie = GetConnection();
            string query = "Update Werknemer SET  ";

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();

                    if (!string.IsNullOrWhiteSpace(naam))
                    {
                        query += "Naam=@naam";
                        cmd.Parameters.AddWithValue("@naam", naam);
                    }
                    if (!string.IsNullOrWhiteSpace(voornaam) && query != "Update Werknemer SET  ") query += ",";
                    if (!string.IsNullOrWhiteSpace(voornaam))
                    {
                        query += "Voornaam=@voornaam";
                        cmd.Parameters.AddWithValue("@voornaam", voornaam);
                    }
                    query += " WHERE WerknemerId=@werknemerId";
                    Console.WriteLine(query);
                    cmd.Parameters.AddWithValue("@werknemerId", werknemerId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("UpdateWerknemer");
                } finally { connectie.Close(); }
            }
        }

        public void VerwijderWerknemer(int werknemerId) {
            SqlConnection connectie = GetConnection();
            string queryWerknemer = "UPDATE Werknemer SET IsVerwijderd = 1 WHERE WerknemerId =@werknemerId";
            string queryContract = "UPDATE Werknemercontract SET IsVerwijderd = 1, ActiefWerkend = 0 WHERE WerknemerId =@werknemerId";

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.Parameters.AddWithValue("@werknemerId", werknemerId);

                    cmd.CommandText = queryWerknemer;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = queryContract;
                    cmd.ExecuteNonQuery();


                } catch (Exception ex)
                {
                    throw new WerknemerRepoADOException("VerwijderWerknemer", ex);
                } finally { connectie.Close(); }
            }
        }
    }
}
