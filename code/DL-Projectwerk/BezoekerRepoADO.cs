using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk
{
    public class BezoekerRepoADO : IBezoekerRepository
    {
        private string connectieString;
        public BezoekerRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectieString);
        }

        public bool BestaatBezoeker(Bezoeker bezoeker)
        {
            SqlConnection connectie = GetConnection();
            string query = "SELECT COUNT(*) FROM Bezoeker WHERE Naam=@naam AND Voornaam=@voornaam AND Email=@email AND Bedrijf=@bedrijf AND IsVerwijderd = 0";
            
            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@naam", bezoeker.Naam);
                    cmd.Parameters.AddWithValue("@voornaam", bezoeker.Voornaam);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);
                    cmd.Parameters.AddWithValue("@bedrijf", bezoeker.Bedrijf);

                    int count = (int)cmd.ExecuteScalar();
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
            SqlConnection connectie = GetConnection();
            string query = "SELECT COUNT(*) FROM Bezoeker WHERE BezoekerId=@bezoekerId AND IsVerwijderd = 0";
            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@bezoekerId", value);

                    int count = (int)cmd.ExecuteScalar();
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
            SqlConnection connectie = GetConnection();
            Bezoeker bezoeker = null;
            string query = "Select * FROM Bezoeker where BezoekerId = @bezoekerId AND IsVerwijderd = 0";
            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@bezoekerId", persoonId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        string Email = (string)reader["Email"];
                        string Bedrijf = (string)reader["Bedrijf"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        bezoeker = new Bezoeker(persoonId, Naam, Voornaam, Email, Bedrijf);
                    }
                    reader.Close();
                    return bezoeker;
                } catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoeker", ex);
                } finally
                {
                    connectie.Close();
                }
            }
           
        }

        public List<Bezoeker> GeefBezoekers()
        {
            SqlConnection connectie = GetConnection();
            List<Bezoeker> bezoekers = new List<Bezoeker>();
            string query = "Select * FROM Bezoeker WHERE IsVerwijderd = 0";
            
            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["BezoekerId"];
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        string Email = (string)reader["Email"];
                        string Bedrijf = (string)reader["Bedrijf"];
                        bezoekers.Add(new Bezoeker(Id, Naam, Voornaam, Email, Bedrijf));
                    }
                    reader.Close();
                    return bezoekers;
                } catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoekers", ex);
                } finally
                {
                    connectie.Close();
                }
            }
        }

        public IEnumerable<Bezoeker> GeefBezoekers(string? naam, string? voornaam, string? email, string? bedrijf)
        {
            SqlConnection connectie = GetConnection();
            string query;
            List<Bezoeker> bezoekers = new List<Bezoeker>();

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(voornaam) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(bedrijf)) query = "SELECT * FROM Bezoeker";
                    else
                    {
                        query = "SELECT * FROM Bezoeker WHERE ";
                        if (!string.IsNullOrEmpty(naam))
                        {
                            query += "Naam = @naam";
                            cmd.Parameters.AddWithValue("@naam", naam);
                        }
                        if (!string.IsNullOrWhiteSpace(voornaam) && query != "SELECT * FROM Bezoeker WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(voornaam))
                        {
                            query += "Voornaam = @voornaam";
                            cmd.Parameters.AddWithValue("@voornaam", voornaam);
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != "SELECT * FROM Bezoeker WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            query += "Email = @email";
                            cmd.Parameters.AddWithValue("@email", email);
                        }
                        if (!string.IsNullOrWhiteSpace(bedrijf) && query != "SELECT * FROM Bezoeker WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(bedrijf))
                        {
                            query += "Bedrijf = @bedrijf";
                            cmd.Parameters.AddWithValue("@bedrijf", bedrijf);
                        }
                        query += " AND IsVerwijderd = 0";
                    }


                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["BezoekerId"];
                        string Naam = (string)reader["Naam"];
                        string Voornaam = (string)reader["Voornaam"];
                        string Email = (string)reader["Email"];
                        string Bedrijf = (string)reader["Bedrijf"];
                        bezoekers.Add(new Bezoeker(ID, Naam, Voornaam, Email, Bedrijf));
                    }
                    reader.Close();
                    return bezoekers;
                } catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("GeefBezoekers", ex);
                } finally
                {
                    connectie.Close();
                }
            }
        }

        public void UpdateBezoeker(int bezoekerid, string? naam, string? voornaam, string? email, string? bedrijf)
        {
            SqlConnection connectie = GetConnection();
            //string query = "UPDATE Bezoeker SET Naam=@naam, Voornaam=@voornaam, Email=@email, Bedrijf=@bedrijf WHERE BezoekerId=@bezoekerId";

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    string query = "UPDATE Bezoeker SET ";

                    if (!string.IsNullOrEmpty(naam))
                    {
                        query += "Naam=@naam";
                        cmd.Parameters.AddWithValue("@naam", naam);
                    }
                    if (!string.IsNullOrEmpty(voornaam) && query != "UPDATE Bezoeker SET ") query += ",";
                    if (!string.IsNullOrEmpty(voornaam))
                    {
                        query += "Voornaam=@voornaam";
                        cmd.Parameters.AddWithValue("@voornaam", voornaam);
                    }
                    if (!string.IsNullOrEmpty(email) && query != "UPDATE Bezoeker SET ") query += ",";
                    if (!string.IsNullOrEmpty(email))
                    {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrEmpty(bedrijf) && query != "UPDATE Bezoeker SET ") query += ",";
                    if (!string.IsNullOrEmpty(bedrijf))
                    {
                        query += "Bedrijf=@bedrijf";
                        cmd.Parameters.AddWithValue(@"bedrijf", bedrijf);
                    }
                    query += " WHERE BezoekerId=@bezoekerId";
                    cmd.Parameters.AddWithValue("@bezoekerId", bezoekerid); 
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
            SqlConnection connectie = GetConnection();
            string query = "UPDATE Bezoeker SET IsVerwijderd = 1 where BezoekerId =@bezoekerId OR Email=@email";

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText= query;
                    cmd.Parameters.AddWithValue("@bezoekerId", bezoeker.PersoonId);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);

                    cmd.ExecuteNonQuery();


                } catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("VerwijderBezoeker", ex);
                } finally
                {
                    connectie.Close();
                }
            }
        }

        public void VoegBezoekerToe(Bezoeker bezoeker)
        {
            SqlConnection connectie = GetConnection();
            string query = "INSERT INTO Bezoeker (Naam, Voornaam, Email, Bedrijf) OUTPUT Inserted.BezoekerID VALUES (@naam, @voornaam,@email,@bedrijf)";

            using (SqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;


                    cmd.Parameters.AddWithValue("@naam", bezoeker.Naam);
                    cmd.Parameters.AddWithValue("@voornaam", bezoeker.Voornaam);
                    cmd.Parameters.AddWithValue("@email", bezoeker.Email);
                    cmd.Parameters.AddWithValue("@bedrijf", bezoeker.Bedrijf);

                    int bezoekerId = (int)cmd.ExecuteScalar();
                    bezoeker.ZetId(bezoekerId);
                }
                catch (Exception ex)
                {
                    throw new BezoekerRepoADOException("VoegBezoekerToe", ex);
                } finally
                {
                    connectie.Close();
                }
            }

            
        }

    }
}
