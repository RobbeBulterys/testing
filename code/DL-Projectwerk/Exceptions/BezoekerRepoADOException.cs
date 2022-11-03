using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    internal class BezoekerRepoADOException : Exception
    {
        public BezoekerRepoADOException(string? message) : base(message)
        {
        }

        public BezoekerRepoADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
