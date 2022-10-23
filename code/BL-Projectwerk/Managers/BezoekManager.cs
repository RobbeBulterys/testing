
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
                throw new BezoekManagerException("VerwijderBezoeker", ex);
            } 
        }

        public void UpdateBezoek(Bezoek bezoek) 
        {
            try
            {

                if (bezoek == null) { throw new BezoekManagerException("UpdateBezoek - Geen aanpasbare bezoek ingegeven"); }
                Bezoek dbBezoek = _bezoekRepo.GeefBezoek(bezoek.BezoekId);
                if (dbBezoek.HeeftZelfdeProperties(bezoek)) throw new BezoekManagerException("UpdateBezoek - bezoek heeft dezelfde properties");
                _bezoekRepo.UpdateBezoek(bezoek);
            } catch (Exception ex)
            {
                throw new BezoekManagerException("VerwijderBezoeker", ex);
            }     
        }

        public IReadOnlyList<Bezoek> GeefBezoeken()
        {
            try
            {
                List<Bezoek> bezoeken = new List<Bezoek>();
                bezoeken.Add(_bezoekRepo.GeefBezoeken());
                return bezoeken;
            } catch (Exception ex)
            {
                throw new BezoekManagerException("GeefBezoeken", ex);
            }
        }

        //TODO: Filteren perfectioneren
        public IReadOnlyList<Bezoek> ZoekBezoeken(Bezoeker? bezoeker, Bedrijf? bedrijf, Werknemer? contactpersoon)
        {
            try
            {
                List<Bezoek> bezoeken = new List<Bezoek>();
                if (bezoeker != null || bedrijf != null || contactpersoon != null )
                {
                    bezoeken.Add(_bezoekRepo.GeefBezoeken(bezoeker, bedrijf, contactpersoon));
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

    }
}
