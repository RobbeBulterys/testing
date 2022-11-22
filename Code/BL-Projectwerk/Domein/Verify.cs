using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL_Projectwerk.Exceptions;



namespace BL_Projectwerk.Domein {
    public static class Verify {
        public static bool IsValidEmailSyntax(string email) {
            if (string.IsNullOrWhiteSpace(email)) { return false; }
            Regex emailCheck = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (emailCheck.IsMatch(email)) return true;
            return false;
        }

        public static bool IsExistingVATnumber(string VatNumber) {
            if (string.IsNullOrWhiteSpace(VatNumber)) { return false; } //TODO : Regex controle geldigheid NL898219504B68 geeft foutmeldig maar zou goede syntax zijn.
            Regex BTWNummerCheck = new Regex(@"^((AT)?U[0-9]{8}|(NL)?[0-9]{9}B[0-9]{2}|(BE)?0[0-9]{9})$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (BTWNummerCheck.IsMatch(VatNumber)) return true;
            return false;
        }

        public static bool IsValidAdressNumberSyntax(string number) {
            if (string.IsNullOrWhiteSpace(number)) { return false; }
            if (!number.Any(c => char.IsDigit(c))) { return false; }
            if (!number.Contains(" ")) return true;
            List<string> list = number.Split(" ").ToList();
            if (list.Count != 3) { return false; }
            return true;
        }
    }
}
