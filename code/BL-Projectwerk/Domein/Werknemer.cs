using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein {
    public class Werknemer : Persoon {
        public Werknemer(int persoonId, string naam, string voornaam, string functie) : base(persoonId, naam, voornaam)
        {
            ZetFunctie(functie);
        }
        public Werknemer(string naam, string voornaam, Bedrijf bedrijf, string functie) : base(naam, voornaam) {
            // nieuwe werknemer
            ZetFunctie(functie);
            ZetBedrijf(bedrijf);
        }

        public Werknemer(int persoonId, string naam, string voornaam, Bedrijf bedrijf, string functie) : base(persoonId, naam, voornaam) {
            // werknemer uit DB
            ZetFunctie(functie);
            ZetBedrijf(bedrijf);
        }

        public Werknemer(string naam, string voornaam, Bedrijf bedrijf, string functie, string email) : base(naam, voornaam, email) {
            // nieuwe werknemer met email
            ZetFunctie(functie);
            ZetBedrijf(bedrijf);
            // TODO: kijken naar non-nullable warnings
        }

        public Werknemer(int persoonId, string naam, string voornaam, Bedrijf bedrijf, string functie, string email) : base(persoonId, naam, voornaam, email) {
            // werknemer uit DB met email
            ZetFunctie(functie);
            ZetBedrijf(bedrijf);
        }

        public string Functie { get; set; }
        public Bedrijf Bedrijf { get; set; }

        public void ZetFunctie(string functie) {
            if (string.IsNullOrWhiteSpace(functie)) { throw new WerknemerException("Werknemer - ZetFunctie - geen functie ingevuld"); }
            Functie = functie.Trim();
        }
        public void VeranderBedrijf(Bedrijf bedrijf) {
            if (bedrijf == null) { throw new WerknemerException("Werknemer - VeranderBedrijf - geen bedrijf ingevuld"); }
            if (this.Bedrijf.Equals(bedrijf)) { throw new WerknemerException("Werknemer - VeranderBedrijf - werknemer werkt al voor dit bedrijf"); }
            this.Bedrijf = bedrijf;
        }
        public void ZetBedrijf(Bedrijf bedrijf) {
            if (bedrijf == null) { throw new WerknemerException("Werknemer - ZetBedrijf - geen bedrijf ingevuld"); }
            Bedrijf = bedrijf;
        }

        public override int GetHashCode() {
            return HashCode.Combine(PersoonId);
        }

        public bool IsDezelfde(Werknemer werknemer) {
            return PersoonId == werknemer.PersoonId &&
                   Naam == werknemer.Naam &&
                   Voornaam == werknemer.Voornaam &&
                   Email == werknemer.Email &&
                   Functie == werknemer.Functie &&
                   Bedrijf.Equals(werknemer.Bedrijf);
        }

        public override bool Equals(object? obj) {
            if (obj is Werknemer werknemer) {
                if (PersoonId == werknemer.PersoonId) {
                    if (PersoonId == 0) { // Id 0 Mike, Id 0 Niels zijn niet hetzelfde
                        return IsDezelfde(werknemer); // true or false
                    } else {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
