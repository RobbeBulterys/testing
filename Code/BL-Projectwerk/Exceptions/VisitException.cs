using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class VisitException : PersonException
    {
        public VisitException(string? message) : base(message)
        {
        }

        public VisitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
