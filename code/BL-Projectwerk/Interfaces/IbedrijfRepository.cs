
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IBedrijfRepository
    {
        bool BedrijvenOpAdresAanwezig(int id);
        bool BestaatBedrijf(Bedrijf bedrijf);
        void UpdateBedrijf(Bedrijf bedrijf);
        void VerwijderBedrijf(Bedrijf bedrijf);
        void VoegBedrijfToe(Bedrijf bedrijf);
    }
}
