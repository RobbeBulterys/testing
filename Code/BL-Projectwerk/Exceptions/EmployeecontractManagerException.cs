using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class EmployeecontractManagerException : Exception {
        public EmployeecontractManagerException(string? message) : base(message) {
        }

        public EmployeecontractManagerException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
