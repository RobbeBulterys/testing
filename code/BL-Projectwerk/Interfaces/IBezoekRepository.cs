
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IBezoekRepository
    {
        bool BestaatBezoek(Bezoek bezoek);
        Bezoek GeefBezoek(int bezoekId);
        Bezoek GeefBezoeken();
        Bezoek GeefBezoeken(Bezoeker? bezoeker, Bedrijf? bedrijf, Werknemer? contactpersoon);
        void UpdateBezoek(Bezoek bezoek);
        void VerwijderBezoek(Bezoek bezoek);
        void VoegBezoekToe(Bezoek bezoek);
    }
}
