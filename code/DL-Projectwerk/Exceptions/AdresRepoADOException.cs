using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class AdresRepoADOException : Exception

    {
        public AdresRepoADOException(string? message) : base(message)
        {
        }

        public AdresRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
