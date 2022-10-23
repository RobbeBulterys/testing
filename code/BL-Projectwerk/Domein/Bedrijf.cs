using BL_Projectwerk.Exceptions;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein {
    public class Bedrijf {
        //public Bedrijf(int id, string naam, string btwNummer, Adres adres, string telefoon, string email) {
        //    ZetId(id);
        //    ZetNaam(naam);
        //    ZetBTWNummer(btwNummer);
        //    Adres = adres;
        //    Telefoon = telefoon;
        //    ZetEmail(email);
        //    Parkeercontract = parkeercontract;
        //}

        public Bedrijf(int id, string naam, string btwNummer, string email) {
            ZetId(id);
            ZetNaam(naam);
            ZetBTWNummer(btwNummer);
            ZetEmail(email);
            //Parkeercontract = parkeercontract;
        }
        public Bedrijf(string naam, string btwNummer, string email) {
            ZetNaam(naam);
            ZetBTWNummer(btwNummer);
            ZetEmail(email);
        }
        /*
        public Bedrijf(int id, string naam, string btwNummer, Adres adres, string email) {
            ZetId(id);
            ZetNaam(naam);
            ZetBTWNummer(btwNummer);
            ZetAdres(adres);
            ZetEmail(email);
            //Parkeercontract = parkeercontract;
        }

        public Bedrijf(int id, string naam, string btwNummer, string telefoon, string email) {
            ZetId(id);
            ZetNaam(naam);
            ZetBTWNummer(btwNummer);
            Telefoon = telefoon;
            ZetEmail(email);
            //Parkeercontract = parkeercontract;
        }
        */
        public int Id { get; set; }

        public string Naam { get; set; }

        public string BTWNummer { get; set; }

        public Adres Adres { get; set; }

        public string Telefoon { get; set; }

        public string Email { get; set; }

        public ParkeerContract Parkeercontract { get; set; }
        private Dictionary<int, Werknemer> _werknemers = new Dictionary<int, Werknemer>();


        public void ZetId(int id) {
            if (id < 1) { throw new BedrijfException("Bedrijf - ZetId - Id ongeldig; kleiner dan 1"); }
            Id = id;
        }
        public void ZetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) { throw new BedrijfException("Bedrijf - ZetNaam - geen naam ingevuld"); }
            Naam = naam;
        }
        public void ZetBTWNummer(string BtwNummer) {
            if (string.IsNullOrWhiteSpace(BtwNummer)) { throw new BedrijfException("Bedrijf - ZetBTWNummer - geen BTWNummer ingevuld"); } //TODO : Regex controle geldigheid
            Controle.IsBestaandBTWnummer(BtwNummer);
            BTWNummer = BtwNummer;
        }
        public void ZetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new BedrijfException("Bedrijf - ZetEmail - geen email ingevuld"); }
            Controle.IsGoedeEmailSyntax(email);
            Email = email;
        }
        public void ZetTelefoon(string telefoon)
        {
            if (string.IsNullOrWhiteSpace(telefoon)) { throw new BedrijfException("Bedrijf - ZetEmail - geen email ingevuld"); }
            Telefoon = telefoon;
        }
        public void ZetAdres(Adres adres) { //TODO
            if (adres == null) { throw new BedrijfException("Bedrijf - VoegAdresToe - geen adres ingevuld"); }
            this.Adres = adres;
        }
        public void VeranderAdres(Adres nieuwAdres) { //TODO
            if (nieuwAdres == null) { throw new BedrijfException("Bedrijf - VeranderAdres - geen adres ingevuld"); }
            if (this.Adres.Equals(nieuwAdres)) { throw new BedrijfException("Bedrijf - VeranderAdres - adres is hetzelfde"); }
            this.Adres = nieuwAdres;
        }
        public IReadOnlyList<Werknemer> GeefWerknemers() { //TODO
            return _werknemers.Values.ToList().AsReadOnly();
        }
        public void VoegWerknemerToe(Werknemer werknemer) { //TODO
            if (werknemer == null) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - geen werknemer ingevuld"); }
            if (_werknemers.ContainsKey(werknemer.PersoonId)) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - werknemer bestaat al"); }
            this._werknemers.Add(werknemer.PersoonId, werknemer);
        }
        public void VerwijderWerknemer(int werknemerId) { //TODO
            if (werknemerId == 0) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - geen werknemer ingevuld"); }
            if (!_werknemers.ContainsKey(werknemerId)) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - werknemer bestaat niet"); }
            this._werknemers.Remove(werknemerId);
        }
        public bool IsDezelfde(Bedrijf bedrijf) {
            if (bedrijf == null) { throw new BedrijfException("Bedrijf - IsDezelfde - geen werknemer ingevuld"); }
            if (bedrijf.Id != this.Id) return false;
            if (bedrijf.Naam != this.Naam) return false;
            if (bedrijf.BTWNummer != this.BTWNummer) return false;
            if (bedrijf.Telefoon != this.Telefoon) return false;
            if (bedrijf.Email != this.Email) return false;
            return true;
        }
        public override bool Equals(object? obj) {
            if (obj is Bedrijf bedrijf) {
                if (bedrijf.Id == this.Id) { // 5 == 5, 0 == 0
                    if (this.Id == 0) {
                        // 0 == 0
                        return IsDezelfde(bedrijf); // intern properties controleren
                    } else {
                        return true;
                    }
                }
            }
            return false;

        }
        public override int GetHashCode() {
            return HashCode.Combine(Id);
        }
    }
}