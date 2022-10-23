using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ControleException : Exception
    {
        public ControleException(string? message) : base(message)
        {

        }

        public ControleException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
