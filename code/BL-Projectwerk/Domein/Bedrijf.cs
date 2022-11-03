using BL_Projectwerk.Exceptions;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein {
    public class Bedrijf {
        public Bedrijf(int id, string naam, string btwNummer, string email) : this(naam, btwNummer, email) {
            ZetId(id);
            //Parkeercontract = parkeercontract;
        }
        public Bedrijf(string naam, string btwNummer, string email) {
            ZetNaam(naam);
            ZetBTWNummer(btwNummer);
            ZetEmail(email);
        }

        public int Id { get; set; }

        public string Naam { get; set; }

        public string BTWNummer { get; set; }

        public Adres Adres { get; set; }

        public string Telefoon { get; set; }

        public string Email { get; set; }

        public ParkeerContract Parkeercontract { get; set; }
        //private Dictionary<int, Werknemer> _werknemers = new Dictionary<int, Werknemer>();


        public void ZetId(int id) {
            if (id < 1) { throw new BedrijfException("Bedrijf - ZetId - Id ongeldig; kleiner dan 1"); }
            Id = id;
        }
        public void ZetNaam(string naam) {
            if (string.IsNullOrWhiteSpace(naam)) { throw new BedrijfException("Bedrijf - ZetNaam - geen naam ingevuld"); }
            Naam = naam.Trim();
        }
        public void ZetBTWNummer(string BtwNummer) {
            if (string.IsNullOrWhiteSpace(BtwNummer)) { throw new BedrijfException("Bedrijf - ZetBTWNummer - geen BTWNummer ingevuld"); } //TODO : Regex controle geldigheid
            BtwNummer = BtwNummer.Trim();
            Controle.IsBestaandBTWnummer(BtwNummer);
            BTWNummer = BtwNummer;
        }
        public void ZetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new BedrijfException("Bedrijf - ZetEmail - geen email ingevuld"); }
            email = email.Trim();
            Controle.IsGoedeEmailSyntax(email);
            Email = email;
        }
        public void ZetTelefoon(string telefoon) {
            if (string.IsNullOrWhiteSpace(telefoon)) { throw new BedrijfException("Bedrijf - ZetEmail - geen telefoon ingevuld"); }
            Telefoon = telefoon.Trim();
        }
        public void ZetAdres(Adres adres) {
            if (adres == null) { throw new BedrijfException("Bedrijf - VoegAdresToe - geen adres ingevuld"); }
            this.Adres = adres;
        }
        public void VeranderAdres(Adres nieuwAdres) {
            if (nieuwAdres == null) { throw new BedrijfException("Bedrijf - VeranderAdres - geen adres ingevuld"); }
            if (this.Adres.IsDezelfde(nieuwAdres)) { throw new BedrijfException("Bedrijf - VeranderAdres - adres is hetzelfde"); }
            this.Adres = nieuwAdres;
        }
        //public IReadOnlyList<Werknemer> GeefWerknemers() {
        //    return _werknemers.Values.ToList().AsReadOnly();
        //}
        //public void VoegWerknemerToe(Werknemer werknemer) { // werknemer die een contract heeft
        //    // We verwachten dat werknemer een contract heeft met dit bedrijf
        //    if (werknemer == null) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - geen werknemer ingevuld"); }
        //    if (_werknemers.ContainsKey(werknemer.PersoonId)) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - werknemer is al toegevoegd aan het bedrijf"); }
        //    if (!werknemer.BevatContractMetBedrijf(this)) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - werknemer bevat geen contract met dit bedrijf"); }
        //    this._werknemers.Add(werknemer.PersoonId, werknemer);
        //}

        //public void StelWerknemerContractOp(Werknemer werknemer, string functie, string? email) {
        //    // Contract opstellen
        //    Werknemercontract wc = new Werknemercontract(this, werknemer, functie);

        //    // Optionele velden toevoegen
        //    if (email != null) {
        //        // Goede syntax van email moet gecontroleerd worden bij email
        //        //if (!Controle.IsGoedeEmailSyntax(email)) { throw new BedrijfException("Bedrijf - StelWerknemerContractOp - geen goede emailsyntax"); }
        //        wc.ZetEmail(email);
        //    } else {
        //        // niets, geen email meegstuurd
        //    }



        //}

        //public void VerwijderWerknemer(int werknemerId) {
        //    if (werknemerId == 0) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - geen werknemer ingevuld"); }
        //    if (!_werknemers.ContainsKey(werknemerId)) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - werknemer bestaat niet"); }
        //    this._werknemers.Remove(werknemerId);
        //}
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

        public override string ToString() {
            return $"Bedrijf: {Id}, {Naam}, {BTWNummer}";
        }
    }
}