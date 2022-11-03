
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;

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
                if (bezoek == null)  throw new BezoekerManagerException("is al bezoek wollah"); 
                if (_bezoekRepo.BestaatBezoek(bezoek))  throw new BezoekerManagerException("VoegBezoekToe - bezoek bestaat al"); 

                _bezoekRepo.VoegBezoekToe(bezoek);
            } catch (Exception ex)
            {
                throw new BezoekManagerException("VoegBezoekToe", ex);
            }
        }

        public void VerwijderBezoek(Bezoek bezoek)
        {
            try
            {
                if (bezoek == null)  throw new BezoekManagerException("VerwijderBezoek - Geen verwijderbaar bezoek ingegeven"); 
                if (!_bezoekRepo.BestaatBezoek(bezoek))  throw new BezoekManagerException("VerwijderBezoek - Bezoek bestaat niet"); 
                _bezoekRepo.VerwijderBezoek(bezoek);
            } catch (Exception ex)
            {
                throw new BezoekManagerException("VerwijderBezoek", ex);
            } 
        }

        public void UpdateBezoek(Bezoek bezoek) 
        {
            try
            {

                if (bezoek == null) { throw new BezoekManagerException("UpdateBezoek - Geen aanpasbare bezoek ingegeven"); }
                Bezoek dbBezoek = _bezoekRepo.GeefBezoek(bezoek.BezoekId);
                if (dbBezoek.IsDezelfde(bezoek)) throw new BezoekManagerException("UpdateBezoek - bezoek heeft dezelfde properties");
                _bezoekRepo.UpdateBezoek(bezoek);
            } catch (Exception ex)
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
            } catch (Exception ex)
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
                    throw new BezoekManagerException("ZoekBezoeker - Geen veld ingevuld");
                }
                return bezoeken;
            } catch (Exception ex)
            {
                throw new BezoekManagerException("ZoekBezoeken", ex);
            }
        }

        public void LogoutBezoek(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) { throw new BezoekManagerException("LogoutBezoek - Geen email ingevuld"); }
                if (!Controle.IsGoedeEmailSyntax(email)) { throw new BezoekerManagerException("LogoutBezoek - ongeldige email");  }
                _bezoekRepo.LogoutBezoek(email);
            } 
            catch (Exception ex)
            {
                throw new BezoekManagerException("LogoutBezoek", ex);
            }
        }
    }
}
