using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class CompanyManagerException : Exception
    {
        public CompanyManagerException(string? message) : base(message)
        {
        }

        public CompanyManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
