using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;

namespace BL_Projectwerk.Domein {
    public class Werknemer : Persoon {

        public Werknemer(int persoonId, string naam, string voornaam) : base(persoonId, naam, voornaam)
        {
        }

        public Werknemer(string naam, string voornaam) : base(naam, voornaam) {
        }

        public override int GetHashCode() {
            return HashCode.Combine(PersoonId);
        }

        public bool IsDezelfde(Werknemer werknemer) {
            if (PersoonId != werknemer.PersoonId) return false;
            if (Naam != werknemer.Naam) return false; 
            if (Voornaam != werknemer.Voornaam) return false;
            return true;
        }


        public override bool Equals(object? obj)
        {
            if (obj is Werknemer werknemer)
            {
                if (PersoonId == werknemer.PersoonId)
                {
                    if (PersoonId == 0)
                    { // Id 0 Mike, Id 0 Niels zijn niet hetzelfde
                        return IsDezelfde(werknemer); // true or false
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //internal bool BevatContractMetBedrijf(Bedrijf bedrijf) { // Mag internal blijven, omdat UI of DL niet van deze method hoeft te weten.
        //    foreach (Werknemercontract wc in Contracten)
        //    {
        //        if (wc.Bedrijf.Equals(bedrijf))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        public override string ToString() {
            return $"Werknemer: {base.ToString()}";
        }
    }
}
