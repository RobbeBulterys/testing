using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk {
    public class BedrijfRepoADO : IBedrijfRepository {
        private string connectieString;
        public BedrijfRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }

        public bool BedrijvenOpAdresAanwezig(int id) {
            return true;
        }

        public bool BestaatBedrijfZonderId(string btwnummer, string naam, string email) {
            SqlConnection conn = GetConnection();
            string query = @"SELECT COUNT(*) FROM dbo.Bedrijf WHERE "
            + "BTWnummer = @BTWnummer and Naam = @Naam and Email = @Email and IsVerwijderd = 0";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@BTWnummer", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));

                    cmd.Parameters["@BTWnummer"].Value = btwnummer;
                    cmd.Parameters["@Naam"].Value = naam;
                    cmd.Parameters["@Email"].Value = email;

                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("BestaatBedrijfZonderId", ex);
                } finally {
                    conn.Close();
                }
            }

        }
        public bool BestaatBedrijfMetId(int id) {
            SqlConnection conn = GetConnection();
            string query = @"SELECT COUNT(*) FROM dbo.Bedrijf WHERE "
            + "BedrijfsId = @Id and IsVerwijderd = 0";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));

                    cmd.Parameters["@Id"].Value = id;

                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("BestaatBedrijfMetId", ex);
                } finally {
                    conn.Close();
                }
            }
        }
        public void UpdateBedrijf(int id, string? btwnummer, string? naam, string? email, string? telefoon) {
            SqlConnection conn = GetConnection();

            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();

                    string query = "update Bedrijf set ";


                    if (!string.IsNullOrWhiteSpace(btwnummer)) {
                        query += "BTWnummer=@btwnummer";
                        cmd.Parameters.AddWithValue("@btwnummer", btwnummer);
                    }
                    if (!string.IsNullOrWhiteSpace(naam) && query != "update Bedrijf set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(naam)) {
                        query += "Naam=@naam";
                        cmd.Parameters.AddWithValue("@naam", naam);
                    }
                    if (!string.IsNullOrWhiteSpace(email) && query != "update Bedrijf set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(email)) {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrWhiteSpace(telefoon) && query != "update Bedrijf set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(telefoon)) {
                        query += "Telefoon=@telefoon";
                        cmd.Parameters.AddWithValue("@telefoon", telefoon);
                    }

                    query += " where BedrijfsId=@id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("UpdateBedrijf");
                } finally {
                    conn.Close();
                }
            }

        }
        public void VoegBedrijfToe(string btwnummer, string naam, string email, string? telefoon, int? adresId) {
            SqlConnection conn = GetConnection();

            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    string query = @"INSERT INTO dbo.bedrijf (BTWnummer,Naam,Email";
                    if (!string.IsNullOrWhiteSpace(telefoon)) query += ",Telefoon";
                    if (adresId.HasValue) query += ",AdresId";
                    query += ") VALUES (@BTWnummer,@naam,@email";
                    if (!string.IsNullOrWhiteSpace(telefoon)) {
                        query += ",@telefoon";
                        cmd.Parameters.Add(new SqlParameter("@telefoon", SqlDbType.VarChar));
                        cmd.Parameters["@telefoon"].Value = telefoon;

                    }
                    if (adresId.HasValue) {
                        query += ",@adresId";
                        cmd.Parameters.Add(new SqlParameter("@adresId", SqlDbType.Int));
                        cmd.Parameters["@adresId"].Value = adresId;
                    }
                    query += ")";


                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@BTWnummer", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.VarChar));


                    cmd.Parameters["@BTWnummer"].Value = btwnummer;
                    cmd.Parameters["@naam"].Value = naam;
                    cmd.Parameters["@email"].Value = email;

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("VoegBedrijfToe");
                } finally {
                    conn.Close();
                }
            }

        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }
        public void VerwijderBedrijf(int id) {
            SqlConnection conn = GetConnection();
            string query = @"update Bedrijf set IsVerwijderd = 1 where BedrijfsId = @bedrijfsid";
            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@bedrijfsid", SqlDbType.Int));

                    cmd.Parameters["@bedrijfsid"].Value = id;

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new AdresRepoADOException("VerwijderBedrijf", ex);
                } finally {
                    conn.Close();
                }
            }

        }
        public List<Bedrijf> GeefBedrijven() {
            SqlConnection connectie = GetConnection();
            List<Bedrijf> bedrijven = new List<Bedrijf>();
            string query = "select b1.BedrijfsId, b1.BTWnummer, b1.Naam, b1.Telefoon, b1.Email, " +
                "a1.AdresId, a1.Land, a1.Postcode, a1.Plaats, a1.Straat, a1.Nummer " +
                "from Bedrijf b1 left join Adres a1 on a1.AdresId = b1.AdresId where b1.IsVerwijderd = 0";

            using (SqlCommand cmd = connectie.CreateCommand()) {
                try {
                    connectie.Open();
                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int IdBedrijf = 0;
                        string BedrijfNaam = null;
                        string BTWnummer = null;
                        string Telefoon = null;
                        string Email = null;
                        int IdAdres = 0;
                        string Land = null;
                        string Postcode = null;
                        string Plaats = null;
                        string Straat = null;
                        string nummer = null;
                        IdBedrijf = (int)reader["BedrijfsId"];
                        BedrijfNaam = (string)reader["Naam"];
                        BTWnummer = (string)reader["BTWnummer"];
                        Email = (string)reader["Email"];
                        Bedrijf b = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);
                        try {
                            Telefoon = (string)reader["Telefoon"];
                            b.ZetTelefoon(Telefoon);
                        } catch (Exception ex) { }
                        try {
                            IdAdres = (int)reader["AdresId"];
                            Land = (string)reader["Land"];
                            Postcode = (string)reader["Postcode"];
                            Plaats = (string)reader["Plaats"];
                            Straat = (string)reader["Straat"];
                            nummer = (string)reader["Nummer"];
                            b.ZetAdres(new Adres(IdAdres, Straat, nummer, Postcode, Plaats, Land));
                        } catch (Exception ex) { }
                        bedrijven.Add(b);
                    }
                    reader.Close();
                    return bedrijven;
                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("GeefBezoekers", ex);
                } finally {
                    connectie.Close();
                }
            }
        }
        public List<Bedrijf> ZoekBedrijven(string? btwnummer, string? naam, string? email, string? telefoon) {
            SqlConnection connectie = GetConnection();
            string query;
            string ster = "b.*, a.Land, a.Plaats, a.Postcode, a.Straat, a.Nummer";
            List<Bedrijf> bedrijven = new List<Bedrijf>();

            using (SqlCommand cmd = connectie.CreateCommand()) {
                try {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(btwnummer) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(telefoon)) query = $"SELECT {ster} FROM Bedrijf b LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE b.IsVerwijderd=0";
                    else {
                        query = $"SELECT {ster} FROM Bedrijf b LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE ";
                        if (!string.IsNullOrWhiteSpace(naam)) {
                            query += "Naam = @naam";
                            cmd.Parameters.AddWithValue("@naam", naam);
                        }
                        if (!string.IsNullOrWhiteSpace(btwnummer) && query != $"SELECT {ster} FROM Bedrijf b LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(btwnummer)) {
                            query += "BTWnummer = @btwnummer";
                            cmd.Parameters.AddWithValue("@btwnummer", btwnummer);
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != $"SELECT {ster} FROM Bedrijf b LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email)) {
                            query += "Email = @email";
                            cmd.Parameters.AddWithValue("@email", email);
                        }
                        if (!string.IsNullOrWhiteSpace(telefoon) && query != $"SELECT {ster} FROM Bedrijf b LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(telefoon)) {
                            query += "Telefoon = @telefoon";
                            cmd.Parameters.AddWithValue("@telefoon", telefoon);
                        }
                        query += " AND b.IsVerwijderd = 0";
                    }


                    cmd.CommandText = query;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int IdBedrijf = (int)reader["BedrijfsId"];
                        string BedrijfNaam = (string)reader["Naam"];
                        string BTWnummer = (string)reader["BTWnummer"];
                        string Email = (string)reader["Email"];
                        Bedrijf b = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);

                        if (reader["Telefoon"] != DBNull.Value) {
                            string Telefoon = (string)reader["Telefoon"];
                            b.ZetTelefoon(Telefoon);
                        }

                        // Hebben we wel een adres?
                        if (reader["AdresId"] != DBNull.Value) {
                            int adresId = (int)reader["AdresId"];
                            string land = (string)reader["Land"];
                            string postcode = (string)reader["Postcode"];
                            string plaats = (string)reader["Plaats"];
                            string straat = (string)reader["Straat"];
                            string nummer = (string)reader["Nummer"];
                            Adres a = new Adres(adresId ,straat, nummer, postcode, plaats, land);
                            b.ZetAdres(a);
                        }

                        bedrijven.Add(b);
                    }
                    reader.Close();
                    return bedrijven;
                } catch (Exception ex) {
                    throw new BezoekerRepoADOException("GeefBezoekers", ex);
                } finally {
                    connectie.Close();
                }
            }
        }
        public Bedrijf GeefBedrijfOpId(int id) {
            SqlConnection connectie = GetConnection();
            Bedrijf bedrijf = null;
            string query = "select b1.BedrijfsId, b1.BTWnummer, b1.Naam, b1.Telefoon, b1.Email, " +
                "a1.AdresId, a1.Land, a1.Postcode, a1.Plaats, a1.Straat, a1.Nummer " +
                "from Bedrijf b1 left join Adres a1 on a1.AdresId = b1.AdresId " +
                "where BedrijfsId = @bedrijfid AND b1.IsVerwijderd = 0";

            using (SqlCommand cmd = connectie.CreateCommand()) {
                try {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@bedrijfid", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int IdBedrijf = 0;
                        string BedrijfNaam = null;
                        string BTWnummer = null;
                        string Telefoon = null;
                        string Email = null;
                        int IdAdres = 0;
                        string Land = null;
                        string Postcode = null;
                        string Plaats = null;
                        string Straat = null;
                        string nummer = null;
                        IdBedrijf = (int)reader["BedrijfsId"];
                        BedrijfNaam = (string)reader["Naam"];
                        BTWnummer = (string)reader["BTWnummer"];
                        Email = (string)reader["Email"];
                        bedrijf = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);
                        try {
                            Telefoon = (string)reader["Telefoon"];
                            bedrijf.ZetTelefoon(Telefoon);
                        } catch (Exception ex) { }
                        try {
                            IdAdres = (int)reader["AdresId"];
                            Land = (string)reader["Land"];
                            Postcode = (string)reader["Postcode"];
                            Plaats = (string)reader["Plaats"];
                            Straat = (string)reader["Straat"];
                            nummer = (string)reader["Nummer"];
                            bedrijf.ZetAdres(new Adres(IdAdres, Straat, nummer, Postcode, Plaats, Land));
                        } catch (Exception ex) { }
                    }
                    reader.Close();
                    return bedrijf;
                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("GeefBedrijfOpId", ex);
                } finally {
                    connectie.Close();
                }
            }
        } 
        public void UpdateBedrijfAdres(int id, int adresId) {
            SqlConnection conn = GetConnection();

            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();
                    string query = "update Bedrijf set AdresId=@adresid where BedrijfsId=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@adresid", adresId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new BedrijfRepoADOException("UpdateBedrijfAdres");
                } finally {
                    conn.Close();
                }
            }
        }
    }
}
