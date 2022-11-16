using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein {
    public class Werknemercontract 
    {
        public Werknemercontract(Bedrijf bedrijf, Werknemer werknemer, string functie) 
        {
            ZetBedrijfEnWerknemer(bedrijf, werknemer);
            ZetFunctie(functie);
        }

        //public Werknemercontract(int contractnummer, Bedrijf bedrijf, Werknemer werknemer, string functie) : this(bedrijf, werknemer, functie)
        //{
        //    ZetContractnummer(contractnummer);
        //}

        public Bedrijf Bedrijf { get; private set; }
        public string Email { get; private set; }
        public string Functie { get; private set; }
        public Werknemer Werknemer { get; private set; }

        private void ZetBedrijfEnWerknemer(Bedrijf bedrijf, Werknemer werknemer) 
        {
            // private omdat we het daarna niet meer kunnen veranderen
            // In geval van verkeerde werknemer of verkeerd bedrijf: ander contract (dit verwijderen en ander aanmaken)
            // We bundelen ZetBedrijf en ZetWerknemer, aangezien de werknemer eerst ingesteld moet worden.
            if (werknemer == null) { throw new WerknemercontractException("Werknemercontract - ZetBedrijfEnWerknemer : ZetWerknemer - Werknemer is null"); }
            if (bedrijf == null) { throw new WerknemercontractException("Werknemercontract - ZetBedrijfEnWerknemer : ZetBedrijf - Bedrijf is null"); }

            // Contract verkeerd, is contract verwijderen, bedrijf en werknemer kunnen niet wijzigen, dus hoeven we geen contract verwijderen bij werknemer of werknemer weghalen bij bedrijf.
            Werknemer = werknemer; // VoegContractToe moet controleren of de werknemer is ingevuld in het contract
            Bedrijf = bedrijf; //VoegWerknemerToe zou moeten controleren of de werknemer een contract bevat met dit bedrijf
        }

        public void ZetEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new WerknemercontractException("Werknemercontract - ZetEmail - Geen email ingevuld"); }
            email = email.Trim();
            try {
                if (!Controle.IsGoedeEmailSyntax(email)) { throw new WerknemercontractException("Werknemercontract - ZetEmail - Ongeldige email"); }
                Email = email;
            } catch (Exception ex) {
                throw ex;
            }
        }

        public void ZetFunctie(string functie) 
        {
            if (string.IsNullOrWhiteSpace(functie)) { throw new WerknemercontractException("Werknemercontract - ZetFunctie - Geen functie ingevuld"); }
            Functie = functie.Trim();
        }

        public bool IsDezelfde(Werknemercontract update) 
        {
            bool gelijkeBedrijfEnWerknemerIds = this.Bedrijf.Equals(update.Bedrijf) && this.Werknemer.Equals(update.Werknemer);
            return gelijkeBedrijfEnWerknemerIds && Email == update.Email && Functie == update.Functie;
        }

        public override string ToString() 
        {
            return $"Werknemercontract: {Bedrijf.Naam} - {Werknemer.Naam} - {Werknemer.Voornaam} - {Functie} - {Email}";
        }

        // Om niet weg te gooien

        //public int Contractnummer { get; private set; }

        //private void ZetContractnummer(int id) {
        //    if (id <= 0) { throw new WerknemercontractException("Werknemercontract - Zetcontractnummer - ongeldige id, kleiner dan of gelijk aan 0"); }
        //    Contractnummer = id;
        //}
    }
}
