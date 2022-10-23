using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BL_Projectwerk.Exceptions;



namespace BL_Projectwerk.Domein
{
    public class Controle
    {
        public static bool IsGoedeEmailSyntax(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new ControleException("Controle - IsGoedeEmailSyntax - geen email ingevuld"); }
            Regex emailCheck = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (!emailCheck.IsMatch(email)) { throw new ControleException("Controle - IsGoedeEmailSyntax - ongeldige email"); }
            return true;
        }
        public static bool IsBestaandBTWnummer(string BtwNummer)
        {
            if (string.IsNullOrWhiteSpace(BtwNummer)) { throw new ControleException("Controle - IsBestaandBTWnummer - geen BTWNummer ingevuld"); } //TODO : Regex controle geldigheid
            Regex BTWNummerCheck = new Regex(@"^((AT)?U[0-9]{8}|(BE)?0[0-9]{9}|(BG)?[0-9]{9,10}|(CY)?[0-9]{8}L|↵
                (CZ)?[0-9]{8,10}|(DE)?[0-9]{9}|(DK)?[0-9]{8}|(EE)?[0-9]{9}|↵
                (EL|GR)?[0-9]{9}|(ES)?[0-9A-Z][0-9]{7}[0-9A-Z]|(FI)?[0-9]{8}|↵
                (FR)?[0-9A-Z]{2}[0-9]{9}|(GB)?([0-9]{9}([0-9]{3})?|[A-Z]{2}[0-9]{3})|↵
                (HU)?[0-9]{8}|(IE)?[0-9]S[0-9]{5}L|(IT)?[0-9]{11}|↵
                (LT)?([0-9]{9}|[0-9]{12})|(LU)?[0-9]{8}|(LV)?[0-9]{11}|(MT)?[0-9]{8}|↵
                (NL)?[0-9]{9}B[0-9]{2}|(PL)?[0-9]{10}|(PT)?[0-9]{9}|(RO)?[0-9]{2,10}|↵
                (SE)?[0-9]{12}|(SI)?[0-9]{8}|(SK)?[0-9]{10})$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            if (!BTWNummerCheck.IsMatch(BtwNummer)) { throw new ControleException("Controle - IsBestaandBTWnummer - ongeldig BTW Nummer"); }
            return true;
        }
        public static bool IsGoedeAdresNummerSyntax(string nummer)
        {
            if (string.IsNullOrWhiteSpace(nummer)) { throw new ControleException("Controle - IsGoedeAdresNummerSyntax - geen nummer ingevuld"); }
            if (!nummer.Any(c => char.IsDigit(c))) { throw new ControleException("Controle - IsGoedeAdresNummerSyntax - geen geldig nummer ingevuld"); }
            if (!nummer.Contains(" ")) return true;
            List<string> list = nummer.Split(" ").ToList();
            if (list[1] != "bus" && list[1] != "Bus") { throw new ControleException("Controle - IsGoedeAdresNummerSyntax - geen geldige syntax ingevuld"); }
            if (list.Count != 3) { throw new ControleException("Controle - IsGoedeAdresNummerSyntax - geen geldige syntax ingevuld"); }
            return true;
        }
    }
}
