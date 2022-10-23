using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class BezoekerManagerException : Exception
    {
        public BezoekerManagerException(string? message) : base(message)
        {
        }

        public BezoekerManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
