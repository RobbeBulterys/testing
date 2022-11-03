using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ParkeercontractException : Exception
    {
        public ParkeercontractException(string? message) : base(message)
        {
        }

        public ParkeercontractException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
