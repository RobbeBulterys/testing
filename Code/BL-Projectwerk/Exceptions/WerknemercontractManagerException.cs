using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class WerknemercontractManagerException : Exception {
        public WerknemercontractManagerException(string? message) : base(message) {
        }

        public WerknemercontractManagerException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
