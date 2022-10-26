using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk.Exceptions
{
    public class WerknemerRepoADOException : Exception {
        public WerknemerRepoADOException(string? message) : base(message) {
        }

        public WerknemerRepoADOException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
