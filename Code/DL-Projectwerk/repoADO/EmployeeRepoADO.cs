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
    public class EmployeeRepoADO : IEmployeeRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";

        public EmployeeRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool EmployeeExists(int employeeid)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Employee WHERE EmployeeId=@employeeId AND IsDeleted=0";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add("@employeeId", MySqlDbType.Int32);
                    cmd.Parameters["@employeeId"].Value = employeeid;
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
                    throw new EmployeeRepoADOException("BestaatWerknemer", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool EmployeeExists(string lastname, string firstname)
        {
            MySqlConnection conn = GetConnection();
            string query = "SELECT COUNT(*) FROM Employee WHERE LastName=@lastname AND FirstName=@firstname AND IsDeleted=0;";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@lastname", lastname);
                    cmd.Parameters.AddWithValue("@firstname", firstname);

                    if ((int)(long)cmd.ExecuteScalar() > 0) return true;
                    return false;
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("BestaatWerknemer", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void AddEmployee(Employee employee)
        {
            MySqlConnection connection = GetConnection();
            string sql = "INSERT INTO Employee (LastName, FirstName) VALUES (@lastname, @firstname); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@lastname", employee.LastName);
                    cmd.Parameters.AddWithValue("@firstname", employee.FirstName);
                    int werknemerId = (int)(long)(ulong)cmd.ExecuteScalar();
                    employee.SetId(werknemerId);
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("VoegWerknemerToe", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public Employee GetEmployee(int personId)
        {
            MySqlConnection connection = GetConnection();
            Employee employee = null;
            string query = "SELECT * FROM Employee WHERE EmployeeId = @employeeId AND IsDeleted = 0";
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

        public List<Employee> GetEmployees()
        {

            MySqlConnection connection = GetConnection();
            string query = "SELECT * FROM Employee WHERE IsDeleted=0";
            List<Employee> alleWerknemers = new List<Employee>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["EmployeeId"];
                        string LastName = (string)reader["LastName"];
                        string FirstName = (string)reader["FirstName"];
                        alleWerknemers.Add(new Employee(ID, LastName, FirstName));
                    }
                    reader.Close();
                    return alleWerknemers;
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("GeefWerknemers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Employee> GetEmployees(string? lastname, string? firstname)
        {
            MySqlConnection connection = GetConnection();
            string query = "SELECT * FROM Employee WHERE IsDeleted=0";
            List<Employee> employees = new List<Employee>();

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    if (string.IsNullOrWhiteSpace(lastname) && string.IsNullOrEmpty(firstname)) query = "SELECT * FROM Employee";
                    else
                    {
                        query = "SELECT * FROM Employee WHERE ";
                        if (!string.IsNullOrWhiteSpace(lastname))
                        {
                            query += "LastName=@lastname";
                            cmd.Parameters.AddWithValue("@lastname", lastname);
                        }
                        if (!string.IsNullOrWhiteSpace(firstname) && query != "SELECT * FROM Employee WHERE ") query += " AND ";
                        if (!string.IsNullOrWhiteSpace(firstname))
                        {
                            query += "FirstName=@firstname";
                            cmd.Parameters.AddWithValue("@firstname", firstname);
                        }
                        query += " AND IsDeleted = 0";
                    }

                    cmd.CommandText = query;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int ID = (int)reader["EmployeeId"];
                        string LastName = (string)reader["LastName"];
                        string FirstName = (string)reader["FirstName"];
                        employees.Add(new Employee(ID, LastName, FirstName));
                    }
                    reader.Close();
                    return employees;
                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("GeefWerknemers", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public void UpdateEmployee(int employeeid, string? lastname, string? firstname)
        {

            MySqlConnection connection = GetConnection();
            string query = "Update Employee SET  ";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();

                    if (!string.IsNullOrWhiteSpace(lastname))
                    {
                        query += "LastName=@lastname";
                        cmd.Parameters.AddWithValue("@lastname", lastname);
                    }
                    if (!string.IsNullOrWhiteSpace(firstname) && query != "Update Employee SET  ") query += ",";
                    if (!string.IsNullOrWhiteSpace(firstname))
                    {
                        query += "FirstName=@firstname";
                        cmd.Parameters.AddWithValue("@firstname", firstname);
                    }
                    query += " WHERE EmployeeId=@employeeId";
                    Console.WriteLine(query);
                    cmd.Parameters.AddWithValue("@employeeId", employeeid);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("UpdateWerknemer", ex);
                }
                finally { connection.Close(); }
            }
        }

        public void DeleteEmployee(int employeeId)
        {
            MySqlConnection connection = GetConnection();
            string queryWerknemer = "UPDATE Employee SET IsDeleted = 1 WHERE EmployeeId=@employeeId";
            string queryContract = "UPDATE Employeecontract SET IsDeleted = 1, ActiveWorkingAtCompany = 0 WHERE EmployeeId=@employeeId";

            using (MySqlCommand cmd = connection.CreateCommand())
            {
                try
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@employeeId", employeeId);

                    cmd.CommandText = queryWerknemer;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = queryContract;
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new EmployeeRepoADOException("VerwijderWerknemer", ex);
                }
                finally { connection.Close(); }
            }
        }
    }
}
