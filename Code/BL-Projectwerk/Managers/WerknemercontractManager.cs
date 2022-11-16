using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Managers {
    public class WerknemercontractManager 
    {
        private IWerknemercontractRepository _wcRepo;

        public WerknemercontractManager(IWerknemercontractRepository werknemercontractRepo) 
        {
            _wcRepo = werknemercontractRepo;
        }

        public bool BestaatContract(int werknemerId, int bedrijfsId) 
        {
            try 
            {
                if (werknemerId <= 0 || bedrijfsId <= 0) throw new WerknemercontractManagerException("WerknemercontractManager - BestaatContract - Ongeldige id's"); 
                return _wcRepo.BestaatContract(werknemerId, bedrijfsId);
            } 
            catch (Exception ex) {
                throw new WerknemercontractManagerException("BestaatContract", ex);
            }
        } // getest

        //public IReadOnlyList<Werknemercontract> GeefContracten() {
        //    List<Werknemercontract> contracten = new List<Werknemercontract>();
        //    try {
        //        contracten.AddRange(_wcRepo.GeefContracten());
        //        return contracten;
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("GeefContracten", ex);
        //    }
        //}

        public IReadOnlyList<Werknemercontract> GeefContractenVanWerknemer(Werknemer werknemer) 
        {
            List<Werknemercontract> contracten = new List<Werknemercontract>();
            try 
            {
                contracten.AddRange(_wcRepo.GeefContractenVanWerknemer(werknemer));
                return contracten;

            } 
            catch (Exception ex) {
                throw new WerknemercontractManagerException("GeefContractenVanWerknemer", ex);
            }
        }  // getest

        public IReadOnlyList<Werknemercontract> GeefContractenVanBedrijf(Bedrijf bedrijf) 
        {
            List<Werknemercontract> contracten = new List<Werknemercontract>();
            try 
            {
                contracten.AddRange(_wcRepo.GeefContractenVanBedrijf(bedrijf));
                return contracten;
            } 
            catch (Exception ex) {
                throw new WerknemercontractManagerException("geefcontractenvanbedrijf", ex);
            }
        } // getest

        //public bool HeeftBedrijfContracten(int bedrijfsId) {
        //    try {
        //        if (bedrijfsId <= 0) { throw new WerknemercontractManagerException("HeeftBedrijfContracten - id <= 0"); }
        //        return _wcRepo.HeeftWerknemerContracten(bedrijfsId);
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("HeeftBedrijfContracten", ex);
        //    }
        //}

        //public bool HeeftWerknemerContracten(int werknemerId) {
        //    try {
        //        if (werknemerId <= 0) { throw new WerknemercontractManagerException("HeeftWerknemerContracten - id <= 0"); }
        //        return _wcRepo.HeeftWerknemerContracten(werknemerId);
        //    } catch (Exception ex) {
        //        throw new WerknemercontractManagerException("HeeftWerknemerContracten", ex);
        //    }
        //}

        public void UpdateContract(Werknemer werknemer, Bedrijf bedrijf, string? functie, string? email) 
        {
            if (werknemer.PersoonId <= 0 || bedrijf.Id <= 0) throw new WerknemercontractManagerException("UpdateContract - werknemerid of bedrijfsid ongeldig"); 
            if (!BestaatContract(werknemer.PersoonId, bedrijf.Id)) throw new WerknemercontractManagerException("UpdateContract - contract bestaat niet"); 
            try 
            {
                if (!string.IsNullOrWhiteSpace(functie) || !string.IsNullOrWhiteSpace(email)) {
                    // een van de twee is ingevuld en veranderd
                    Werknemercontract contractDB = _wcRepo.GeefContract(werknemer, bedrijf);

                    // Overnemen van contractDB (clonen)
                    Werknemercontract update = new Werknemercontract(contractDB.Bedrijf, contractDB.Werknemer, contractDB.Functie);
                    if (contractDB.Email != null) update.ZetEmail(contractDB.Email); 

                    // Wijzigingen aanpassen
                    if (!string.IsNullOrWhiteSpace(functie)) update.ZetFunctie(functie);
                    if (!string.IsNullOrWhiteSpace(email)) update.ZetEmail(email);

                    // Controleren of het dezelfde is
                    if (contractDB.IsDezelfde(update)) throw new WerknemercontractManagerException("UpdateContract - contract is niet gewijzigd");

                    // contract is gewijzigd
                    _wcRepo.UpdateContract(werknemer.PersoonId, bedrijf.Id, functie, email);

                } 
                else throw new WerknemercontractManagerException("UpdateContract - geen parameters functie of email ingevuld"); 
            } 
            catch (Exception ex) {
                throw new WerknemercontractManagerException("UpdateContract", ex);
            }
        } // valid paden getest

        public void VerwijderContract(Werknemercontract contract) 
        {
            try 
            {
                if (contract == null) throw new WerknemercontractManagerException("WerknemercontractManager - VerwijderContract - Geen contract ingevuld"); 
                if (!_wcRepo.BestaatContract(contract.Werknemer.PersoonId, contract.Bedrijf.Id)) throw new WerknemercontractManagerException("WerknemercontractManager - VerwijderContract - Contract bestaat niet"); 
                _wcRepo.VerwijderContract(contract.Werknemer.PersoonId, contract.Bedrijf.Id);
            } 
            catch (Exception ex) 
            {
                throw new WerknemercontractManagerException("VerwijderContract", ex);
            }
        } // getest

        public void VoegContractToe(Werknemercontract contract) 
        {
            try 
            {
                if (contract == null) throw new WerknemercontractManagerException("WerknemercontractManager - VoegContractToe - Geen contract gegeven");
                if (_wcRepo.BestaatContract(contract.Werknemer.PersoonId, contract.Bedrijf.Id)) throw new WerknemercontractManagerException("WerknemercontractManager - VoegContractToe - Werknemercontract bestaat al");
                _wcRepo.VoegContractToe(contract);
            } 
            catch (Exception ex) 
            {
                throw new WerknemercontractManagerException("VoegContractToe", ex);
            }
        } // getest


    }
}
