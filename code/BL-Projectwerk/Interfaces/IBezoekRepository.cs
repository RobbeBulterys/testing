
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IBezoekRepository
    {
        bool BestaatBezoek(Bezoek bezoek);
        Bezoek GeefBezoek(int bezoekId);
        List<Bezoek> GeefBezoeken();
        List<Bezoek> GeefBezoeken(Bezoeker? bezoeker, Bedrijf? bedrijf, Werknemer? contactpersoon, string? StartTijd);
        void UpdateBezoek(Bezoek bezoek);
        void VerwijderBezoek(Bezoek bezoek);
        void VoegBezoekToe(Bezoek bezoek);
        void LogoutBezoek(string Email);
    }
}
