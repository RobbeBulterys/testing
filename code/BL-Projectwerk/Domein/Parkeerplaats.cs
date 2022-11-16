using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein
{
    public class Parkeerplaats
    {
        public Parkeerplaats(int parkeerId, Bedrijf bedrijfVanReservatie, bool isIngenomen, string nummerplaat, bool isReserveerbaar)
        {
            ZetId(parkeerId);
            ZetVanBedrijf(bedrijfVanReservatie, isReserveerbaar);
            IsIngenomen = isIngenomen;
            Nummerplaat = nummerplaat;
            IsReserveerbaar = isReserveerbaar;
        }

        public int ParkeerId { get; set; }
        public Bedrijf BedrijfVanReservatie { get; set; }
        public bool IsIngenomen { get; set; }
        public string Nummerplaat { get; set; }
        public bool IsReserveerbaar { get; set; }

        public void ZetId(int id)
        {
            if (id < 1) { throw new ParkeerplaatsException("Parkeerplaats - ZetId - Id ongeldig; Kleiner dan 1"); }
            ParkeerId = id;
        }

        public void ZetVanBedrijf(Bedrijf bedrijfVanReservatie, bool IsReserveerbaar)
        {
            if (!IsReserveerbaar && bedrijfVanReservatie != null) { throw new ParkeerplaatsException("Parkeerplaats - ZetVanBedrijf - Een niet reserveerbare parkeerplek mag geen bedrijf toegewezen krijgen"); }
            BedrijfVanReservatie = bedrijfVanReservatie;
        }
    }
}
