using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces {
    public interface IWerknemercontractRepository {
        bool BestaatContract(int werknemerId, int bedrijfsId);
        Werknemercontract GeefContract(Werknemer werknemer, Bedrijf bedrijf);
        //IEnumerable<Werknemercontract> GeefContracten();
        IEnumerable<Werknemercontract> GeefContractenVanBedrijf(Bedrijf bedrijf);
        IEnumerable<Werknemercontract> GeefContractenVanWerknemer(Werknemer werknemer);
        //bool HeeftWerknemerContracten(int bedrijfsId);
        void UpdateContract(int werknemerId, int bedrijfsId, string? functie, string? email);
        void VerwijderContract(int werknemerId, int bedrijfsId);
        void VoegContractToe(Werknemercontract contract);
    }
}
