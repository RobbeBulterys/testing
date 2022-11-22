using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using Microsoft.IdentityModel.Xml;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.repoADO
{
    public class EmployeecontractRepoADO : IEmployeecontractRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public EmployeecontractRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        public bool ContractExists(int werknemerId, int bedrijfsId)
        {
            if (werknemerId <= 0 || bedrijfsId <= 0) { throw new EmployeecontractRepoADOException("BestaatContract - werknemer- of bedrijfsId niet ongeldig"); }

            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Employeecontract WHERE EmployeeId=@employeeId AND CompanyId=@companyId AND IsDeleted=0;";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", werknemerId);
                    cmd.Parameters.AddWithValue("@companyId", bedrijfsId);

                    return (int)(long)cmd.ExecuteScalar() > 0 ? true : false; // Nog eens ternary operator opfrissen :)

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("Bestaatcontract", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Employeecontract GetContract(Employee werknemer, Company bedrijf)
        {
            if (werknemer.PersonId <= 0 || bedrijf.Id <= 0) { throw new EmployeecontractRepoADOException("GeefContract - id's werknemer of bedrijf ongeldig"); }
            string query = "SELECT  EmployeeId, CompanyId, EmployeeFunction, Email FROM Employeecontract WHERE CompanyId=@companyId AND EmployeeId=@employeeId AND IsDeleted=0;";

            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", bedrijf.Id);
                    cmd.Parameters.AddWithValue("@employeeId", werknemer.PersonId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string functie = (string)reader["EmployeeFunction"];

                        Employeecontract contract = new Employeecontract(bedrijf, werknemer, functie);

                        if (!(reader["Email"] == DBNull.Value))
                        {
                            string email = (string)reader["Email"];
                            contract.SetEmail(email);
                        }

                        return contract;
                    }
                    return null; // komen we normaal niet, tenzij er geen contract is

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("GeefContractenVanBedrijf", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Employeecontract> GetCompanyContracts(Company bedrijf)
        {
            // controles
            if (bedrijf == null) { throw new EmployeecontractRepoADOException("GeefContractenVanBedrijf - bedrijf niet ingevuld"); }
            if (bedrijf.Id <= 0) { throw new EmployeecontractRepoADOException("GeefContractenVanBedrijf - bedrijfsId ongeldig"); }

            // nodig
            List<Employeecontract> contracten = new List<Employeecontract>();

            // query
            string query = "SELECT  ec.EmployeeId, ec.EmployeeFunction, ec.Email, e.LastName, e.FirstName FROM Employeecontract ec LEFT JOIN Employee e ON e.EmployeeId=ec.EmployeeId WHERE CompanyId=@companyId AND ec.IsDeleted=0;";

            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@companyId", bedrijf.Id);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int werknemerId = (int)reader["EmployeeId"];
                        string functie = (string)reader["EmployeeFunction"];
                        string naam = (string)reader["LastName"];
                        string voornaam = (string)reader["FirstName"];

                        Employee werker = new Employee(werknemerId, naam, voornaam);
                        Employeecontract contract = new Employeecontract(bedrijf, werker, functie);

                        if (!(reader["Email"] == DBNull.Value))
                        {
                            string email = (string)reader["Email"];
                            contract.SetEmail(email);
                        }

                        contracten.Add(contract);
                    }
                    return contracten;

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("GeefContractenVanBedrijf", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        } // Ready

        public IEnumerable<Employeecontract> GetEmployeeContracts(Employee werknemer)
        {
            // controles
            if (werknemer == null) { throw new EmployeecontractRepoADOException("GeefContractenVanWerknemer - werknemer niet ingevuld"); }
            if (werknemer.PersonId <= 0) { throw new EmployeecontractRepoADOException("GeefContractenVanWerknemer - werknemerId ongeldig"); }

            // nodig
            List<Employeecontract> contracten = new List<Employeecontract>();

            // query
            string query = "SELECT ec.CompanyId, ec.EmployeeFunction, ec.Email AS EmployeeEmail, c.CompanyName, c.VATnumber, c.Email AS CompanyEmail, c.Phone, a.AddressId, a.Country, a.PostalCode, a.City, a.Street, a.AddressNumber FROM Employeecontract ec LEFT JOIN Company c ON c.CompanyId=ec.CompanyId LEFT JOIN Address a ON c.AddressId=a.AddressId WHERE EmployeeId=@employeeId AND ec.IsDeleted=0;";

            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", werknemer.PersonId);

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // verplichte velden
                        int bedrijfsId = (int)reader["CompanyId"];
                        string functie = (string)reader["EmployeeFunction"];
                        string naam = (string)reader["CompanyName"];
                        string btwNummer = (string)reader["VATnumber"];
                        string emailBedrijf = (string)reader["CompanyEmail"];

                        Company bedrijf = new Company(bedrijfsId, naam, btwNummer, emailBedrijf);
                        Employeecontract contract = new Employeecontract(bedrijf, werknemer, functie);

                        // optionele gegevens (emailWerknemer contract, telefoon bedrijf en adres bedrijf)
                        if (reader["EmployeeEmail"] != DBNull.Value)
                        {
                            string email = (string)reader["EmployeeEmail"];
                            contract.SetEmail(email);
                        }
                        if (reader["Phone"] != DBNull.Value)
                        {
                            string telefoon = (string)reader["Phone"];
                            bedrijf.SetPhoneNumber(telefoon);
                        }
                        if (reader["AddressId"] != DBNull.Value)
                        {
                            int adresId = (int)reader["AddressId"];
                            string land = (string)reader["Country"];
                            string postcode = (string)reader["PostalCode"];
                            string plaats = (string)reader["City"];
                            string straat = (string)reader["Street"];
                            string nummer = (string)reader["AddressNumber"];
                            Address a = new Address(adresId, straat, nummer, postcode, plaats, land);
                            bedrijf.SetAddress(a);
                        }
                        contracten.Add(contract);
                    }
                    return contracten;

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("GeefContractenVanWerknemer", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        } //ready


        public void UpdateContract(int werknemerId, int bedrijfsId, string? functie, string? email)
        {
            if (werknemerId <= 0 || bedrijfsId <= 0) { throw new EmployeecontractRepoADOException("UpdateContract - werknemerid of bedrijfsid ongeldig"); }
            if (string.IsNullOrWhiteSpace(functie) && string.IsNullOrWhiteSpace(email)) { throw new EmployeecontractRepoADOException("UpdateContract - functie en email beiden niet ingevuld"); }

            // functie of email is dus ingevuld
            string query = "UPDATE Employeecontract SET ";

            MySqlConnection conn = GetConnection();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    // kijken wat er in de query bijkomt
                    if (!string.IsNullOrWhiteSpace(functie))
                    {
                        query += "EmployeeFunction=@function";
                        cmd.Parameters.AddWithValue("@function", functie);
                        cmd.Parameters.Add("@function", MySqlDbType.VarChar);
                    }

                    if (!string.IsNullOrWhiteSpace(email) && query != "UPDATE Employeecontract SET ") { query += ", "; } // Als er al eerder een parameter (functie) is ingevuld, dan voegen we een ", " spatie toe voor meerdere updates
                    if (!string.IsNullOrWhiteSpace(email))
                    {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }


                    query += " WHERE IsDeleted=0 AND EmployeeId=@employeeId AND CompanyId=@companyId";

                    cmd.Parameters.AddWithValue("@employeeId", werknemerId);
                    cmd.Parameters.AddWithValue("@companyId", bedrijfsId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("UpdateContract", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteContract(int werknemerId, int bedrijfsId)
        {

            // query
            string sql = "UPDATE Employeecontract SET IsDeleted=1, ActiveWorkingAtCompany=0 WHERE EmployeeId=@employeeId AND CompanyId=@companyId;";

            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@employeeId", werknemerId);
                    cmd.Parameters.AddWithValue("@companyId", bedrijfsId);

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("VerwijderContract", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        } // Ready


        public void AddContract(Employeecontract contract)
        {
            // controles
            if (contract == null) { throw new EmployeecontractRepoADOException("VoegContractToe - geen contract ingevuld"); }

            // query
            string sql = "INSERT INTO Employeecontract (EmployeeId, CompanyId, EmployeeFunction, Email) VALUES (@employeeId, @companyId, @function, @email);";

            MySqlConnection connection = GetConnection();
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@employeeId", contract.Employee.PersonId);
                    cmd.Parameters.AddWithValue("@companyId", contract.Company.Id);
                    cmd.Parameters.AddWithValue("@function", contract.Function);

                    // Optionele parameters
                    if (contract.Email != null)
                    {
                        cmd.Parameters.AddWithValue("@email", contract.Email);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new EmployeecontractRepoADOException("VoegContractToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        } // Ready

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

    }
}
