using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces
{
    public interface IWerknemerRepository
    {
        bool BestaatWerknemer(Werknemer werknemer);
        Werknemer GeefWerknemer(int persoonId);
        Werknemer GeefWerknemers();
        IEnumerable<Werknemer> GeefWerknemers(string? naam, string? voornaam, Bedrijf? bedrijf, string? functie, string? email);
        void UpdateKlant(Werknemer werknemer);
        void VerwijderWerknemer(Werknemer werknemer);
        void VoegWerknemerToe(Werknemer werknemer);
    }
}
