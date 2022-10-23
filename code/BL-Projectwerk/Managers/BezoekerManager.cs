
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;

namespace BL_Projectwerk.Managers
{
    public class BezoekerManager
    {
        private IBezoekerRepository _bezoekerRepo;

        public BezoekerManager(IBezoekerRepository bezoekerRepo)
        {
            _bezoekerRepo = bezoekerRepo;
        }

        public void VoegBezoekerToe(Bezoeker bezoeker)
        {
            try
            {
                if (bezoeker == null) { throw new BezoekerManagerException("VoegBezoekerToe - Geen Bezoeker gegeven"); }
                if (_bezoekerRepo.BestaatBezoeker(bezoeker)) { throw new BezoekerManagerException("VoegBezoekerToe - Bezoeker Bestaat al"); }
                _bezoekerRepo.VoegBezoekerToe(bezoeker);
            }
            catch (Exception ex)
            {
                throw new BezoekerManagerException("VoegBezoekerToe", ex);
            }

        }

        public void VerwijderBezoeker(Bezoeker bezoeker)
        {
            try
            {
                if (bezoeker == null) { throw new BezoekerManagerException("VerwijderBezoeker - Geen bezoeker ingevuld"); }
                if (!_bezoekerRepo.BestaatBezoeker(bezoeker)) { throw new BezoekerManagerException("VerwijderBezoeker - Bezoeker dat je wil verwijderen bestaat niet"); }
            } 
            catch (Exception ex)
            {
                throw new BezoekerManagerException("VerwijderBezoeker", ex);
            }
        }

        public void UpdateBezoeker(Bezoeker bezoeker)
        {
            if (bezoeker == null) throw new BezoekerManagerException("UpdateBezoeker - Geen bezoeker ingevuld");
            Bezoeker dbBezoeker = _bezoekerRepo.GeefBezoeker(bezoeker.PersoonId);
            if (dbBezoeker.HeefZelfdeProperties(bezoeker)) throw new BezoekerManagerException("UpdateBezoeker - Bezoeker bestaat reeds");
            _bezoekerRepo.UpdateBezoeker(bezoeker);
        }

        public Bezoeker GeefDetailsBezoeker()
        {
            return _bezoekerRepo.GeefBezoeker();
        }

        public IReadOnlyList<Bezoeker> GeefBezoekers()
        {
            List<Bezoeker> bezoekers = new List<Bezoeker>();
            try
            {
                bezoekers.Add(_bezoekerRepo.GeefBezoekers());
                return bezoekers;
            }
            catch (Exception ex)
            {
                throw new BezoekerManagerException("AlleBezoekers", ex);
            }
        }
        //TODO: perfectioneren
        public IReadOnlyList<Bezoeker> ZoekBezoekers(string? naam, string? voornaam, string? email, string? bedrijf)
        {
            List<Bezoeker> bezoekers = new List<Bezoeker>();

            try
            {
                if (!string.IsNullOrEmpty(naam) || !string.IsNullOrEmpty(voornaam) || !string.IsNullOrEmpty(email) || string.IsNullOrEmpty(bedrijf))
                {
                    bezoekers.AddRange(_bezoekerRepo.GeefBezoekers(naam, voornaam, email, bedrijf));
                }
                else
                {
                    throw new BezoekerManagerException("ZoekBezoekers- Geen velden ingevuld");
                }
                return bezoekers;
            }
            catch (Exception ex)
            {
                throw new BezoekerManagerException("ZoekBezoekers", ex);
            }
        }
    }
}
