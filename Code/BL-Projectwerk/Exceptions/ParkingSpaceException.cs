using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ParkingSpaceException : Exception
    {
        public ParkingSpaceException(string? message) : base(message)
        {
        }

        public ParkingSpaceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
