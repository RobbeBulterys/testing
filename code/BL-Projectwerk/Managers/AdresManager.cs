
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Managers
{
    public class AdresManager
    {
        private IAdresRepository adresRepo;

        public AdresManager(IAdresRepository adresRepo)
        {
            this.adresRepo = adresRepo;
        }

        public void VoegAdresToe(Adres adres)
        {
            try
            {
                if (adres == null) throw new AdresManagerException("VoegAdresToe");
                if (adresRepo.BestaatAdres(adres)) throw new AdresManagerException("VoegAdresToe - adres bestaat al");
                adresRepo.VoegAdresToe(adres);
            }
            catch (Exception ex)
            {
                throw new AdresManagerException("VoegAdresToe", ex);
            }
        }

        public void VerwijderAdres(Adres adres)
        {
            try
            {
                if (!adresRepo.BestaatAdres(adres))
                {
                    throw new AdresManagerException("VerwijderAdres - Onbestaand Adres");
                }
                else adresRepo.VerwijderAdres(adres);
            }
            catch(Exception ex)
            {
                throw new AdresManagerException("VerwijderAdres", ex);
            }
        }

        public void UpdateAdres(Adres adres)
        {
            try
            {
                if (adres == null) throw new AdresManagerException("UpdateAdres");
                if (adresRepo.BestaatAdres(adres)) throw new AdresManagerException("UpdateAdres - adres is dezelfde");

                adresRepo.UpdateAdres(adres);
            }
            catch (Exception ex)
            {
                throw new AdresManagerException("UpdateAdres", ex);
            }
        }
    }
}
