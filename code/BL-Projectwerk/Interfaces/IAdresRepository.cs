
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IAdresRepository
    {
        bool BestaatAdres(Adres adres);
        void UpdateAdres(Adres adres);
        void VerwijderAdres(Adres adres);
        void VoegAdresToe(Adres adres);
    }
}
