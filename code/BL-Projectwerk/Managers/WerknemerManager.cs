using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Managers {
    public class WerknemerManager {
        private IWerknemerRepository _werknemerRepo;

        public WerknemerManager(IWerknemerRepository werknemerRepo) {
            _werknemerRepo = werknemerRepo;

        }

        

        public void VoegWerknemerToe(Werknemer werknemer) {
            try {
                if (werknemer == null) { throw new WerknemerManagerException("VoegWerknemerToe"); }
                //if (_werknemerRepo.BestaatWerknemer(werknemer.Naam, werknemer.Voornaam)) { throw new WerknemerManagerException("VoegWerknemerToe"); }
                _werknemerRepo.VoegWerknemerToe(werknemer);
            } catch (Exception ex) {
                throw new WerknemerManagerException("VoegWerknemerToe", ex);
            }

        }

        public bool BestaatWerknemer(int werknemerId) {
            try {
                if (werknemerId == 0) throw new WerknemerManagerException("BestaatWerknemer");
                return _werknemerRepo.BestaatWerknemer(werknemerId);
            } catch (Exception ex) {
                throw new WerknemerManagerException("BestaatWerknemer", ex);
            }
        }

        public bool BestaatWerknemer(string naam, string voornaam) {
            try {
                if (string.IsNullOrEmpty(naam) && string.IsNullOrEmpty(voornaam)) throw new WerknemerManagerException("BestaatWerknemer");
                return _werknemerRepo.BestaatWerknemer(naam, voornaam);
            } catch (Exception ex) {
                throw new WerknemerManagerException("BestaatWerknemer", ex);
            }
        }

        public void VerwijderWerknemer(int werknemerId) {
            try {
                if (werknemerId == 0) { throw new WerknemerManagerException("VerwijderWerknemer"); }
                if (!_werknemerRepo.BestaatWerknemer(werknemerId)) { throw new WerknemerManagerException("VerwijderWerknemer"); }
                _werknemerRepo.VerwijderWerknemer(werknemerId);
            } catch (Exception ex) {
                throw new WerknemerManagerException("VerwijderWerknemer", ex);
            }
        }

        public void UpdateWerknemer(int werknemerId, string? naam, string? voornaam) {
            try {
                if (string.IsNullOrWhiteSpace(naam) && string.IsNullOrWhiteSpace(voornaam)) throw new WerknemerManagerException("UpdateWerknemer - Geen updates ingevuld");
                if (!string.IsNullOrWhiteSpace(naam) || !string.IsNullOrWhiteSpace(voornaam)) {
                    if (_werknemerRepo.BestaatWerknemer(werknemerId)) {
                        Werknemer dbwerknemer = _werknemerRepo.GeefWerknemer(werknemerId);
                        Werknemer update = new Werknemer(werknemerId, dbwerknemer.Naam, dbwerknemer.Voornaam);
                        if (!string.IsNullOrWhiteSpace(naam)) { update.ZetNaam(naam); }
                        if (!string.IsNullOrWhiteSpace(voornaam)) { update.ZetVoorNaam(voornaam); }
                        if (dbwerknemer.IsDezelfde(update)) throw new WerknemerManagerException("UpdateWerknemer - werknemer is hetzelfde");
                    }
                }
                _werknemerRepo.UpdateWerknemer(werknemerId, naam, voornaam);
            } catch (Exception ex) {
                throw new WerknemerManagerException("UpdateWerknemer", ex);
            }
        }

        public IReadOnlyList<Werknemer> GeefAlleWerknemers() {
            List<Werknemer> werknemers = new List<Werknemer>();
            try {
                werknemers.AddRange(_werknemerRepo.GeefWerknemers());
                return werknemers;
            } catch (Exception ex) {
                throw new WerknemerManagerException("GeefBezoekers", ex);
            }
        }

        public IReadOnlyList<Werknemer> GeefWerknemersVoorBedrijf(int bedrijfsId) {
            throw new NotImplementedException();
        }


        //TODO: perfectioneren
        public IReadOnlyList<Werknemer> ZoekWerknemers(string? naam, string? voornaam) {
            List<Werknemer> werknemers = new List<Werknemer>();

            try {
                if (!string.IsNullOrEmpty(naam) || !string.IsNullOrEmpty(voornaam)) {
                    werknemers.AddRange(_werknemerRepo.GeefWerknemers(naam, voornaam));
                } else {
                    throw new WerknemerManagerException("ZoekWerknemers - Geen velden ingevuld");
                }
                return werknemers;
            } catch (Exception ex) {
                throw new WerknemerManagerException("ZoekWerknemers", ex);
            }
        }

        public Werknemer GeefWerknemer(int werknemerId)
        {
            Werknemer werknemer = null;
            try
            {
                if (werknemerId == 0 || werknemerId < 0) { throw new WerknemerManagerException("GeefWerknemer"); }
                werknemer = _werknemerRepo.GeefWerknemer(werknemerId);
                return werknemer;
                
            } catch (Exception ex)
            {
                throw new WerknemerManagerException("GeefWerknemer", ex);
            }
        } 

    }
}
