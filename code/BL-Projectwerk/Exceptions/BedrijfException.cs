using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class BedrijfException : Exception
    {
        public BedrijfException(string? message) : base(message)
        {
        }

        public BedrijfException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
