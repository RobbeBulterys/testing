using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class EmployeeRepoADOException : Exception {
        public EmployeeRepoADOException(string? message) : base(message) {
        }

        public EmployeeRepoADOException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
