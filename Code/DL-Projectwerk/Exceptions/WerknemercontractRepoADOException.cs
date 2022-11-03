using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions {
    public class WerknemercontractRepoADOException : Exception {
        public WerknemercontractRepoADOException(string? message) : base(message) {
        }

        public WerknemercontractRepoADOException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
