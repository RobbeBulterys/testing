using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class AddressRepoADOException : Exception

    {
        public AddressRepoADOException(string? message) : base(message)
        {
        }

        public AddressRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
