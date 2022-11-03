
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
                _bezoekerRepo.VerwijderBezoeker(bezoeker);
            } 
            catch (Exception ex)
            {
                throw new BezoekerManagerException("VerwijderBezoeker", ex);
            }
        }

        public void UpdateBezoeker(int bezoekerid, string? naam, string? voornaam, string? email, string? bedrijf)
        {
            if (bezoekerid == 0) throw new BezoekerManagerException("UpdateBezoeker - Geen ID ingevuld");
            //Bezoeker bezoeker = new Bezoeker(naam, voornaam, email, bedrijf);
            //Bezoeker dbBezoeker = _bezoekerRepo.GeefBezoeker(bezoekerid);
            //if (dbBezoeker.IsDezelfde(bezoeker)) throw new BezoekerManagerException("UpdateBezoeker - Bezoeker bestaat reeds");
            _bezoekerRepo.UpdateBezoeker(bezoekerid, naam, voornaam, email, bedrijf);
        }


        public IReadOnlyList<Bezoeker> GeefBezoekers()
        {
            List<Bezoeker> bezoekers = new List<Bezoeker>();
            try
            {
                bezoekers = _bezoekerRepo.GeefBezoekers();
                return bezoekers;
            }
            catch (Exception ex)
            {
                throw new BezoekerManagerException("AlleBezoekers", ex);
            }
        }


        public IReadOnlyList<Bezoeker> ZoekBezoekers(string? naam, string? voornaam, string? email, string? bedrijf)
        {
            List<Bezoeker> bezoekers = new List<Bezoeker>();

            try
            {
                if (!string.IsNullOrEmpty(naam) || !string.IsNullOrEmpty(voornaam) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(bedrijf) )
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

        public bool BestaatBezoeker(Bezoeker bezoeker)
        {
            return _bezoekerRepo.BestaatBezoeker(bezoeker);
        }
    }
}
