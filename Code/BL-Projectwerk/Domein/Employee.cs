using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace BL_Projectwerk.Domein {
    public class Employee : Person 
    {
        public Employee(int personId, string lastname, string firstname) : base(personId, lastname, firstname)
        {

        }

        public Employee(string lastname, string firstname) : base(lastname, firstname) 
        {

        }

        public bool HasSameProperties(Employee employee) 
        {
            if (PersonId != employee.PersonId) return false;
            if (LastName != employee.LastName) return false; 
            if (FirstName != employee.FirstName) return false;
            return true;
        }

        //internal bool BevatContractMetBedrijf(Bedrijf bedrijf) { // Mag internal blijven, omdat UI of DL niet van deze method hoeft te weten.
        //    foreach (Werknemercontract wc in Contracten)
        //    {
        //        if (wc.Bedrijf.Equals(bedrijf))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public override string ToString() 
        {
            return $"Employee: {base.ToString()}";
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(PersonId);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Employee employee)
            {
                if (PersonId == employee.PersonId)
                {
                    if (PersonId == 0)
                    { // Id 0 Mike, Id 0 Niels zijn niet hetzelfde
                        return HasSameProperties(employee); // true or false
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
