using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class VerifyException : Exception
    {
        public VerifyException(string? message) : base(message)
        {

        }

        public VerifyException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
