using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System.Data;
using System.Data.SqlClient;

namespace DL_Projectwerk
{
    public class BezoekRepoADO : IBezoekRepository
    {
        private BezoekerRepoADO bezoekerRepoADO;
        private BedrijfRepoADO bedrijfRepoADO;
        private WerknemerRepoADO werknemerRepoADO;
        private string connectieString;
        public BezoekRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }

        public bool BestaatBezoek(Bezoek bezoek) {
            if (bezoek is null) { throw new WerknemerRepoADOException("BezoekRepoADO - VoegBezoekToe - bezoek is null"); }

            SqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Bezoek WHERE IsVerwijderd=0 AND Bezoeker=@Bezoeker AND BezoekerEmail=@BezoekerEmail AND BezochtBedrijf=@BezochtBedrijf AND Contactpersoon=@Contactpersoon AND ";

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.Add("@Bezoeker", SqlDbType.Int);
                    cmd.Parameters.Add("@BezoekerEmail", SqlDbType.VarChar);
                    cmd.Parameters.Add("@BezochtBedrijf", SqlDbType.Int);
                    cmd.Parameters.Add("@Contactpersoon", SqlDbType.Int);
                    cmd.Parameters.Add("@Starttijd", SqlDbType.DateTime);

                    cmd.Parameters["@Bezoeker"].Value = bezoek.Bezoeker.PersoonId;
                    cmd.Parameters["@BezoekerEmail"].Value = bezoek.Bezoeker.Email;
                    cmd.Parameters["@BezochtBedrijf"].Value = bezoek.Bedrijf.Id;
                    cmd.Parameters["@Contactpersoon"].Value = bezoek.Contactpersoon.PersoonId;
                    if (bezoek.Contactpersoon.Email != null)
                    {
                        query += "ContactpersoonEmail=@ContactpersoonEmail AND ";
                        cmd.Parameters.Add("@ContactpersoonEmail", SqlDbType.VarChar);
                        cmd.Parameters["@ContactpersoonEmail"].Value = bezoek.Contactpersoon.Email;
                    }
                    cmd.Parameters["@Starttijd"].Value = bezoek.StartTijd;
                    query += "StartTijd=@StartTijd AND IsVerwijderd = 0";
                    cmd.CommandText = query;
                    int i = (int)cmd.ExecuteScalar();
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

        public Bezoek GeefBezoek(int bezoekId) {
            SqlConnection connection = GetConnection();
            Bezoek bezoek = null;
            string query = "Select * FROM Bezoek WHERE BezoekId=@bezoekId AND IsVerwijderd = 0";
            
            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText= query;
                    cmd.Parameters.AddWithValue("@BezoekId", bezoekId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Bezoeker Bezoeker = bezoekerRepoADO.GeefBezoeker((int)reader["Bezoeker"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["BezochtBedrijf"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["Contactpersoon"]);
                        DateTime Starttijd = (DateTime)reader["Starttijd"];
                        DateTime Eindtijd = (DateTime)reader["Eindtijd"];
                        //bezoek = new Bezoek(bezoekId, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd);
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

        public List<Bezoek> GeefBezoeken() {
            SqlConnection connection = GetConnection();
            List<Bezoek> bezoeken = new List<Bezoek>();
            string query = "Select * FROM Bezoek WHERE IsVerwijderd = 0";

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["BezoekId"];
                        Bezoeker Bezoeker = bezoekerRepoADO.GeefBezoeker((int)reader["Bezoeker"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["BezochtBedrijf"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["Contactpersoon"]);
                        DateTime Starttijd = (DateTime)reader["Starttijd"];
                        DateTime Eindtijd;
                        if (reader["Eindtijd"] != DBNull.Value) { Eindtijd = (DateTime)reader["Eindtijd"]; }
                        else { Eindtijd = Starttijd; }
                        bezoeken.Add(new Bezoek(Id, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return bezoeken;
                }
                catch(Exception ex)
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
            SqlConnection connection = GetConnection();
            string query;
            List<Bezoek> bezoeken = new List<Bezoek>();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    if (bezoeker == null && bedrijf == null && contactpersoon == null && StartTijd == null) query = "SELECT * FROM Bezoek";
                    else
                    {
                        query = "SELECT * FROM Bezoek WHERE ";
                        if (bezoeker != null)
                        {
                            query += "Bezoeker = @bezoeker";
                            cmd.Parameters.AddWithValue("@bezoeker", bezoeker.PersoonId);
                        }
                        if (bedrijf != null && query != "SELECT * FROM Bezoek WHERE ") query += " AND ";
                        if (bedrijf != null)
                        {
                            query += "BezochtBedrijf=@bezochtbedrijf";
                            cmd.Parameters.AddWithValue("@bezochtbedrijf", bedrijf.Id);
                        }
                        if (contactpersoon != null && query != "SELECT * FROM Bezoek WHERE ") query += " AND ";
                        if (contactpersoon != null)
                        {
                            query += "Contactpersoon=@contactpersoon";
                            cmd.Parameters.AddWithValue("@contactpersoon", contactpersoon.PersoonId);
                        }
                        if (StartTijd != null && query != "SELECT * FROM Bezoek WHERE ") query += " AND ";
                        if (StartTijd != null)
                        {
                            query += "Starttijd>@starttijd";
                            cmd.Parameters.AddWithValue("@starttijd", StartTijd);
                        }
                        query += " AND IsVerwijderd = 0";
                    }

                    bezoekerRepoADO = new BezoekerRepoADO(connectieString);
                    bedrijfRepoADO = new BedrijfRepoADO(connectieString);
                    werknemerRepoADO = new WerknemerRepoADO(connectieString);
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["BezoekId"];
                        Bezoeker Bezoeker = bezoekerRepoADO.GeefBezoeker((int)reader["Bezoeker"]);
                        Bedrijf Bedrijf = bedrijfRepoADO.GeefBedrijfOpId((int)reader["BezochtBedrijf"]);
                        Werknemer Contactpersoon = werknemerRepoADO.GeefWerknemer((int)reader["Contactpersoon"]);
                        DateTime Starttijd = (DateTime)reader["Starttijd"];
                        DateTime Eindtijd;
                        if (reader["Eindtijd"] != DBNull.Value) { Eindtijd = (DateTime)reader["Eindtijd"]; }
                        else { Eindtijd = Starttijd; }
                        bezoeken.Add(new Bezoek(Id, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return bezoeken;
                }
                catch(Exception ex)
                {
                    throw new BezoekRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateBezoek(Bezoek bezoek) {
            SqlConnection connection = GetConnection();

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Bezoek SET Bezoeker=@Bezoeker,Bedrijf=@BezochtBedrijf,Contactpersoon=@Contactpersoon,StartTijd=@StartTijd WHERE BezoekId=@BezoekId";
                    cmd.Parameters.AddWithValue("@BezoekId", bezoek.BezoekId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new BezoekRepoADOException("UpdateBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void VerwijderBezoek(Bezoek bezoek) {
            SqlConnection connection = GetConnection();
            string query = "UPDATE Bezoeker SET IsVerwijderd = 1 where BezoekId=@BezoekId";

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@BezoekId", bezoek.BezoekId);

                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
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

            SqlConnection connection = GetConnection();
            string query = "INSERT INTO Bezoek (Bezoeker, BezoekerEmail, BezochtBedrijf, Contactpersoon, Starttijd) output INSERTED.BezoekId VALUES (@Bezoeker, @BezoekerEmail, @BezochtBedrijf, @Contactpersoon, @Starttijd);";

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.Add("@Bezoeker", SqlDbType.Int);
                    cmd.Parameters.Add("@BezoekerEmail", SqlDbType.VarChar);
                    cmd.Parameters.Add("@BezochtBedrijf", SqlDbType.Int);
                    cmd.Parameters.Add("@Contactpersoon", SqlDbType.Int);
                    cmd.Parameters.Add("@Starttijd", SqlDbType.DateTime);

                    cmd.Parameters["@Bezoeker"].Value = bezoek.Bezoeker.PersoonId;
                    cmd.Parameters["@BezoekerEmail"].Value = bezoek.Bezoeker.Email;
                    cmd.Parameters["@BezochtBedrijf"].Value = bezoek.Bedrijf.Id;
                    cmd.Parameters["@Contactpersoon"].Value = bezoek.Contactpersoon.PersoonId;
                    if(bezoek.Contactpersoon.Email != null)
                    {
                        query = "INSERT INTO Bezoek (Bezoeker, BezoekerEmail, BezochtBedrijf, Contactpersoon, ContactpersoonEmail, Starttijd) output INSERTED.BezoekId VALUES (@Bezoeker, @BezoekerEmail, @BezochtBedrijf, @Contactpersoon, @ContactpersoonEmail, @Starttijd);";
                        cmd.Parameters.Add("@ContactpersoonEmail", SqlDbType.VarChar);
                        cmd.Parameters["@ContactpersoonEmail"].Value = bezoek.Contactpersoon.Email;
                    }
                    cmd.Parameters["@Starttijd"].Value = bezoek.StartTijd;
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
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
            SqlConnection connection = GetConnection();
            string query = "UPDATE Bezoek SET Eindtijd=@eindtijd WHERE BezoekerEmail=@email AND Eindtijd IS NULL";

            using (SqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();

                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@eindtijd", DateTime.Now);

                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new BezoekerRepoADOException("LogoutBezoek", ex);
                }
                finally
                { 
                    connection.Close();
                }
            }
        }
    }
}
