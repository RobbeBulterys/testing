using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein
{
    public class Visitor : Person
    {
        public Visitor(string lastname, string firstname, string email, string company) : base(lastname, firstname)
        {
            SetCompany(company);
            SetEmail(email);
        }

        public Visitor(int visitorId, string lastname, string firstname, string email, string company) : base(visitorId, lastname, firstname) 
        {
            SetCompany(company);
            SetEmail(email);
        }

        public string Company { get; set; }

        public void SetCompany(string company)
        {
            if (string.IsNullOrWhiteSpace(company)) { throw new VisitorException("Visitor - SetCompany - No company data entry"); }
            Company = company.Trim();
        }

        public void ChangeCompany(string company) 
        {
            if (string.IsNullOrWhiteSpace(company)) { throw new VisitorException("Visitor - ChangeCompany - No company data entry"); }
            company = company.Trim();
            if (company == Company) { throw new VisitorException("Visitor - ChangeCompany - Company is the same"); }
            Company = company;
        }

        public bool HasSameProperties(Visitor visitor)
        {
            if (LastName != visitor.LastName) return false;
            if (FirstName != visitor.FirstName) return false;
            if (Email != visitor.Email) return false;
            if (Company != visitor.Company) return false;
            return true;
        }

        public override string ToString()
        {
            return $"Visitor: {base.ToString()} - {Company} - {Email}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersonId);
        }

        public override bool Equals(object? obj)
        {
            return obj is Visitor visitor &&
                   PersonId == visitor.PersonId;
        }
    }
}
