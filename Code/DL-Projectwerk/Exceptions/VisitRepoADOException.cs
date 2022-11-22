using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    internal class VisitRepoADOException : Exception
    {
        public VisitRepoADOException(string? message) : base(message)
        {
        }

        public VisitRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
