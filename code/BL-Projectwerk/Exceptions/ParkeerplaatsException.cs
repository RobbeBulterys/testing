using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ParkeerplaatsException : Exception
    {
        public ParkeerplaatsException(string? message) : base(message)
        {
        }

        public ParkeerplaatsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
