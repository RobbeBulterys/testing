using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions {
    public class EmployeecontractRepoADOException : Exception {
        public EmployeecontractRepoADOException(string? message) : base(message) {
        }

        public EmployeecontractRepoADOException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
