using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Managers {
    public class EmployeeManager 
    {
        private IEmployeeRepository _employeeRepo;

        public EmployeeManager(IEmployeeRepository employeeRepo) 
        {
            _employeeRepo = employeeRepo;
        }

        public void AddEmployee(Employee employee) 
        {
            try 
            {
                if (employee == null) throw new EmployeeManagerException("EmployeeManager - AddEmployee - Employee is null");
                //if (_werknemerRepo.BestaatWerknemer(werknemer.Naam, werknemer.Voornaam)) { throw new WerknemerManagerException("VoegWerknemerToe"); }
                _employeeRepo.AddEmployee(employee);
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("AddEmployee", ex);
            }

        }

        public bool EmployeeExists(int employeeId) 
        {
            try 
            {
                if (employeeId == 0) throw new EmployeeManagerException("EmployeeManager - EmployeeExists - Invalid Employeeid");
                return _employeeRepo.EmployeeExists(employeeId);
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("EmployeeExists", ex);
            }
        }

        public bool EmployeeExists(string lastname, string firstname) 
        {
            try 
            {
                if (string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(firstname)) throw new EmployeeManagerException("EmployeeManager - EmployeeExists - No Lastname and/or firstname data entry");
                return _employeeRepo.EmployeeExists(lastname, firstname);
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("EmployeeExists", ex);
            }
        }

        public void DeleteEmployee(int employeeId) 
        {
            try 
            {
                if (employeeId == 0) throw new EmployeeManagerException("EmployeeManager - DeleteEmployee - Invalid employeeid"); 
                if (!_employeeRepo.EmployeeExists(employeeId)) throw new EmployeeManagerException("EmployeeManager - DeleteEmployee - employeeid does not exist"); 
                _employeeRepo.DeleteEmployee(employeeId);
            } 
            catch (Exception ex)
            {
                throw new EmployeeManagerException("DeleteEmployee", ex);
            }
        }

        public void UpdateEmployee(int employeeId, string? lastname, string? firstname) 
        {
            try 
            {
                if (string.IsNullOrWhiteSpace(lastname) && string.IsNullOrWhiteSpace(firstname)) throw new EmployeeManagerException("EmployeeManager - UpdateEmployee - No data entry");
                if (!string.IsNullOrWhiteSpace(lastname) || !string.IsNullOrWhiteSpace(firstname)) {
                    if (_employeeRepo.EmployeeExists(employeeId)) {
                        Employee dbwerknemer = _employeeRepo.GetEmployee(employeeId);
                        Employee update = new Employee(employeeId, dbwerknemer.LastName, dbwerknemer.FirstName);
                        if (!string.IsNullOrWhiteSpace(lastname)) update.SetLastName(lastname); 
                        if (!string.IsNullOrWhiteSpace(firstname)) update.SetFirstName(firstname);
                        if (dbwerknemer.HasSameProperties(update)) throw new EmployeeManagerException("EmployeeManager - UpdateEmployee - Employee is the same");
                    }
                }
                _employeeRepo.UpdateEmployee(employeeId, lastname, firstname);
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("UpdateEmployee", ex);
            }
        }

        public IReadOnlyList<Employee> GetAllEmployees() 
        {
            List<Employee> employees = new List<Employee>();
            try 
            {
                employees.AddRange(_employeeRepo.GetEmployees());
                return employees;
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("GetAllEmployees", ex);
            }
        }

        public IReadOnlyList<Employee> SearchEmployees(string? lastname, string? firstname) 
        {
            List<Employee> employees = new List<Employee>();
            try 
            {
                if (!string.IsNullOrEmpty(lastname) || !string.IsNullOrEmpty(firstname)) employees.AddRange(_employeeRepo.GetEmployees(lastname, firstname));
                else throw new EmployeeManagerException("EmployeeManager - SearchEmployees - Fields not filled in");
                return employees;
            } 
            catch (Exception ex) 
            {
                throw new EmployeeManagerException("SearchEmployees", ex);
            }
        }

        public Employee GetEmployee(int employeeId)
        {
            Employee employee = null;
            try
            {
                if (employeeId == 0 || employeeId < 0) { throw new EmployeeManagerException("EmployeeManager - GetEmployee - Invalid employeeid"); }
                employee = _employeeRepo.GetEmployee(employeeId);
                return employee;
                
            } 
            catch (Exception ex)
            {
                throw new EmployeeManagerException("GetEmployee", ex);
            }
        } 
    }
}
