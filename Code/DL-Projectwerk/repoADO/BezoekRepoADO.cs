using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

namespace DL_Projectwerk.repoADO
{
    public class BezoekRepoADO : IBezoekRepository
    {
        private BezoekerRepoADO bezoekerRepoADO;
        private BedrijfRepoADO bedrijfRepoADO;
        private WerknemerRepoADO werknemerRepoADO;
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public BezoekRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }
        private Bezoeker GeefBezoeker(int persoonId)
        {
            MySqlConnection connectie = GetConnection();
            Bezoeker bezoeker = null;
            string query = "SELECT * FROM Visitor WHERE VisitorId = @visitorId";
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
                        string Bedrijf = (string)reader["Company"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        bezoeker = new Bezoeker(persoonId, Name, Voornaam, Email, Bedrijf);
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

        public bool BestaatBezoek(Bezoek bezoek)
        {
            if (bezoek is null) { throw new WerknemerRepoADOException("BezoekRepoADO - VoegBezoekToe - bezoek is null"); }

            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Visit WHERE IsDeleted=0 AND Visitor=@visitorId AND VisitorEmail=@visitorEmail AND VisitedCompany=@visitedCompany AND ContactPerson=@contactPerson AND ";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.Add("@visitorId", MySqlDbType.Int32);
                    cmd.Parameters.Add("@visitorEmail", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@visitedCompany", MySqlDbType.Int32);
                    cmd.Parameters.Add("@contactPerson", MySqlDbType.Int32);
                    cmd.Parameters.Add("@registerTime", MySqlDbType.DateTime);

                    cmd.Parameters["@visitorId"].Value = bezoek.Bezoeker.PersoonId;
                    cmd.Parameters["@visitorEmail"].Value = bezoek.Bezoeker.Email;
                    cmd.Parameters["@visitedCompany"].Value = bezoek.Bedrijf.Id;
                    cmd.Parameters["@contactPerson"].Value = bezoek.Contactpersoon.PersoonId;
                    if (bezoek.Contactpersoon.Email != null)
                    {
                        query += "ContactPersonEmail=@contactpersonEmail AND ";
                        cmd.Parameters.Add("@ContactPersonEmail", MySqlDbType.VarChar);
                        cmd.Parameters["@ContactPersonEmail"].Value = bezoek.Contactpersoon.Email;
                    }
                    cmd.Parameters["@registerTime"].Value = bezoek.StartTijd;
                    query += "RegisterTime=@registerTime AND IsDeleted = 0";
                    cmd.CommandText = query;
                    int i = (int)(long)cmd.ExecuteScalar();
                    if (i > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("BestaatBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Bezoek GeefBezoek(int bezoekId)
        {
            MySqlConnection connection = GetConnection();
            Bezoek bezoek = null;
            string query = "SELECT * FROM Visit WHERE VisitId=@visitId AND IsDeleted = 0";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitId", bezoekId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Bezoeker Bezoeker = bezoekerRepoADO.GeefBezoeker((int)reader["Visitor"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["VisitedCompany"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["ContactPerson"]);
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd = (DateTime)reader["UnRegistrationTime"];
                        //bezoek = new Bezoek(bezoekId, Visitor, Bedrijf, ContactPerson, Starttijd, Eindtijd);
                    }
                    reader.Close();
                    return bezoek;
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("GeefBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Bezoek> GeefBezoeken()
        {
            MySqlConnection connection = GetConnection();
            List<Bezoek> bezoeken = new List<Bezoek>();
            string query = "SELECT * FROM Visit WHERE IsDeleted = 0";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitId"];
                        Bezoeker Bezoeker = GeefBezoeker((int)reader["Visitor"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["VisitedCompany"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["ContactPerson"]);
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd;
                        if (reader["UnregistrationTime"] != DBNull.Value) { Eindtijd = (DateTime)reader["UnregistrationTime"]; }
                        else { Eindtijd = Starttijd; }
                        bezoeken.Add(new Bezoek(Id, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return bezoeken;
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Bezoek> GeefBezoeken(Bezoeker? bezoeker, Bedrijf? bedrijf, Werknemer? contactpersoon, string? StartTijd)
        {
            MySqlConnection connection = GetConnection();
            string query;
            List<Bezoek> bezoeken = new List<Bezoek>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    if (bezoeker == null && bedrijf == null && contactpersoon == null && StartTijd == null) query = "SELECT * FROM Visit";
                    else
                    {
                        query = "SELECT * FROM Visit WHERE ";
                        if (bezoeker != null)
                        {
                            query += "Visitor = @visitor";
                            cmd.Parameters.AddWithValue("@visitor", bezoeker.PersoonId);
                        }
                        if (bedrijf != null && query != "SELECT * FROM Visit WHERE ") query += " AND ";
                        if (bedrijf != null)
                        {
                            query += "VisitedCompany=@visitedCompany";
                            cmd.Parameters.AddWithValue("@visitedCompany", bedrijf.Id);
                        }
                        if (contactpersoon != null && query != "SELECT * FROM Visit WHERE ") query += " AND ";
                        if (contactpersoon != null)
                        {
                            query += "ContactPerson=@contactPerson";
                            cmd.Parameters.AddWithValue("@contactPerson", contactpersoon.PersoonId);
                        }
                        if (StartTijd != null && query != "SELECT * FROM Visit WHERE ") query += " AND ";
                        if (StartTijd != null)
                        {
                            query += "RegisterTime>@registerTime";
                            cmd.Parameters.AddWithValue("@registerTime", StartTijd);
                        }
                        query += " AND IsDeleted = 0";
                    }

                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitId"];
                        Bezoeker Bezoeker = GeefBezoeker((int)reader["Visitor"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["VisitedCompany"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["ContactPerson"]);
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd;
                        if (reader["UnregistrationTime"] != DBNull.Value) { Eindtijd = (DateTime)reader["UnregistrationTime"]; }
                        else { Eindtijd = Starttijd; }
                        bezoeken.Add(new Bezoek(Id, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return bezoeken;
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateBezoek(Bezoek bezoek)
        {
            MySqlConnection connection = GetConnection();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Visit SET Visitor=@visitorId, VisitedCompany=@visitedCompany, ContactPerson=@contactPerson, RegisterTime=@registerTime WHERE VisitId=@visitId";
                    cmd.Parameters.AddWithValue("@visitId", bezoek.BezoekId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("UpdateBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VerwijderBezoek(Bezoek bezoek)
        {
            MySqlConnection connection = GetConnection();
            string query = "UPDATE Visitor SET IsDeleted = 1 WHERE VisitId=@VisitId";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitId", bezoek.BezoekId);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("VerwijderBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VoegBezoekToe(Bezoek bezoek)
        {
            if (bezoek is null) { throw new WerknemerRepoADOException("BezoekRepoADO - VoegBezoekToe - bezoek is null"); }

            MySqlConnection connection = GetConnection();
            string query = "INSERT INTO Visit (Visitor, VisitorEmail, VisitedCompany, ContactPerson, RegisterTime) VALUES (@visitorId, @visitorEmail, @visitedCompany, @contactPerson, @registerTime); SELECT LAST_INSERT_ID();";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.Add("@visitorId", MySqlDbType.Int32);
                    cmd.Parameters.Add("@visitorEmail", MySqlDbType.VarChar);
                    cmd.Parameters.Add("@visitedCompany", MySqlDbType.Int32);
                    cmd.Parameters.Add("@contactPerson", MySqlDbType.Int32);
                    cmd.Parameters.Add("@registerTime", MySqlDbType.DateTime);

                    cmd.Parameters["@visitorId"].Value = bezoek.Bezoeker.PersoonId;
                    cmd.Parameters["@visitorEmail"].Value = bezoek.Bezoeker.Email;
                    cmd.Parameters["@visitedCompany"].Value = bezoek.Bedrijf.Id;
                    cmd.Parameters["@contactPerson"].Value = bezoek.Contactpersoon.PersoonId;
                    if (bezoek.Contactpersoon.Email != null)
                    {
                        query = "INSERT INTO Visit (Visitor, VisitorEmail, VisitedCompany, ContactPerson, ContactPersonEmail, RegisterTime) VALUES (@visitor, @visitorEmail, @visitedCompany, @contactPerson, @contactPersonEmail, @registerTime); SELECT LAST_INSERT_ID();";
                        cmd.Parameters.Add("@contactPersonEmail", MySqlDbType.VarChar);
                        cmd.Parameters["@contactPersonEmail"].Value = bezoek.Contactpersoon.Email; // TODO update, email is anders
                    }
                    cmd.Parameters["@registerTime"].Value = bezoek.StartTijd;
                    cmd.CommandText = query;
                    bezoek.ZetId((int)(long)(ulong)cmd.ExecuteNonQuery());
                }
                catch (Exception ex)
                {
                    throw new BezoekRepoADOException("VoegBezoekToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void LogoutBezoek(string email)
        {
            MySqlConnection connection = GetConnection();
            string query = "UPDATE Visit SET UnregistrationTime=@unregistrationTime WHERE VisitorEmail=@email AND UnregistrationTime IS NULL";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@unregistrationTime", DateTime.Now);

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("LogoutBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool IsLoggedIn(string email)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Visit WHERE VisitorEmail=@email AND YEAR(RegisterTime) = YEAR(NOW()) AND MONTH(RegisterTime) = MONTH(NOW()) AND DAY(RegisterTime) = DAY(NOW())";

            DateTime today = DateTime.Now.Date;
            int todayYear = today.Year;
            int todayMonth = today.Month;
            int todayDay = today.Day;

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@todayYear", todayYear);
                    cmd.Parameters.AddWithValue("@todayMonth", todayMonth);
                    cmd.Parameters.AddWithValue("@todayDay", todayDay);

                    cmd.CommandText = query;
                    int i = (int)(long)cmd.ExecuteScalar();

                    Console.WriteLine(email);
                    Console.WriteLine(query);

                    if (i == 1) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
