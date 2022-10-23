
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;

namespace BL_Projectwerk.Managers
{
    public class BedrijfManager
    {
        private IBedrijfRepository bedrijfRepo;

        public BedrijfManager(IBedrijfRepository bedrijfRepo)
        {
            this.bedrijfRepo = bedrijfRepo;
        }

        public void VoegBedrijfToe(Bedrijf bedrijf)
        {
            try
            {
                if (bedrijf == null) throw new AdresManagerException("VoegAdresToe");
                if (bedrijfRepo.BestaatBedrijf(bedrijf)) throw new BedrijfManagerException("VoegBestellingToe");
                bedrijfRepo.VoegBedrijfToe(bedrijf);
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("VoegBedrijfToe", ex);
            }
        }

        public void VerwijderBedrijf(Bedrijf bedrijf)
        {
            try
            {
                if (!bedrijfRepo.BestaatBedrijf(bedrijf))
                {
                    throw new AdresManagerException("VerwijderBedrijf - Onbestaand Bedrijf");
                }
                else bedrijfRepo.VerwijderBedrijf(bedrijf);
            }
            catch (Exception ex)
            {
                throw new BedrijfManagerException("Verwijderedrijf", ex);
            }

        }
        public void UpdateBedrijf(Bedrijf bedrijf)
        {
            try
            {
                if (bedrijf == null) throw new AdresManagerException("UpdateBedrijf");
                if (bedrijfRepo.BestaatBedrijf(bedrijf)) throw new AdresManagerException("UpdateBedrijf - bedrijf is dezelfde");

                bedrijfRepo.UpdateBedrijf(bedrijf);
            }
            catch (Exception ex)
            {
                throw new AdresManagerException("UpdateBedrijf", ex);
            }

        }
    }
}
