using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces {
    public interface IEmployeecontractRepository {
        bool ContractExists(int employeeId, int companyId);
        Employeecontract GetContract(Employee employee, Company company);
        //IEnumerable<Werknemercontract> GeefContracten();
        IEnumerable<Employeecontract> GetCompanyContracts(Company company);
        IEnumerable<Employeecontract> GetEmployeeContracts(Employee employee);
        //bool HeeftWerknemerContracten(int bedrijfsId);
        void UpdateContract(int employeeId, int companyId, string? function, string? email);
        void DeleteContract(int employeeId, int companyId);
        void AddContract(Employeecontract contract);
    }
}
