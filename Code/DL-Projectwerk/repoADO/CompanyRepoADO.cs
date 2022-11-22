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

namespace DL_Projectwerk.repoADO {
    public class CompanyRepoADO : ICompanyRepository {
        private string connectionString;

        public CompanyRepoADO(string connectionString) {
            this.connectionString = connectionString;
        }
        private MySqlConnection GetConnection() {
            return new MySqlConnection(connectionString);
        }

        public bool CompanyExistsWithoutId(string vatnumber, string name, string email) {
            MySqlConnection connection = GetConnection();
            string query = @$"SELECT COUNT(*) FROM Company WHERE VATnumber = @VATnumber AND CompanyName = @name AND Email = @email AND IsDeleted = 0";
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@VATnumber", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));

                    cmd.Parameters["@VATnumber"].Value = vatnumber;
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters["@email"].Value = email;

                    int n = (int)(long)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("BestaatBedrijfZonderId", ex);
                } finally {
                    connection.Close();
                }
            }

        } // todo controleren
        public bool CompanyExistsWithId(int id) {
            MySqlConnection connection = GetConnection();
            string query = @$"SELECT COUNT(*) FROM Company WHERE "
            + "CompanyId = @id AND IsDeleted = 0";
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@id", MySqlDbType.Int32));

                    cmd.Parameters["@id"].Value = id;

                    int n = (int)(long)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("BestaatBedrijfMetId", ex);
                } finally {
                    connection.Close();
                }
            }
        } // kept working
        public void UpdateCompany(int id, string? vatnumber, string? name, string? email, string? phonenumber) {
            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();

                    string query = "UPDATE Company SET ";


                    if (!string.IsNullOrWhiteSpace(vatnumber)) {
                        query += "VATnumber=@VATnumber";
                        cmd.Parameters.AddWithValue("@VATnumber", vatnumber);
                    }
                    if (!string.IsNullOrWhiteSpace(name) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(name)) {
                        query += "CompanyName=@name";
                        cmd.Parameters.AddWithValue("@name", name);
                    }
                    if (!string.IsNullOrWhiteSpace(email) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(email)) {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }
                    if (!string.IsNullOrWhiteSpace(phonenumber) && query != "UPDATE Company SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(phonenumber)) {
                        query += "Phone=@phone";
                        cmd.Parameters.AddWithValue("@phone", phonenumber);
                    }

                    query += " WHERE CompanyId=@id";

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new CompanyRepoADOException("UpdateBedrijf");
                } finally {
                    connection.Close();
                }
            }

        } // todo controleren
        public void AddCompany(Company company) {
            try {
                if (company.Address != null) {
                    AddCompanyWithAddress(company.VATNumber, company.Name, company.Email, company.PhoneNumber, company.Address.Country, company.Address.Postalcode, company.Address.Place, company.Address.Street, company.Address.Number);
                } else {
                    AddCompanyWithoutAddress(company.VATNumber, company.Name, company.Email, company.PhoneNumber);
                }
            } catch (Exception ex) {
                throw new CompanyRepoADOException("AddCompany", ex);
            }


        } // updated & tested
        private void AddCompanyWithoutAddress(string vatnumber, string name, string email, string? phonenumber) {
            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    string query = @$"INSERT INTO Company (VATnumber,CompanyName,Email";
                    if (!string.IsNullOrWhiteSpace(phonenumber)) {
                        query += ",Phone";
                        cmd.Parameters.Add(new MySqlParameter("@phone", MySqlDbType.VarChar));
                        cmd.Parameters["@phone"].Value = phonenumber;
                    }
                    query += ") VALUES (@VATnumber,@name,@email";
                    if (!string.IsNullOrWhiteSpace(phonenumber)) query += ",@phone";
                    query += ");";


                    connection.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@VATnumber", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));


                    cmd.Parameters["@VATnumber"].Value = vatnumber;
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters["@email"].Value = email;

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("AddCompanyWithoutAddress", ex);
                } finally {
                    connection.Close();
                }
            }
        } // new to simplify code
        private void AddCompanyWithAddress(string vatnumber, string name, string email, string? phonenumber, string country, string postalCode, string place, string street, string addressNumber) {
            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    string query = @$"INSERT INTO Company (VATnumber,CompanyName,Email";
                    if (!string.IsNullOrWhiteSpace(phonenumber)) query += ",Phone";
                    query += ",Country,PostalCode,Place,Street,AddressNumber";
                    query += ") VALUES (@VATnumber,@name,@email";
                    if (!string.IsNullOrWhiteSpace(phonenumber)) {
                        query += ",@phone";
                        cmd.Parameters.AddWithValue("@phone", MySqlDbType.VarChar);
                        cmd.Parameters["@phone"].Value = phonenumber;

                    }
                    query += ",@country,@postalcode,@place,@street,@addressnumber";
                    cmd.Parameters.AddWithValue("@country", MySqlDbType.VarChar);
                    cmd.Parameters.AddWithValue("@postalcode", MySqlDbType.VarChar);
                    cmd.Parameters.AddWithValue("@place", MySqlDbType.VarChar);
                    cmd.Parameters.AddWithValue("@street", MySqlDbType.VarChar);
                    cmd.Parameters.AddWithValue("@addressnumber", MySqlDbType.VarChar);

                    query += ");";


                    connection.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@VATnumber", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@name", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@email", MySqlDbType.VarChar));


                    cmd.Parameters["@VATnumber"].Value = vatnumber;
                    cmd.Parameters["@name"].Value = name;
                    cmd.Parameters["@email"].Value = email;

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("AddCompanyWithAddress", ex);
                } finally {
                    connection.Close();
                }
            }
        } // new to simplify code

        public void DeleteCompany(int id) {
            MySqlConnection connection = GetConnection();
            string query = @"UPDATE Company SET IsDeleted = 1 WHERE CompanyId = @companyId";
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@companyId", MySqlDbType.Int32));

                    cmd.Parameters["@companyId"].Value = id;

                    cmd.ExecuteNonQuery();
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("VerwijderBedrijf", ex);
                } finally {
                    connection.Close();
                }
            }

        } // todo controleren
        public List<Company> GetCompanies() {
            MySqlConnection connection = GetConnection();
            List<Company> companies = new List<Company>();
            string query = "SELECT CompanyId, VATnumber, CompanyName, Phone, Email, Country, PostalCode, Place, Street, AddressNumber FROM Company WHERE IsDeleted = 0";
            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int CompanyID = (int)reader["CompanyId"];
                        string CompanyName = (string)reader["CompanyName"];
                        string VATNumber = (string)reader["VATnumber"];
                        string Email = (string)reader["Email"];
                        Company b = new Company(CompanyID, CompanyName, VATNumber, Email);
                        try {
                            string PhoneNumber = (string)reader["Phone"];
                            b.SetPhoneNumber(PhoneNumber);
                        } catch (Exception ex) { }
                        try {
                            int AddressID = (int)reader["AddressId"];
                            string Country = (string)reader["Country"];
                            string PostalCode = (string)reader["PostalCode"];
                            string Place = (string)reader["City"];
                            string Street = (string)reader["Street"];
                            string Number = (string)reader["AddressNumber"];
                            b.SetAddress(new Address(AddressID, Street, Number, PostalCode, Place, Country));
                        } catch (Exception ex) { }
                        companies.Add(b);
                    }
                    reader.Close();
                    return companies;
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("GeefBezoekers", ex);
                } finally {
                    connection.Close();
                }
            }
        } // querry aangepast
        public List<Company> SearchCompanies(string? vatnumber, string? name, string? email, string? phonenumber) {
            MySqlConnection connection = GetConnection();
            string query;
            string columns = "c.*, a.Country, a.City, a.PostalCode, a.Street, a.AddressNumber";
            List<Company> companies = new List<Company>();

            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(vatnumber) && string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(phonenumber)) query = $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE c.IsDeleted=0";
                    else {
                        query = $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ";
                        if (!string.IsNullOrWhiteSpace(name)) {
                            query += "CompanyName = @name";
                            cmd.Parameters.AddWithValue("@name", "%"+name+"%");
                        }
                        if (!string.IsNullOrWhiteSpace(vatnumber) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(vatnumber)) {
                            query += "VATnumber = @vatnumber";
                            cmd.Parameters.AddWithValue("@vatnumber", "%"+vatnumber+"%");
                        }
                        if (!string.IsNullOrWhiteSpace(email) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(email)) {
                            query += "Email = @email";
                            cmd.Parameters.AddWithValue("@email", "%"+ email +"%");
                        }
                        if (!string.IsNullOrWhiteSpace(phonenumber) && query != $"SELECT {columns} FROM Company c LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(phonenumber)) {
                            query += "Phone = @phone";
                            cmd.Parameters.AddWithValue("@phone", "%"+phonenumber+"%");
                        }
                        query += " AND c.IsDeleted = 0";
                    }


                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int CompanyID = (int)reader["CompanyId"];
                        string Name = (string)reader["CompanyName"];
                        string VATNumber = (string)reader["VATnumber"];
                        string Email = (string)reader["Email"];
                        Company b = new Company(CompanyID, Name, VATNumber, Email);

                        if (reader["Phone"] != DBNull.Value) {
                            string Telefoon = (string)reader["Phone"];
                            b.SetPhoneNumber(Telefoon);
                        }

                        // Hebben we wel een adres?
                        if (reader["AddressId"] != DBNull.Value) {
                            int AddressId = (int)reader["AddressId"];
                            string Country = (string)reader["Country"];
                            string PostalCode = (string)reader["PostalCode"];
                            string Place = (string)reader["City"];
                            string Street = (string)reader["Street"];
                            string Number = (string)reader["AddressNumber"];
                            Address a = new Address(AddressId, Street, Number, PostalCode, Place, Country);
                            b.SetAddress(a);
                        }

                        companies.Add(b);
                    }
                    reader.Close();
                    return companies;
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("GeefBezoekers", ex);
                } finally {
                    connection.Close();
                }
            }
        } // todo controleren
        public Company GetCompanyOnId(int id) {
            MySqlConnection connection = GetConnection();
            Company company = null;
            string query = "SELECT * FROM Company WHERE CompanyId=@companyid";

            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", id);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {

                        int CompanyID = (int)reader["CompanyId"];
                        string Name = (string)reader["CompanyName"];
                        string VATNumber = (string)reader["VATnumber"];
                        string Email = (string)reader["Email"];
                        company = new Company(CompanyID, Name, VATNumber, Email);
                        try {
                            string Telefoon = (string)reader["Phone"];
                            company.SetPhoneNumber(Telefoon);
                        } catch (Exception ex) { }
                        try {
                            int AdressID = (int)reader["AddressId"];
                            string Country = (string)reader["Country"];
                            string PostalCode = (string)reader["PostalCode"];
                            string Place = (string)reader["City"];
                            string Street = (string)reader["Street"];
                            string Number = (string)reader["AddressNumber"];
                            company.SetAddress(new Address(AdressID, Street, Number, PostalCode, Place, Country));
                        } catch (Exception ex) { }
                    }
                    reader.Close();
                    return company;
                } catch (Exception ex) {
                    throw new CompanyRepoADOException("GeefBedrijfOpId", ex);
                } finally {
                    connection.Close();
                }
            }
        } // upddated
        public void UpdateCompanyAddress(int id, int addressId) {
            MySqlConnection connection = GetConnection();

            using (MySqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    string query = "UPDATE Company SET AddressId=@addressId WHERE CompanyId=@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new CompanyRepoADOException("UpdateBedrijfAdres");
                } finally {
                    connection.Close();
                }
            }
        } // todo controleren
    }
}
