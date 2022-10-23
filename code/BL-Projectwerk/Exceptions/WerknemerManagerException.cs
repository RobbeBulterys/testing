using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class WerknemerManagerException : Exception
    {
        public WerknemerManagerException(string? message) : base(message)
        {
        }

        public WerknemerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
 