﻿using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein
{
    public class ParkingContract
    {
        public ParkingContract(int contractNummer, DateOnly startDatum, DateOnly eindDatum, int aantalParkeerPlaatsen)
        {
            ContractNummer = contractNummer;
            StartDatum = startDatum;
            EindDatum = eindDatum;
            AantalParkeerPlaatsen = aantalParkeerPlaatsen;
        }

        public int ContractNummer { get; set; }
        public DateOnly StartDatum { get; set; }
        public DateOnly EindDatum { get; set; }
        public int AantalParkeerPlaatsen { get; set; }

        public void ZetContractNummer(int contractNummer)
        {
            if (contractNummer == 0) { throw new ParkingcontractException("ParkeerContract - ZetContractNummer - Geen contractnummer"); }
            ContractNummer = contractNummer;
        }

        public void ZetStartDatum(DateOnly startDatum)
        {
            if (string.IsNullOrEmpty(startDatum.ToString())) { throw new ParkingcontractException("ParkeerContact - ZetStartDatum - Geen datum ingevuld"); }
            StartDatum = startDatum;
        }

        public void ZetEindDatum(DateOnly eindDatum, DateOnly startDatum)
        {
            if (eindDatum.GetHashCode() == 0) { throw new ParkingcontractException("ParkeerContact - ZetEindDatum - Geen datum ingevuld"); }
            if (startDatum > eindDatum) { throw new ParkingcontractException("ParkeerContract - ZetEindDatum - Einddatum kan niet voor de startdatum vallen"); }
            EindDatum = eindDatum;
        }
    }

}

