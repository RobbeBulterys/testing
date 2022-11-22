using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using MySqlConnector;
using System.Data;
using System.Data.SqlClient;

namespace DL_Projectwerk.repoADO
{
    public class VisitRepoADO : IVisitRepository
    {
        private VisitorRepoADO bezoekerRepoADO;
        private CompanyRepoADO CompanyRepoADO;
        private EmployeeRepoADO werknemerRepoADO;
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public VisitRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }
        private Visitor GetVisitor(int persoonId)
        {
            MySqlConnection connectie = GetConnection();
            Visitor bezoeker = null;
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
                        bezoeker = new Visitor(persoonId, Name, Voornaam, Email, Bedrijf);
                    }
                    reader.Close();
                    return bezoeker;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("GeefBezoeker", ex);
                }
                finally
                {
                    connectie.Close();
                }
            }

        }

        public Employee GetEmployee(int personId)
        {
            MySqlConnection connection = GetConnection();
            Employee employee = null;
            string query = "SELECT * FROM Employee WHERE EmployeeId = @employeeId";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@employeeId", personId);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int EmployeeID = (int)reader["EmployeeId"];
                        string LastName = (string)reader["LastName"];
                        string FirstName = (string)reader["FirstName"];
                        //string Nummerplaat = (string)reader["Nummerplaat"];
                        employee = new Employee(EmployeeID, LastName, FirstName);
                    }
                    reader.Close();
                    return employee;
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("GeefBezoeker", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool VisitExists(Visit visit)
        {
            if (visit is null) { throw new VisitRepoADOException("BezoekRepoADO - VoegBezoekToe - bezoek is null"); }

            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Visit WHERE IsDeleted=0 AND VisitorFirstName=@visitorFirstName AND VisitorLastName=@visitorLastName AND VisitorEmail=@visitorEmail AND VisitorCompany=@visitorCompany AND VisitedCompany=@visitedCompany AND ContactPerson=@contactPerson AND RegisterTime=@registerTime AND IsDeleted = 0";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@visitedCompany", visit.Company.Id);
                    cmd.Parameters.AddWithValue("@contactPerson", visit.Contact.PersonId);
                    cmd.Parameters.AddWithValue("@registerTime", visit.StartingTime);
                    cmd.Parameters.AddWithValue("@visitorFirstName", visit.Visitor.FirstName);
                    cmd.Parameters.AddWithValue("@visitorLastName", visit.Visitor.LastName);
                    cmd.Parameters.AddWithValue("@visitorEmail", visit.Visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visit.Visitor.Company);
                    cmd.CommandText = query;
                    int i = (int)(long)cmd.ExecuteScalar();
                    if (i > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("BestaatBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Visit GetVisit(int visitid)
        {
            MySqlConnection connection = GetConnection();
            Visit bezoek = null;
            string query = "SELECT * FROM Visit WHERE VisitId=@visitId AND IsDeleted = 0";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    bezoekerRepoADO = new VisitorRepoADO(connectieString);
                    CompanyRepoADO = new CompanyRepoADO(connectieString);
                    werknemerRepoADO = new EmployeeRepoADO(connectieString);
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitId", visitid);
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        
                        string FirstNameVisitor = (string)reader["VisitorFirstName"];
                        string LastNameVisitor = (string)reader["VisitorLastName"];
                        string EmailVisitor = (string)reader["VisitorEmail"];
                        string CompanyVisitor = (string)reader["VisitorCompany"];
                        Visitor Visitor = new Visitor(LastNameVisitor, FirstNameVisitor, EmailVisitor, CompanyVisitor);
                        Company Bedrijf = CompanyRepoADO.GetCompanyOnId((int)reader["VisitedCompany"]);
                        Employee ContactPerson = werknemerRepoADO.GetEmployee((int)reader["ContactPerson"]);
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        //DateTime Eindtijd = (DateTime)reader["UnRegistrationTime"];
                        bezoek = new Visit(visitid, Visitor, Bedrijf, ContactPerson, Starttijd);
                    }
                    reader.Close();
                    return bezoek;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("GeefBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Visit> GetVisits()
        {
            MySqlConnection connection = GetConnection();
            List<Visit> visits = new List<Visit>();
            string query = "SELECT v1.VisitId, v1.RegisterTime, v1.UnregistrationTime, v1.VisitorLastName as visitorLastName, v1.VisitorFirstName as visitorFirstName, v1.VisitorCompany as visitorCompany, v1.VisitorEmail as visitorEmail, e1.EmployeeId, e1.LastName as employeeLastName, e1.FirstName as employeeFirstName, c1.CompanyId, c1.VATnumber, c1.CompanyName, c1.Phone, c1.Email as companyEmail, c1.Country, c1.PostalCode, c1.Place, c1.Street, c1.AddressNumber " +
                "FROM Visit v1 " +
                "LEFT JOIN Employee e1 on e1.EmployeeId = v1.ContactPerson " +
                "LEFT JOIN Company c1 on c1.CompanyId = v1.VisitedCompany";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitId"];
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd;
                        if (reader["UnregistrationTime"] != DBNull.Value) { Eindtijd = (DateTime)reader["UnregistrationTime"]; }
                        else { Eindtijd = Starttijd; }
                        Visitor visitor = new Visitor((string)reader["visitorLastName"], (string)reader["visitorFirstName"], (string)reader["visitorEmail"], (string)reader["visitorCompany"]);
                        Employee employee = new Employee((int)reader["EmployeeId"], (string)reader["employeeLastName"], (string)reader["employeeFirstName"]);
                        int CompanyID = (int)reader["CompanyId"];
                        string CompanyName = (string)reader["CompanyName"];
                        string VATNumber = (string)reader["VATnumber"];
                        string Email = (string)reader["companyEmail"];
                        Company company = new Company(CompanyID, CompanyName, VATNumber, Email);
                        if (reader["Phone"] != DBNull.Value) { company.SetPhoneNumber((string)reader["Phone"]); }
                        if (reader["Country"] != DBNull.Value)
                        {
                            string Country = (string)reader["Country"];
                            string PostalCode = (string)reader["PostalCode"];
                            string Place = (string)reader["Place"];
                            string Street = (string)reader["Street"];
                            string Number = (string)reader["AddressNumber"];
                            company.SetAddress(new Address(Street, Number, PostalCode, Place, Country));
                        }
                        visits.Add(new Visit(Id, visitor, company, employee, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return visits;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public List<Visit> GetVisits(Visitor? bezoeker, Company? bedrijf, Employee? contactpersoon, string? StartTijd)
        {
            MySqlConnection connection = GetConnection();
            string query;
            List<Visit> bezoeken = new List<Visit>();

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
                            cmd.Parameters.AddWithValue("@visitor", bezoeker.PersonId);
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
                            cmd.Parameters.AddWithValue("@contactPerson", contactpersoon.PersonId);
                        }
                        if (StartTijd != null && query != "SELECT * FROM Visit WHERE ") query += " AND ";
                        if (StartTijd != null)
                        {
                            query += "RegisterTime>@registerTime";
                            cmd.Parameters.AddWithValue("@registerTime", StartTijd);
                        }
                        query += " AND IsDeleted = 0";
                    }

                    bezoekerRepoADO = new VisitorRepoADO(connectieString);
                    CompanyRepoADO = new CompanyRepoADO(connectieString);
                    werknemerRepoADO = new EmployeeRepoADO(connectieString);
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitId"];
                        Visitor Bezoeker = GetVisitor((int)reader["Visitor"]);
                        Company Bedrijf = CompanyRepoADO.GetCompanyOnId((int)reader["VisitedCompany"]);
                        Employee Contactpersoon = GetEmployee((int)reader["ContactPerson"]);
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd;
                        if (reader["UnregistrationTime"] != DBNull.Value) { Eindtijd = (DateTime)reader["UnregistrationTime"]; }
                        else { Eindtijd = Starttijd; }
                        bezoeken.Add(new Visit(Id, Bezoeker, Bedrijf, Contactpersoon, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return bezoeken;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateVisit(Visit visit)
        {
            MySqlConnection connection = GetConnection();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Visit SET VisitedCompany=@visitedCompany, ContactPerson=@contactperson, ContactPersonEmail=@contactpersonemail, VisitorFirstName=@visitorfirstname,VisitorLastName=@visitorlastname, VisitorEmail=@visitoremail, VisitorCompany=@visitorcompany WHERE VisitId=@visitId";
                    cmd.Parameters.AddWithValue("@visitId", visit.VisitId);
                    cmd.Parameters.AddWithValue("@visitedCompany", visit.Company.Id);
                    cmd.Parameters.AddWithValue("@contactperson", visit.Contact.PersonId);
                    cmd.Parameters.AddWithValue("@contactpersonemail", visit.Contact.Email);
                    cmd.Parameters.AddWithValue("@visitorfirstname", visit.Visitor.FirstName);
                    cmd.Parameters.AddWithValue("@visitorlastname", visit.Visitor.LastName);
                    cmd.Parameters.AddWithValue("@visitoremail", visit.Visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorcompany", visit.Visitor.Company);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("UpdateBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void DeleteVisit(Visit visit)
        {
            MySqlConnection connection = GetConnection();
            string query = "UPDATE Visitor SET IsDeleted = 1 WHERE VisitId=@VisitId";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@visitId", visit.VisitId);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("VerwijderBezoek", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void AddVisit(Visit visit)
        {
            MySqlConnection connection = GetConnection();
            string query = "INSERT INTO Visit (VisitedCompany, ContactPerson, RegisterTime, VisitorFirstName, VisitorLastName, VisitorEmail, VisitorCompany) VALUES (@visitedCompany, @contactPerson, @registerTime, @visitorFirstName, @visitorLastName, @visitorEmail, @visitorCompany); SELECT LAST_INSERT_ID();";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@visitedCompany", visit.Company.Id);
                    cmd.Parameters.AddWithValue("@contactPerson", visit.Contact.PersonId);
                    cmd.Parameters.AddWithValue("@registerTime", visit.StartingTime);
                    cmd.Parameters.AddWithValue("@visitorFirstName", visit.Visitor.FirstName);
                    cmd.Parameters.AddWithValue("@visitorLastName", visit.Visitor.LastName);
                    cmd.Parameters.AddWithValue("@visitorEmail", visit.Visitor.Email);
                    cmd.Parameters.AddWithValue("@visitorCompany", visit.Visitor.Company);
                    cmd.CommandText = query;
                    visit.SetId((int)(long)(ulong)cmd.ExecuteNonQuery());
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("VoegBezoekToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public void LogoutVisit(string email)
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
                    throw new VisitorRepoADOException("LogoutBezoek", ex);
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

        public List<Visit> GetSpecificVisits(string startingtime, string endingtime)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT v1.VisitId, v1.RegisterTime, v1.UnregistrationTime, v1.VisitorLastName as visitorLastName, v1.VisitorFirstName as visitorFirstName, v1.VisitorCompany as visitorCompany, v1.VisitorEmail as visitorEmail, e1.EmployeeId, e1.LastName as employeeLastName, e1.FirstName as employeeFirstName, c1.CompanyId, c1.VATnumber, c1.CompanyName, c1.Phone, c1.Email as companyEmail, c1.Country, c1.PostalCode, c1.Place, c1.Street, c1.AddressNumber " +
                "FROM Visit v1 " +
                "LEFT JOIN Employee e1 on e1.EmployeeId = v1.ContactPerson " +
                "LEFT JOIN Company c1 on c1.CompanyId = v1.VisitedCompany " +
                "WHERE RegisterTime>@startingtime AND RegisterTime<@endingtime";
            List<Visit> visits = new List<Visit>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@startingtime", startingtime);
                    cmd.Parameters.AddWithValue("@endingtime", endingtime);
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int Id = (int)reader["VisitId"];
                        DateTime Starttijd = (DateTime)reader["RegisterTime"];
                        DateTime Eindtijd;
                        if (reader["UnregistrationTime"] != DBNull.Value) { Eindtijd = (DateTime)reader["UnregistrationTime"]; }
                        else { Eindtijd = Starttijd; }
                        Visitor visitor = new Visitor((string)reader["visitorLastName"], (string)reader["visitorFirstName"], (string)reader["visitorEmail"], (string)reader["visitorCompany"]);
                        Employee employee = new Employee((int)reader["EmployeeId"], (string)reader["employeeLastName"], (string)reader["employeeFirstName"]);
                        int CompanyID = (int)reader["CompanyId"];
                        string CompanyName = (string)reader["CompanyName"];
                        string VATNumber = (string)reader["VATnumber"];
                        string Email = (string)reader["companyEmail"];
                        Company company = new Company(CompanyID, CompanyName, VATNumber, Email);
                        if (reader["Phone"] != DBNull.Value) { company.SetPhoneNumber((string)reader["Phone"]); }
                        if (reader["Country"] != DBNull.Value)
                        {
                            string Country = (string)reader["Country"];
                            string PostalCode = (string)reader["PostalCode"];
                            string Place = (string)reader["Place"];
                            string Street = (string)reader["Street"];
                            string Number = (string)reader["AddressNumber"];
                            company.SetAddress(new Address(Street, Number, PostalCode, Place, Country));
                        }
                        visits.Add(new Visit(Id, visitor, company, employee, Starttijd, Eindtijd));
                    }
                    reader.Close();
                    return visits;
                }
                catch (Exception ex)
                {
                    throw new VisitRepoADOException("GeefBezoeken", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
