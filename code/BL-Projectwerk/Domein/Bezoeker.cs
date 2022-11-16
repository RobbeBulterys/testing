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
            if (string.IsNullOrWhiteSpace(bedrijf)) { throw new BezoekerException("Bezoeker - ZetBedrijf - Geen bedrijf ingevuld"); }
            Bedrijf = bedrijf.Trim();
        }

        public void VeranderBedrijf(string bedrijf) 
        {
            if (string.IsNullOrWhiteSpace(bedrijf)) { throw new BezoekerException("Bezoeker - VeranderBedrijf - Geen bedrijf ingevuld"); }
            bedrijf = bedrijf.Trim();
            if (bedrijf == Bedrijf) { throw new BezoekerException("Bezoeker - VeranderBedrijf - Bedrijf is hetzelfde"); }
            Bedrijf = bedrijf;
        }

        public bool IsDezelfde(Bezoeker bezoeker)
        {
            if (Naam != bezoeker.Naam) return false;
            if (Voornaam != bezoeker.Voornaam) return false;
            if (Email != bezoeker.Email) return false;
            if (Bedrijf != bezoeker.Bedrijf) return false;
            return true;
        }

        public override string ToString()
        {
            return $"Bezoeker: {base.ToString()} - {Bedrijf} - {Email}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersoonId);
        }

        public override bool Equals(object? obj)
        {
            return obj is Bezoeker bezoeker &&
                   PersoonId == bezoeker.PersoonId;
        }
    }
}
