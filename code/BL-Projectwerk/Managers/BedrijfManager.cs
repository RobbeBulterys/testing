﻿
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System.IO;

namespace BL_Projectwerk.Managers
{
    public class BedrijfManager
    {
        private IBedrijfRepository bedrijfRepo;
        private AdresManager AM = null;
        private WerknemercontractManager WCM = null;

        public BedrijfManager(IBedrijfRepository bedrijfRepo, AdresManager am, WerknemercontractManager wCM)
        {
            this.bedrijfRepo = bedrijfRepo;
            AM = am;
            WCM = wCM;  
        }

        public void VoegBedrijfToe(string btwnummer, string naam, string email, string? telefoon,string? land, string? straat, string? nummer, string? postcode,string? plaats)
        {
            int? adresId = null;
            try
            {
                if ((AM != null)) 
                {
                    if (!string.IsNullOrWhiteSpace(land) && !string.IsNullOrWhiteSpace(straat) && !string.IsNullOrWhiteSpace(nummer) && !string.IsNullOrWhiteSpace(postcode) && !string.IsNullOrWhiteSpace(plaats))
                    {
                        Adres adresZonderId = new Adres(straat, nummer, postcode, plaats, land);
                        adresId = AM.VoegAdresToe(adresZonderId);
                        adresZonderId.ZetId(adresId.Value);
                    }
                }
                Bedrijf bedrijf = new Bedrijf(naam, btwnummer, email);
                if (bedrijfRepo.BestaatBedrijfZonderId(btwnummer,naam,email)) throw new BedrijfManagerException("BedrijfManager - VoegBedrijfToe - Bedrijf bestaat al");
                bedrijfRepo.VoegBedrijfToe(btwnummer,naam,email,telefoon,adresId);
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("VoegBedrijfToe", ex);
            }
        }

        public void VerwijderBedrijf(Bedrijf bedrijf, int? adresid) 
        {
            try
            {
                if (!bedrijfRepo.BestaatBedrijfMetId(bedrijf.Id))
                {
                    throw new BedrijfManagerException("BedrijfManager - VerwijderBedrijf - Onbestaand bedrijf");
                }
                else 
                {
                    if (adresid.HasValue)
                    {
                        AM.VerwijderAdres((int)adresid);
                    }
                    IReadOnlyList<Werknemercontract> contracten = WCM.GeefContractenVanBedrijf(bedrijf);
                    foreach (Werknemercontract WC in contracten)
                    {
                        WCM.VerwijderContract(WC);
                    }
                    bedrijfRepo.VerwijderBedrijf(bedrijf.Id);
                }
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("Verwijderedrijf", ex);
            }

        }

        public void UpdateBedrijf(int id,string? btwnummer, string? naam, string? email, string? telefoon)
        {
            try
            {
                if (!bedrijfRepo.BestaatBedrijfMetId(id)) throw new BedrijfManagerException("BedrijfManager - UpdateBedrijf - Bedrijf is dezelfde");
                bedrijfRepo.UpdateBedrijf(id,btwnummer,naam,email,telefoon);
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("UpdateBedrijf", ex);
            }
        }

        public void UpdateBedrijfAdres(int id, int adresId)
        {
            try
            {
                if (!bedrijfRepo.BestaatBedrijfMetId(id)) throw new BedrijfManagerException("BedrijfManager - UpdateBedrijf - Bedrijf is dezelfde");
                bedrijfRepo.UpdateBedrijfAdres(id, adresId);
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("UpdateBedrijfAdres", ex);
            }
        }

        public IReadOnlyList<Bedrijf> GeefBedrijven()
        {
            List<Bedrijf> Bedrijven = new List<Bedrijf>();
            try
            {
                Bedrijven = bedrijfRepo.GeefBedrijven();
                return Bedrijven;
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("GeefBedrijven", ex);
            }
        }

        public IReadOnlyList<Bedrijf> ZoekBedrijven(string? btwnummer, string? naam, string? email, string? telefoon)
        {
            List<Bedrijf> Bedrijven = new List<Bedrijf>();
            //List<Bedrijf> Bedrijven2 = new List<Bedrijf>();
            try
            {
                Bedrijven.AddRange(bedrijfRepo.ZoekBedrijven(btwnummer, naam, email, telefoon));
                //foreach (Bedrijf bedrijf in Bedrijven)
                //{
                //    Bedrijven2.Add(bedrijfRepo.GeefBedrijfOpId(bedrijf.Id));
                //}
                return Bedrijven;
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("ZoekBedrijven", ex);
            }
        }
    }
}
