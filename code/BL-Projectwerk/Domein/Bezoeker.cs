using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein
{
    public class Bezoeker : Persoon
    {
        public Bezoeker(string naam, string voornaam, string email, string bedrijf) : base(naam, voornaam)
        {
            ZetBedrijf(bedrijf);
            ZetEmail(email);
        }

        public Bezoeker(int bezoekerId, string naam, string voornaam, string email, string bedrijf) : base(bezoekerId, naam, voornaam) 
        {
            ZetBedrijf(bedrijf);
            ZetEmail(email);
        }
        public string Bedrijf { get; set; }
        public void ZetBedrijf(string bedrijf)
        {
            // TODO : controle zet bedrijf
            if (string.IsNullOrEmpty(bedrijf)) { throw new BezoekerException("Bezoeker - ZetBedrijf - geen bedrijf ingevuld"); }
            Bedrijf = bedrijf;
        }
        public void VeranderBedrijf(string bedrijf) {
            try {
                if (bedrijf == Bedrijf) { throw new BezoekerException("Bezoeker - VeranderBedrijf - Bedrijf is hetzelfde"); }
                ZetBedrijf(bedrijf);
            } catch (Exception ex) {
                throw new BezoekerException("Bezoeker - VeranderBedrijf", ex); // exception zou komen van ZetBedrijf
            }
        }
        public Bezoeker Clone() {
            // Reden waarom deze method hier staat:
            // Bezoeker bevat een constructor van 4 strings, maar die volgorde is daarom niet exact als we toevoegingen doen of verplaatsingen in de code. Bij unit tests zijn de readonly bezoekers immutable in referentieadres, maar niet qua object. We willen wel steeds met het originele object werken in de test en niet diegene met de gewijzigde properties. In de meeste gevallen geeft het geen verschil.
            Bezoeker bezoeker = new Bezoeker(this.Naam, this.Voornaam, this.Email, this.Bedrijf);
            return bezoeker;
        }

        public override bool Equals(object? obj)
        {
            return obj is Bezoeker bezoeker &&
                   PersoonId == bezoeker.PersoonId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersoonId);
        }

        internal bool HeefZelfdeProperties(Bezoeker bezoeker)
        {
            throw new NotImplementedException();
        }
        public bool IsDezelfde(Bezoeker bezoeker)
        {
            if (PersoonId != bezoeker.PersoonId ) return false;
            if (Naam != bezoeker.Naam) return false;
            if (Voornaam != bezoeker.Voornaam) return false;
            if (Email != bezoeker.Email) return false;
            if (Bedrijf != bezoeker.Bedrijf) return false;
            return true;
        }
    }
}
