using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions
{
    public class ContractException : Exception
    {
        public ContractException(string? message) : base(message)
        {
        }

        public ContractException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
