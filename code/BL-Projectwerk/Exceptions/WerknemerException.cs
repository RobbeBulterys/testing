using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class WerknemerException : PersoonException
    {
        public WerknemerException(string? message) : base(message)
        {
        }

        public WerknemerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
