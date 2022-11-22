using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class CompanyRepoADOException : Exception
    {
        public CompanyRepoADOException(string? message) : base(message)
        {
        }

        public CompanyRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
