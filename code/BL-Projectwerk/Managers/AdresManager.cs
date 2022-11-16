
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Managers
{
    public class AdresManager
    {
        private IAdresRepository adresRepo;
        private IBedrijfRepository bedrijfRepo;

        public AdresManager(IAdresRepository adresRepo, IBedrijfRepository bedrijfRepo)
        {
            this.adresRepo = adresRepo;
            this.bedrijfRepo = bedrijfRepo;
        }

        public int VoegAdresToe(Adres adres)
        {
            try
            {
                if (adres == null) throw new AdresManagerException("AdresManager - VoegAdresToe - Adres is null");
                if (!adresRepo.BestaatAdresZonderId(adres)) return adresRepo.VoegAdresToe(adres);
                else return adresRepo.GeefAdresId(adres); 
            }
            catch (Exception ex)
            {
                throw new AdresManagerException("VoegAdresToe", ex);
            }
        }

        public void VerwijderAdres(int adresId)
        {
            try
            {
                if (!adresRepo.BestaatAdresMetId(adresId))
                {
                    throw new AdresManagerException("AdresManager - VerwijderAdres - Onbestaand Adres");
                }
                else if (!bedrijfRepo.BedrijvenOpAdresAanwezig(adresId))
                {
                    throw new AdresManagerException("AdresManager - VerwijderAdres - Kan geen adres verwijderen waar er nog steeds bedrijven aanwezig zijn");
                }
                else adresRepo.VerwijderAdres(adresId);
            }
            catch(Exception ex)
            {
                throw new AdresManagerException("VerwijderAdres", ex);
            }
        }

        public void UpdateAdres(int id,string? straat,string? nummer,string? postcode,string? plaats, string? land)
        {
            try
            {
                //if (adres == null) throw new AdresManagerException("UpdateAdres");
                if (!adresRepo.BestaatAdresMetId(id)) throw new AdresManagerException("AdresManager - UpdateAdres - Adres bestaat niet");
                adresRepo.UpdateAdres(id,straat,nummer,postcode,plaats,land);
            }
            catch (Exception ex)
            {
                throw new AdresManagerException("UpdateAdres", ex);
            }
        }
    }
}
