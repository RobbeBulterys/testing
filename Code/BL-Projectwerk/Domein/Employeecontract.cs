using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein {
    public class Employeecontract 
    {
        public Employeecontract(Company company, Employee employee, string function) 
        {
            SetCompanyAndEmployee(company, employee);
            SetFunction(function);
        }

        //public Werknemercontract(int contractnummer, Bedrijf bedrijf, Werknemer werknemer, string functie) : this(bedrijf, werknemer, functie)
        //{
        //    ZetContractnummer(contractnummer);
        //}

        public Company Company { get; private set; }
        public string Email { get; private set; }
        public string Function { get; private set; }
        public Employee Employee { get; private set; }

        private void SetCompanyAndEmployee(Company company, Employee employee) 
        {
            // private omdat we het daarna niet meer kunnen veranderen
            // In geval van verkeerde werknemer of verkeerd bedrijf: ander contract (dit verwijderen en ander aanmaken)
            // We bundelen ZetBedrijf en ZetWerknemer, aangezien de werknemer eerst ingesteld moet worden.
            if (employee == null) { throw new EmployeecontractException("Employeecontract - SetCompanyAndEmployee : SetEmployee - Employee is null"); }
            if (company == null) { throw new EmployeecontractException("Employeecontract - SetCompanyAndEmployee : SetCompany - Company is null"); }

            // Contract verkeerd, is contract verwijderen, bedrijf en werknemer kunnen niet wijzigen, dus hoeven we geen contract verwijderen bij werknemer of werknemer weghalen bij bedrijf.
            Employee = employee; // VoegContractToe moet controleren of de werknemer is ingevuld in het contract
            Company = company; //VoegWerknemerToe zou moeten controleren of de werknemer een contract bevat met dit bedrijf
        }

        public void SetEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new EmployeecontractException("Employeecontract - SetEmail - No email data entry"); }
            email = email.Trim();
            try {
                if (!Verify.IsValidEmailSyntax(email)) { throw new EmployeecontractException("Employeecontract - SetEmail - Invalid email"); }
                Email = email;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void SetFunction(string function) 
        {
            if (string.IsNullOrWhiteSpace(function)) { throw new EmployeecontractException("Employeecontract - SetFunction - No function data entry"); }
            Function = function.Trim();
        }

        public bool HasSameProperties(Employeecontract update) 
        {
            bool SameCompanyAndEmployeeIds = this.Company.Equals(update.Company) && this.Employee.Equals(update.Employee);
            return SameCompanyAndEmployeeIds && Email == update.Email && Function == update.Function;
        }

        public override string ToString() 
        {
            return $"Employeecontract: {Company.Name} - {Employee.LastName} - {Employee.FirstName} - {Function} - {Email}";
        }

        // Om niet weg te gooien

        //public int Contractnummer { get; private set; }

        //private void ZetContractnummer(int id) {
        //    if (id <= 0) { throw new WerknemercontractException("Werknemercontract - Zetcontractnummer - ongeldige id, kleiner dan of gelijk aan 0"); }
        //    Contractnummer = id;
        //}
    }
}
