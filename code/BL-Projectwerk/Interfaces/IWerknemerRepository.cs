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
        bool BestaatWerknemer(int id);
        bool BestaatWerknemer(string naam, string voornaam);
        Werknemer GeefWerknemer(int persoonId);
        List<Werknemer> GeefWerknemers();
        IEnumerable<Werknemer> GeefWerknemers(string? naam, string? voornaam);
        void UpdateWerknemer(int werknemerId, string? naam, string? voornaam);
        void VerwijderWerknemer(int WerknemerId);
        void VoegWerknemerToe(Werknemer werknemer);
    }
}
