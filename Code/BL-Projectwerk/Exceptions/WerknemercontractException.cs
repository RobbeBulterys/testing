using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class WerknemercontractException : Exception {
        public WerknemercontractException(string? message) : base(message) {
        }

        public WerknemercontractException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
