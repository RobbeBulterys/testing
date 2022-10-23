using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class BezoekManagerException : Exception
    {
        public BezoekManagerException(string? message) : base(message)
        {
        }

        public BezoekManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
