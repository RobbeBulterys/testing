using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.repoADO
{
    public class BedrijfRepoADO : IBedrijfRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public BedrijfRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool BestaatBedrijfZonderId(string btwnummer, string naam, string email)
        {
            MySqlConnection conn = GetConnection();
            string query = @$"SELECT COUNT(*) FROM {dbname}.Company WHERE VATnumber = @VATnumber AND CompanyName = @name AND Email = @email AND IsDeleted = 0";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@VATnumber", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));

                    cmd.Parameters["@VATnumber"].Value = btwnummer;
                    cmd.Parameters["@name"].Value = naam;
                    cmd.Parameters["@email"].Value = email;

                    int n = (int)(long)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("BestaatBedrijfZonderId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public bool BestaatBedrijfMetId(int id)
        {
            MySqlConnection conn = GetConnection();
            string query = @$"SELECT COUNT(*) FROM {dbname}.Company WHERE "
            + "CompanyId = @id AND IsDeleted = 0";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));

                    cmd.Parameters["@id"].Value = id;

                    int n = (int)(long)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("BestaatBedrijfMetId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void UpdateBedrijf(int id, string? btwnummer, string? naam, string? email, string? telefoon)
        {
            MySqlConnection conn = GetConnection();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    string query = "UPDATE Company SET ";


                    if (!string.IsNullOrWhiteSpace(btwnummer))
                    {
                        query += "VATnumber=@VATnumber";
                        cmd.Parameters.AddWithValue("@VATnumber", btwnummer);
                    }
                    if (!string.IsNullOrWhiteSpace(naam) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(naam))
                    {
                        query += "CompanyName=@name";
                        cmd.Parameters.AddWithValue("@name", naam);
                    }
                    if (!string.IsNullOrWhiteSpace(email) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrWhiteSpace(telefoon) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(telefoon))
                    {
                        query += "Phone=@phone";
                        cmd.Parameters.AddWithValue("@phone", telefoon);
                    }

                    query += " WHERE CompanyId=@id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("UpdateBedrijf");
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public void VoegBedrijfToe(string btwnummer, string naam, string email, string? telefoon, int? adresId)
        {
            MySqlConnection conn = GetConnection();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    string query = @$"INSERT INTO {dbname}.Company (VATnumber,CompanyName,Email";
                    if (!string.IsNullOrWhiteSpace(telefoon)) query += ",Phone";
                    if (adresId.HasValue) query += ",AddressId";
                    query += ") VALUES (@VATnumber,@name,@email";
                    if (!string.IsNullOrWhiteSpace(telefoon))
                    {
                        query += ",@phone";
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar));
                        cmd.Parameters["@phone"].Value = telefoon;

                    }
                    if (adresId.HasValue)
                    {
                        query += ",@addressId";
                        cmd.Parameters.Add(new MySqlParameter("@addressId", MySqlDbType.Int32));
                        cmd.Parameters["@addressId"].Value = adresId;
                    }
                    query += ");";


                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@VATnumber", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));


                    cmd.Parameters["@VATnumber"].Value = btwnummer;
                    cmd.Parameters["@name"].Value = naam;
                    cmd.Parameters["@email"].Value = email;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("VoegBedrijfToe");
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public void VerwijderBedrijf(int id)
        {
            MySqlConnection conn = GetConnection();
            string query = @"UPDATE Company SET IsDeleted = 1 WHERE CompanyId = @companyId";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@companyId", MySqlDbType.Int32));

                    cmd.Parameters["@companyId"].Value = id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("VerwijderBedrijf", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
        public List<Bedrijf> GeefBedrijven()
        {
            MySqlConnection connectie = GetConnection();
            List<Bedrijf> bedrijven = new List<Bedrijf>();
            string query = "SELECT c1.CompanyId, c1.VATnumber, c1.CompanyName, c1.Phone, c1.Email, a1.AddressId, a1.Country, a1.PostalCode, a1.City, a1.Street, a1.AddressNumber FROM Company c1 LEFT JOIN Address a1 on a1.AddressId = c1.AddressId WHERE c1.IsDeleted = 0" ;
            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
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
                        IdBedrijf = (int)reader["CompanyId"];
                        BedrijfNaam = (string)reader["CompanyName"];
                        BTWnummer = (string)reader["VATnumber"];
                        Email = (string)reader["Email"];
                        Bedrijf b = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);
                        try
                        {
                            Telefoon = (string)reader["Phone"];
                            b.ZetTelefoon(Telefoon);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            IdAdres = (int)reader["AddressId"];
                            Land = (string)reader["Country"];
                            Postcode = (string)reader["PostalCode"];
                            Plaats = (string)reader["City"];
                            Straat = (string)reader["Street"];
                            nummer = (string)reader["AddressNumber"];
                            b.ZetAdres(new Adres(IdAdres, Straat, nummer, Postcode, Plaats, Land));
                        }
                        catch (Exception ex) { }
                        bedrijven.Add(b);
                    }
                    reader.Close();
                    return bedrijven;
                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("GeefBezoekers", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }
        public List<Bedrijf> ZoekBedrijven(string? btwnummer, string? naam, string? email, string? telefoon)
        {
            MySqlConnection connectie = GetConnection();
            string query;
            string columns = "c.*, a.Country, a.City, a.PostalCode, a.Street, a.AddressNumber";
            List<Bedrijf> bedrijven = new List<Bedrijf>();

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(btwnummer) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(telefoon)) query = $"SELECT {columns} FROM Company c LEFT JOIN {dbname}.Address a ON c.AddressId=a.AddressId WHERE c.IsDeleted=0";
                    else
                    {
                        query = $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ";
                        if (!string.IsNullOrWhiteSpace(naam))
                        {
                            query += "CompanyName = @name";
                            cmd.Parameters.AddWithValue("@name", naam);
                        }
                        if (!string.IsNullOrWhiteSpace(btwnummer) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(btwnummer))
                        {
                            query += "VATnumber = @vatnumber";
                            cmd.Parameters.AddWithValue("@vatnumber", btwnummer);
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email))
                        {
                            query += "Email = @email";
                            cmd.Parameters.AddWithValue("@email", email);
                        }
                        if (!string.IsNullOrWhiteSpace(telefoon) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(telefoon))
                        {
                            query += "Phone = @phone";
                            cmd.Parameters.AddWithValue("@phone", telefoon);
                        }
                        query += " AND c.IsDeleted = 0";
                    }


                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int IdBedrijf = (int)reader["CompanyId"];
                        string BedrijfNaam = (string)reader["CompanyName"];
                        string BTWnummer = (string)reader["VATnumber"];
                        string Email = (string)reader["Email"];
                        Bedrijf b = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);

                        if (reader["Phone"] != DBNull.Value)
                        {
                            string Telefoon = (string)reader["Phone"];
                            b.ZetTelefoon(Telefoon);
                        }

                        // Hebben we wel een adres?
                        if (reader["AddressId"] != DBNull.Value)
                        {
                            int adresId = (int)reader["AddressId"];
                            string land = (string)reader["Country"];
                            string postcode = (string)reader["PostalCode"];
                            string plaats = (string)reader["City"];
                            string straat = (string)reader["Street"];
                            string nummer = (string)reader["AddressNumber"];
                            Adres a = new Adres(adresId, straat, nummer, postcode, plaats, land);
                            b.ZetAdres(a);
                        }

                        bedrijven.Add(b);
                    }
                    reader.Close();
                    return bedrijven;
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
        public Bedrijf GeefBedrijfOpId(int id)
        {
            MySqlConnection connectie = GetConnection();
            Bedrijf bedrijf = null;
            string query = "SELECT c1.CompanyId, c1.VATnumber, c1.CompanyName, c1.Phone, c1.Email, " +
                "a1.AddressId, a1.Country, a1.PostalCode, a1.City, a1.Street, a1.AddressNumber " +
                "FROM Company c1 LEFT JOIN Address a1 on a1.AddressId = c1.AddressId " +
                "WHERE CompanyId = @companyId AND c1.IsDeleted = 0";

            using (MySqlCommand cmd = connectie.CreateCommand())
            {
                try
                {
                    connectie.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", id);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
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
                        IdBedrijf = (int)reader["CompanyId"];
                        BedrijfNaam = (string)reader["CompanyName"];
                        BTWnummer = (string)reader["VATnumber"];
                        Email = (string)reader["Email"];
                        bedrijf = new Bedrijf(IdBedrijf, BedrijfNaam, BTWnummer, Email);
                        try
                        {
                            Telefoon = (string)reader["Phone"];
                            bedrijf.ZetTelefoon(Telefoon);
                        }
                        catch (Exception ex) { }
                        try
                        {
                            IdAdres = (int)reader["AddressId"];
                            Land = (string)reader["Country"];
                            Postcode = (string)reader["PostalCode"];
                            Plaats = (string)reader["City"];
                            Straat = (string)reader["Street"];
                            nummer = (string)reader["AddressNumber"];
                            bedrijf.ZetAdres(new Adres(IdAdres, Straat, nummer, Postcode, Plaats, Land));
                        }
                        catch (Exception ex) { }
                    }
                    reader.Close();
                    return bedrijf;
                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("GeefBedrijfOpId", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }
        }
        public void UpdateBedrijfAdres(int id, int adresId)
        {
            MySqlConnection conn = GetConnection();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Company SET AddressId=@addressId WHERE CompanyId=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@addressId", adresId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("UpdateBedrijfAdres");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool BedrijvenOpAdresAanwezig(int adresId)
        {
            MySqlConnection conn = GetConnection();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Company WHERE AddressId=@addressId";
                    cmd.Parameters.AddWithValue("@addressid", adresId);
                    cmd.CommandText = query;
                    bool bedrijvenOpAdresAanwezig = false;
                    bedrijvenOpAdresAanwezig = (int)(long)cmd.ExecuteScalar() > 0 ? true : false;
                    return bedrijvenOpAdresAanwezig;

                }
                catch (Exception ex)
                {
                    throw new BedrijfRepoADOException("UpdateBedrijfAdres");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
