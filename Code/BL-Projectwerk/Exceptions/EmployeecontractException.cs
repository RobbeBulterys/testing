using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class EmployeecontractException : Exception {
        public EmployeecontractException(string? message) : base(message) {
        }

        public EmployeecontractException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
