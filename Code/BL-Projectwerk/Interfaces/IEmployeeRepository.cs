using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces
{
    public interface IEmployeeRepository
    {
        bool EmployeeExists(int id);
        bool EmployeeExists(string LastName, string firstName);
        Employee GetEmployee(int personId);
        List<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployees(string? lastName, string? firstName);
        void UpdateEmployee(int employeeId, string? LastName, string? FirstName);
        void DeleteEmployee(int employeeId);
        void AddEmployee(Employee employee);
    }
}
