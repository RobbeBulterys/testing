using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class EmployeeManagerException : Exception
    {
        public EmployeeManagerException(string? message) : base(message)
        {
        }

        public EmployeeManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
 