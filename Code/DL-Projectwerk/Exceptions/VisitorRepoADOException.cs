using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    internal class VisitorRepoADOException : Exception
    {
        public VisitorRepoADOException(string? message) : base(message)
        {
        }

        public VisitorRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
