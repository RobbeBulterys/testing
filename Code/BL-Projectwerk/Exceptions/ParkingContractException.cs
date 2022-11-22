using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ParkingcontractException : Exception
    {
        public ParkingcontractException(string? message) : base(message)
        {
        }

        public ParkingcontractException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
