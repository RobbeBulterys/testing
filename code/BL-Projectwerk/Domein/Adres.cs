using BL_Projectwerk.Exceptions;
using System.IO;

namespace BL_Projectwerk.Domein
{
    public class Adres
    {
        public Adres(string straat, string nummer, string postcode, string plaats, string land)
        {
            ZetStraat(straat);
            ZetNummer(nummer);
            ZetPostcode(postcode);
            ZetPlaats(plaats);
            ZetLand(land);
        }

        public Adres(int id, string straat, string nummer, string postcode, string plaats, string land) {
            // Adres vanuit databank
            ZetId(id);
            ZetStraat(straat);
            ZetNummer(nummer);
            ZetPostcode(postcode);
            ZetPlaats(plaats);
            ZetLand(land);
        }

        public int Id { get; set; }
        public string Straat { get; set; }
        public string Nummer { get; set; }
        public string Postcode { get; set; }
        public string Plaats { get; set; }

        public string Land { get; set; }

        public void ZetId(int id) {
            if (id < 1) { throw new AdresException("Adres - ZetId - Id ongeldig; kleiner dan 1"); }
            Id = id;
        }
        public void ZetStraat(string straat)
        {
            if (string.IsNullOrWhiteSpace(straat)) { throw new AdresException("Adres - ZetStraat - geen straat ingevuld"); }
            Straat = straat.Trim();
        }

        public void ZetNummer(string nummer)
        {
            if (string.IsNullOrWhiteSpace(nummer)) { throw new AdresException("Adres - ZetNummer - geen nummer ingevuld"); }
            if (!Controle.IsGoedeAdresNummerSyntax(nummer)) { throw new AdresException("Adres - ZetNummer - geen geldige nummer ingevuld"); }
            Nummer = nummer.Trim();
        }

        public void ZetPostcode(string postcode)
        {
            if (string.IsNullOrWhiteSpace(postcode)) { throw new AdresException("Adres - ZetPostcode - geen postcode ingevuld"); }
            Postcode = postcode.Trim();
        }

        public void ZetPlaats(string plaats)
        {
            if (string.IsNullOrWhiteSpace(plaats)) { throw new AdresException("Adres - ZetPlaats - geen plaats ingevuld"); }
            Plaats = plaats.Trim();
        }

        public void ZetLand(string land)
        {
            if (string.IsNullOrWhiteSpace(land)) { throw new AdresException("Adres - ZetLand - geen land ingevuld"); }
            Land = land.Trim();
        }

        public bool IsDezelfde(object? obj)
        {
            return obj is Adres adres &&
                   Id == adres.Id &&
                   Straat == adres.Straat &&
                   Nummer == adres.Nummer &&
                   Postcode == adres.Postcode &&
                   Plaats == adres.Plaats &&
                   Land == adres.Land;
        }

        public override string ToString()
        {
            return $"{Id} - {Straat} - {Nummer} - {Postcode} - {Plaats} - {Land}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Straat, Nummer, Postcode, Plaats, Land);
        }

        public override bool Equals(object? obj)
        {
            return obj is Adres adres &&
                   Id == adres.Id;
        }
    }
}
