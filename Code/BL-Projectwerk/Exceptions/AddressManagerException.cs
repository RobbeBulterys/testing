using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class AddressManagerException : Exception
    {
        public AddressManagerException(string? message) : base(message)
        {
        }

        public AddressManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
