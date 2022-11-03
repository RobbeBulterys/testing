
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IBedrijfRepository
    {
        bool BedrijvenOpAdresAanwezig(int id);
        bool BestaatBedrijfZonderId(string btwnummer, string naam, string email);
        bool BestaatBedrijfMetId(int id);
        void UpdateBedrijfAdres(int id, int adresId);
        void UpdateBedrijf(int id, string? btwnummer, string? naam, string? email, string? telefoon);
        void VerwijderBedrijf(int id);
        void VoegBedrijfToe(string btwnummer, string naam, string email, string? telefoon, int? adresId);
        List<Bedrijf> GeefBedrijven();
        List<Bedrijf> ZoekBedrijven(string? btwnummer, string? naam, string? email, string? telefoon);
        Bedrijf GeefBedrijfOpId(int id);
    }
}
