using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Managers
{
    public class WerknemerManager
    {
        private IWerknemerRepository _werknemerRepo;

        public WerknemerManager(IWerknemerRepository werknemerRepo)
        {
            _werknemerRepo = werknemerRepo;
        }

        public void VoegWerknemerToe(Werknemer werknemer)
        {
            try
            {
                if (werknemer == null) { throw new WerknemerManagerException("VoegWerknemerToe"); }
                if (_werknemerRepo.BestaatWerknemer(werknemer)) { throw new WerknemerManagerException("VoegWerknemerToe"); }
                _werknemerRepo.VoegWerknemerToe(werknemer);
            }
            catch (Exception ex)
            {
                throw new WerknemerManagerException("VoegWerknemerToe", ex);
            }

        }

        public void VerwijderWerknemer(Werknemer werknemer)
        {
            try
            {
                if (werknemer == null) { throw new WerknemerManagerException("VerwijderWerknemer"); }
                if (!_werknemerRepo.BestaatWerknemer(werknemer)) { throw new WerknemerManagerException("VerwijderWerknemer"); }
                _werknemerRepo.VerwijderWerknemer(werknemer);
            }
            catch (Exception ex)
            {
                throw new WerknemerManagerException("VerwijderWerknemer", ex);
            }
        }

        public void UpdateWerknemer(Werknemer werknemer)
        {
            try
            {
                if (_werknemerRepo.BestaatWerknemer(werknemer))
                {
                    Werknemer dbwerknemer = _werknemerRepo.GeefWerknemer(werknemer.PersoonId);
                    if (dbwerknemer.IsDezelfde(werknemer)) throw new WerknemerManagerException("UpdateWerknemer - werknemer is hetzelfde");
                    _werknemerRepo.UpdateKlant(werknemer);
                }
            }
            catch (Exception ex)
            {
                throw new WerknemerManagerException("UpdateWerknemer", ex);
            }
        }

        public IReadOnlyList<Werknemer> GeefWerknemers()
        {
            List<Werknemer> werknemers = new List<Werknemer>();
            try
            {
                werknemers.Add(_werknemerRepo.GeefWerknemers());
                return werknemers;
            }
            catch (Exception ex)
            {
                throw new WerknemerManagerException("AlleBezoekers", ex);
            }
        }
        //TODO: perfectioneren
        public IReadOnlyList<Werknemer> ZoekWerknemers(string? naam, string? voornaam, Bedrijf? bedrijf, string? functie, string? email)
        {
            List<Werknemer> bezoekers = new List<Werknemer>();

            try
            {
                if (!string.IsNullOrEmpty(naam) || !string.IsNullOrEmpty(voornaam) || !string.IsNullOrEmpty(email) || string.IsNullOrEmpty(functie) || bedrijf == null) 
                {
                    bezoekers.AddRange(_werknemerRepo.GeefWerknemers(naam, voornaam, bedrijf, functie, email));
                }
                else
                {
                    throw new WerknemerManagerException("ZoekWerknemers - Geen velden ingevuld");
                }
                return bezoekers;
            }
            catch (Exception ex)
            {
                throw new WerknemerManagerException("ZoekWerknemers", ex);
            }
        }

    }
}
