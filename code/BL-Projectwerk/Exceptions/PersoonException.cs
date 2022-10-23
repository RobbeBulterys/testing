﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Exceptions {
    public class PersoonException : Exception {
        public PersoonException(string? message) : base(message) {
        }

        public PersoonException(string? message, Exception? innerException) : base(message, innerException) {
        }
    }
}
