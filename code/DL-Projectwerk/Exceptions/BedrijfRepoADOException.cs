using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class BedrijfRepoADOException : Exception
    {
        public BedrijfRepoADOException(string? message) : base(message)
        {
        }

        public BedrijfRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
