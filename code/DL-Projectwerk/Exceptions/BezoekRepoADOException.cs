using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    internal class BezoekRepoADOException : Exception
    {
        public BezoekRepoADOException(string? message) : base(message)
        {
        }

        public BezoekRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
