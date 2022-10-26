
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IAdresRepository
    {
        bool BestaatAdresZonderId(Adres adres);
        bool BestaatAdresMetId(int id);

        void UpdateAdres(int id, string? straat, string? nummer, string? postcode, string? plaats, string? land);
        void VerwijderAdres(int id);
        Adres VoegAdresToe(Adres adres);
        Adres GeefAdresMetId(Adres adres);
    }
}
