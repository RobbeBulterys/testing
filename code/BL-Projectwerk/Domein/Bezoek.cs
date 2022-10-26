using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein {
    public class Bezoek {
        public Bezoek(Bezoeker bezoeker, Bedrijf bedrijf, Werknemer contactPersoonId, DateTime startTijd) {
            ZetBezoeker(bezoeker);
            ZetBedrijf(bedrijf);
            ZetContactpersoon(contactPersoonId);
            ZetStartTijd(startTijd);
        }

        public Bezoek(int bezoekId, Bezoeker bezoeker, Bedrijf bedrijf, Werknemer contactPersoonId, DateTime startTijd, DateTime eindTijd) {
            ZetId(bezoekId);
            ZetBezoeker(bezoeker);
            ZetBedrijf(bedrijf);
            ZetContactpersoon(contactPersoonId);
            ZetStartTijd(startTijd);
            ZetEindTijd(eindTijd, startTijd);
        }

        // public Bezoek() {        }

        public int BezoekId { get; set; }

        public Bezoeker Bezoeker { get; set; }

        public Bedrijf Bedrijf { get; set; }

        public Werknemer Contactpersoon { get; set; }

        public DateTime StartTijd { get; set; }

        public DateTime EindTijd { get; set; }

        public void ZetId(int id) {
            if (id < 1) { throw new BezoekException("Bezoek - ZetId - Id ongeldig; kleiner dan 1"); }
            BezoekId = id;
        }
        public void ZetBezoeker(Bezoeker bezoeker) {
            if (bezoeker == null) { throw new BezoekException("Bezoek - ZetBezoeker - geen bezoeker ingevuld"); }
            Bezoeker = bezoeker;
        }

        public void ZetBedrijf(Bedrijf bedrijf) {
            if (bedrijf == null) { throw new BezoekException("Bezoek - ZetBedrijf - geen bedrijf ingevuld"); }
            Bedrijf = bedrijf;
        }
        public void VeranderBedrijf(Bedrijf bedrijf) {
            if (bedrijf == null) { throw new BezoekException("Bezoek - VeranderBedrijf - geen bedrijf ingevuld"); }
            if (bedrijf.Equals(Bedrijf)) { throw new BezoekException("Bezoek - VeranderBedrijf - je stelt opnieuw hetzelfde bedrijf in."); }
            Bedrijf = bedrijf;
        }

        public void ZetContactpersoon(Werknemer contactpersoon) {
            if (contactpersoon == null) { throw new BezoekException("Bezoek - ZetContactPersoonId - geen contactpersoon ingevuld"); }
            Contactpersoon = contactpersoon;
        }
        public void VeranderContactpersoon(Werknemer contactpersoon) {
            if (Contactpersoon == null) { throw new BezoekException("Bezoek - VeranderContactpersoon - Bezoek had nog geen contactpersoon: dit is niet de geschikte methode"); }
            if (contactpersoon == null) { throw new BezoekException("Bezoek - VeranderContactpersoon - geen contactpersoon ingevuld"); }
            if (contactpersoon.Equals(Contactpersoon)) { throw new BezoekException("Bezoek - VeranderContactpersoon - contactpersoon is hetzelfde"); }
            Contactpersoon = contactpersoon;
        }

        public void VeranderBezoeker(Bezoeker nieuweBezoeker) {
            if (Bezoeker == null) { throw new BezoekException("Bezoek - VeranderBezoeker - Bezoek had nog geen bezoeker: dit is niet de geschikte methode"); }
            if (nieuweBezoeker == null) { throw new BezoekException("Bezoek - VeranderBezoeker - geen bezoeker ingevuld"); }
            if (nieuweBezoeker.Equals(Bezoeker)) { throw new BezoekException("Bezoek - VeranderBezoeker - bezoeker is hetzelfde"); }
            Bezoeker = nieuweBezoeker;
        }

        public void ZetStartTijd(DateTime starttijd) {
            // TODO: Eventueel extra controles (bvb voor het jaar 2000 mag niet)
            // if (starttijd < DateTime.Now) { throw new BezoekException("Bezoek - ZetStartTijd - starttijd kan niet in het verleden zijn"); }
            // dat datetime met hashcode = 0 een niet ingevoerde datetime is
            if (starttijd.GetHashCode() == 0) { throw new BezoekException("Bezoek - ZetStartTijd - geen starttijd ingevuld"); }
            // TODO: controle als tijd hetzelfde is exception
            StartTijd = starttijd;
        }

        public void ZetEindTijd(DateTime eindtijd, DateTime starttijd) {
            if (eindtijd.GetHashCode() == 0) { throw new BezoekException("Bezoek - ZetEindTijd - geen eindtijd ingevuld"); }
            if (eindtijd < starttijd) { throw new BezoekException("Bezoek - ZetEindTijd - eindtijd kan niet voor starttijd liggen"); }
            // TODO: controle als tijd hetzelfde is exception
            EindTijd = eindtijd;
        }

        public bool IsDezelfde(Bezoek bezoek)
        {
            return BezoekId == bezoek.BezoekId &&
                   bezoek.Bezoeker.IsDezelfde(Bezoeker) &&
                   bezoek.Bedrijf.IsDezelfde(Bedrijf) &&
                   bezoek.Contactpersoon.IsDezelfde(Contactpersoon) &&
                   StartTijd == bezoek.StartTijd &&
                   EindTijd == bezoek.EindTijd;
        }

        public override int GetHashCode() {
            return HashCode.Combine(BezoekId, Bezoeker, Bedrijf, Contactpersoon, StartTijd, EindTijd);
        }

        internal bool BestaatBezoeker(Bezoeker bezoeker)
        {
            throw new NotImplementedException();
        }
    }
}
