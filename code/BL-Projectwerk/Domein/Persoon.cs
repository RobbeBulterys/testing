using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BL_Projectwerk.Domein {
    public abstract class Persoon {
        protected Persoon(int persoonId, string naam, string voornaam) {
            // persoon uit databank
            ZetId(persoonId);
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
        }
        /*
        protected Persoon(int persoonId, string naam, string voornaam, string email) {
            // persoon uit databank
            ZetId(persoonId);
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
            ZetEmail(email);
        }
        */
        protected Persoon(string naam, string voornaam) {
            // nieuw persoon voor databank
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
        }
        /*
        protected Persoon(string naam, string voornaam, string email) {
            // nieuw persoon voor databank
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
            ZetEmail(email);
        }
        */
        public int PersoonId { get; private set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Email { get; set; }

        public void ZetId(int id) {
            if (id < 1) { throw new PersoonException("Persoon - ZetId - Id ongeldig; kleiner dan 1"); }
            PersoonId = id;
        }
        public void ZetNaam(string naam)
        { //een naam kan een spatie bevatten
            if (string.IsNullOrWhiteSpace(naam)) { throw new PersoonException("Persoon - ZetNaam - geen naam ingevuld"); }
            Naam = naam.Trim();
        }

        public void ZetVoorNaam(string voornaam)
        { 
            if (string.IsNullOrWhiteSpace(voornaam)) { throw new PersoonException("Persoon - ZetVoorNaam - geen voornaam ingevuld"); }
            voornaam = voornaam.Trim();
            if (voornaam.Contains(' ')) { throw new PersoonException("Persoon - ZetVoorNaam - naam mag spaties bevatten"); }
            Voornaam = voornaam;
        }

        public void ZetEmail(string email) {
            if (string.IsNullOrWhiteSpace(email)) { throw new PersoonException("Persoon - ZetEmail - geen email ingevuld"); }
            email = email.Trim();
            try {
                if (!Controle.IsGoedeEmailSyntax(email)) { throw new PersoonException("Persoon - ZetEmail - ongeldige email"); }
                Email = email;
            } catch (Exception ex) {
                throw new PersoonException("Persoon - ZetEmail", ex);
            }
        }

        public override string ToString() {
            return $"{Naam}, {Voornaam}, {Email}";
        }
    }
}
