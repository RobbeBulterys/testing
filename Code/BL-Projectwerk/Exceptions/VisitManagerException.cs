using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class VisitManagerException : Exception
    {
        public VisitManagerException(string? message) : base(message)
        {
        }

        public VisitManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
