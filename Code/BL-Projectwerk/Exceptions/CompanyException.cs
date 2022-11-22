using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class CompanyException : Exception
    {
        public CompanyException(string? message) : base(message)
        {
        }

        public CompanyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
