
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System.ServiceModel.Channels;

namespace BL_Projectwerk.Managers
{
    public class BezoekManager
    {
        private IBezoekRepository _bezoekRepo;

        public BezoekManager(IBezoekRepository bezoekRepo)
        {
            _bezoekRepo = bezoekRepo;
        }

        public void VoegBezoekToe(Bezoek bezoek)
        {
            try
            {
                if (bezoek == null)  throw new BezoekManagerException("BezoekManager - VoegBezoek - Bezoek is null"); 
                if (_bezoekRepo.BestaatBezoek(bezoek))  throw new BezoekManagerException("BezoekManager - VoegBezoekToe - Bezoek bestaat al");
                if (_bezoekRepo.IsLoggedIn(bezoek.Bezoeker.Email)) throw new BezoekManagerException("VoegBezoekToe - VoegBezoekToe - Bezoeker is reeds ingelogd");
                _bezoekRepo.VoegBezoekToe(bezoek);
            } 
            catch (Exception ex)
            {
                throw;
            }
        }

        public void VerwijderBezoek(Bezoek bezoek)
        {
            try
            {
                if (bezoek == null)  throw new BezoekManagerException("BezoekManager - VerwijderBezoek - Bezoek is null"); 
                if (!_bezoekRepo.BestaatBezoek(bezoek))  throw new BezoekManagerException("BezoekManager - VerwijderBezoek - Onbestaand bezoek"); 
                _bezoekRepo.VerwijderBezoek(bezoek);
            } 
            catch (Exception ex)
            {
                throw new BezoekManagerException("VerwijderBezoek", ex);
            } 
        }

        public void UpdateBezoek(Bezoek bezoek) 
        {
            try
            {

                if (bezoek == null) { throw new BezoekManagerException("BezoekManager - UpdateBezoek - Bezoek is null"); }
                Bezoek dbBezoek = _bezoekRepo.GeefBezoek(bezoek.BezoekId);
                if (dbBezoek.IsDezelfde(bezoek)) throw new BezoekManagerException("BezoekManager - UpdateBezoek - Bezoek is dezelfde");
                _bezoekRepo.UpdateBezoek(bezoek);
            } 
            catch (Exception ex)
            {
                throw new BezoekManagerException("UpdateBezoek", ex);
            }     
        }

        public IReadOnlyList<Bezoek> GeefBezoeken()
        {
            try
            {
                List<Bezoek> bezoeken = _bezoekRepo.GeefBezoeken().ToList();
                return bezoeken;
            } 
            catch (Exception ex)
            {
                throw new BezoekManagerException("GeefBezoeken", ex);
            }
        }

        //TODO: Filteren perfectioneren
        public IReadOnlyList<Bezoek> ZoekBezoeken(Bezoeker? bezoeker, Bedrijf? bedrijf, Werknemer? contactpersoon, string? Starttijd)
        {
            try
            {
                List<Bezoek> bezoeken = new List<Bezoek>();
                if (bezoeker != null || bedrijf != null || contactpersoon != null || Starttijd != null)
                {
                    bezoeken = _bezoekRepo.GeefBezoeken(bezoeker, bedrijf, contactpersoon, Starttijd);
                } else
                {
                    throw new BezoekManagerException("BezoekManager - ZoekBezoeker - Geen veld ingevuld");
                }
                return bezoeken;
            } 
            catch (Exception ex)
            {
                throw new BezoekManagerException("ZoekBezoeken", ex);
            }
        }

        public void LogoutBezoek(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) { throw new BezoekManagerException("Geen email ingevuld"); }
                if (!Controle.IsGoedeEmailSyntax(email)) { throw new BezoekManagerException("Ongeldige email");  }
                if (!_bezoekRepo.IsLoggedIn(email)) { throw new BezoekManagerException("not logged in"); }
                _bezoekRepo.LogoutBezoek(email);
            } 
            catch (BezoekManagerException)
            {
                throw;
            }
            
        }

        public bool IsLoggedIn(string email) {
            try {
                return _bezoekRepo.IsLoggedIn(email);
            } catch (Exception) {
                throw;
            }
        }
    }
}
