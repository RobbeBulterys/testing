using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BL_Projectwerk.Domein {
    public abstract class Persoon 
    {
        protected Persoon(int persoonId, string naam, string voornaam)
        {
            // Persoon uit databank met ID.
            ZetId(persoonId);
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
        }

        protected Persoon(string naam, string voornaam) 
        {
            // Nieuw persoon om toe te voegen aan de databank : databank maakt een ID aan.
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
        }

        /*
         * Hieronder overbodige constructors met optionele property email die de repo of UI maar moet toevoegen als extra, anders zijn er te veel contructors. 
        protected Persoon(int persoonId, string naam, string voornaam, string email) {
            // persoon uit databank
            ZetId(persoonId);
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
            ZetEmail(email);
        }

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

        public void ZetId(int id) 
        {
            if (id < 1) { throw new PersoonException("Persoon - ZetId - Id ongeldig; Kleiner dan 1"); }
            PersoonId = id;
        }

        public void ZetNaam(string naam)
        { //een naam kan een spatie bevatten
            if (string.IsNullOrWhiteSpace(naam)) { throw new PersoonException("Persoon - ZetNaam - Geen naam ingevuld"); }
            Naam = naam.Trim();
        }

        public void ZetVoorNaam(string voornaam)
        { 
            if (string.IsNullOrWhiteSpace(voornaam)) { throw new PersoonException("Persoon - ZetVoorNaam - Geen voornaam ingevuld"); }
            voornaam = voornaam.Trim();
            Voornaam = voornaam;
        }

        public void ZetEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new PersoonException("Persoon - ZetEmail - Geen email ingevuld"); }
            email = email.Trim();
            try 
            {
                if (!Controle.IsGoedeEmailSyntax(email)) { throw new PersoonException("Persoon - ZetEmail - Ongeldige email"); }
                Email = email;
            } 
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public override string ToString() {
            return $"Persoon: {Naam} - {Voornaam} - {Email}";
        }
    }
}
