using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Managers {
    public class EmployeecontractManager 
    {
        private IEmployeecontractRepository _ecRepo;

        public EmployeecontractManager(IEmployeecontractRepository employeecontractRepo) 
        {
            _ecRepo = employeecontractRepo;
        }

        public bool ContractExists(int employeeId, int companyId) 
        {
            try 
            {
                if (employeeId <= 0 || companyId <= 0) throw new EmployeecontractManagerException("EmployeecontractManager - ContractExists - Invalid id's"); 
                return _ecRepo.ContractExists(employeeId, companyId);
            } 
            catch (Exception ex) {
                throw new EmployeecontractManagerException("ContractExists", ex);
            }
        } // getest

        //public IReadOnlyList<Werknemercontract> GeefContracten() {
        //    List<Werknemercontract> contracten = new List<Werknemercontract>();
        //    try {
        //        contracten.AddRange(_wcRepo.GeefContracten());
        //        return contracten;
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("GeefContracten", ex);
        //    }
        //}

        public IReadOnlyList<Employeecontract> GetEmployeeContracts(Employee employee) 
        {
            List<Employeecontract> contracts = new List<Employeecontract>();
            try 
            {
                contracts.AddRange(_ecRepo.GetEmployeeContracts(employee));
                return contracts;

            } 
            catch (Exception ex) {
                throw new EmployeecontractManagerException("GetEmployeeContracts", ex);
            }
        }  // getest

        public IReadOnlyList<Employeecontract> GetCompanyContracts(Company company) 
        {
            List<Employeecontract> contracts = new List<Employeecontract>();
            try 
            {
                contracts.AddRange(_ecRepo.GetCompanyContracts(company));
                return contracts;
            } 
            catch (Exception ex) {
                throw new EmployeecontractManagerException("GetCompanyContracts", ex);
            }
        } // getest

        //public bool HeeftBedrijfContracten(int bedrijfsId) {
        //    try {
        //        if (bedrijfsId <= 0) { throw new WerknemercontractManagerException("HeeftBedrijfContracten - id <= 0"); }
        //        return _wcRepo.HeeftWerknemerContracten(bedrijfsId);
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("HeeftBedrijfContracten", ex);
        //    }
        //}

        //public bool HeeftWerknemerContracten(int werknemerId) {
        //    try {
        //        if (werknemerId <= 0) { throw new WerknemercontractManagerException("HeeftWerknemerContracten - id <= 0"); }
        //        return _wcRepo.HeeftWerknemerContracten(werknemerId);
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("HeeftWerknemerContracten", ex);
        //    }
        //}

        public void UpdateContract(Employee employee, Company company, string? function, string? email) 
        {
            if (employee.PersonId <= 0 || company.Id <= 0) throw new EmployeecontractManagerException("UpdateContract - employeeid or companyid invalid"); 
            if (!ContractExists(employee.PersonId, company.Id)) throw new EmployeecontractManagerException("UpdateContract - contract does not exist"); 
            try 
            {
                if (!string.IsNullOrWhiteSpace(function) || !string.IsNullOrWhiteSpace(email)) {
                    // een van de twee is ingevuld en veranderd
                    Employeecontract contractDB = _ecRepo.GetContract(employee, company);

                    // Overnemen van contractDB (clonen)
                    Employeecontract update = new Employeecontract(contractDB.Company, contractDB.Employee, contractDB.Function);
                    if (contractDB.Email != null) update.SetEmail(contractDB.Email); 

                    // Wijzigingen aanpassen
                    if (!string.IsNullOrWhiteSpace(function)) update.SetFunction(function);
                    if (!string.IsNullOrWhiteSpace(email)) update.SetEmail(email);

                    // Controleren of het dezelfde is
                    if (contractDB.HasSameProperties(update)) throw new EmployeecontractManagerException("UpdateContract - contract hasn't changed");

                    // contract is gewijzigd
                    _ecRepo.UpdateContract(employee.PersonId, company.Id, function, email);

                } 
                else throw new EmployeecontractManagerException("UpdateContract - no function or email parameter data entries"); 
            } 
            catch (Exception ex) {
                throw new EmployeecontractManagerException("UpdateContract", ex);
            }
        } // valid paden getest

        public void DeleteContract(Employeecontract contract) 
        {
            try 
            {
                if (contract == null) throw new EmployeecontractManagerException("EmployeecontractManager - DeleteContract - no contract data entry"); 
                if (!_ecRepo.ContractExists(contract.Employee.PersonId, contract.Company.Id)) throw new EmployeecontractManagerException("EmployeecontractManager - DeleteContract - Contract does not exist"); 
                _ecRepo.DeleteContract(contract.Employee.PersonId, contract.Company.Id);
            } 
            catch (Exception ex) 
            {
                throw new EmployeecontractManagerException("DeleteContract", ex);
            }
        } // getest

        public void AddContract(Employeecontract contract) 
        {
            try 
            {
                if (contract == null) throw new EmployeecontractManagerException("EmployeecontractManager - AddContract - no contract data entry");
                if (_ecRepo.ContractExists(contract.Employee.PersonId, contract.Company.Id)) throw new EmployeecontractManagerException("EmployeecontractManager - AddContract - Employeecontract already exists");
                _ecRepo.AddContract(contract);
            } 
            catch (Exception ex) 
            {
                throw new EmployeecontractManagerException("AddContract", ex);
            }
        } // getest


    }
}
