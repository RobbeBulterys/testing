using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class BezoekException : PersoonException
    {
        public BezoekException(string? message) : base(message)
        {
        }

        public BezoekException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
