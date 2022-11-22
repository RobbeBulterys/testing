using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class PersonException : Exception {
        public PersonException(string? message) : base(message) {
        }

        public PersonException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
