
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;

namespace BL_Projectwerk.Managers
{
    public class VisitorManager
    {
        private IVisitorRepository _visitorRepo;

        public VisitorManager(IVisitorRepository visitorRepo)
        {
            _visitorRepo = visitorRepo;
        }

        public void AddVisitor(Visitor visitor)
        {
            try
            {
                if (!_visitorRepo.VisitorExists(visitor))
                {
                    if (visitor == null) { throw new VisitorManagerException("VisitorManager - AddVisitor - Visitor is null"); }
                    _visitorRepo.AddVisitor(visitor);
                }
                
            
            }
            catch (Exception ex)
            {
                throw new VisitorManagerException("AddVisitor", ex);
            }
        }

        public void DeleteVisitor(Visitor visitor)
        {
            try
            {
                if (visitor == null) { throw new VisitorManagerException("VisitorManager - DeleteVisitor - No visitor data entry"); }
                if (!_visitorRepo.VisitorExists(visitor)) { throw new VisitorManagerException("VisitorManager - DeleteVisitor - Visitor does not exist"); }
                _visitorRepo.DeleteVisitor(visitor);
            } 
            catch (Exception ex)
            {
                throw new VisitorManagerException("DeleteVisitor", ex);
            }
        }

        public void UpdateVisitor(int visitorid, string? lastname, string? firstname, string? email, string? company)
        {
            if (visitorid == 0) throw new VisitorManagerException("VisitorManager - UpdateVisitor - No id data entry");
            //Bezoeker bezoeker = new Bezoeker(naam, voornaam, email, bedrijf);
            //Bezoeker dbBezoeker = _bezoekerRepo.GeefBezoeker(bezoekerid);
            //if (dbBezoeker.IsDezelfde(bezoeker)) throw new BezoekerManagerException("UpdateBezoeker - Bezoeker bestaat reeds");
            _visitorRepo.UpdateVisitor(visitorid, lastname, firstname, email, company);
        }

        public IReadOnlyList<Visitor> GetVisitors()
        {
            List<Visitor> visitors = new List<Visitor>();
            try
            {
                visitors = _visitorRepo.GetVisitors();
                return visitors;
            }
            catch (Exception ex)
            {
                throw new VisitorManagerException("GetVisitors", ex);
            }
        }

        public IReadOnlyList<Visitor> SearchVisitors(string? lastname, string? firstname, string? email, string? company)
        {
            List<Visitor> visitors = new List<Visitor>();
            try
            {
                if (!string.IsNullOrEmpty(lastname) || !string.IsNullOrEmpty(firstname) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(company) )
                {
                    visitors.AddRange(_visitorRepo.GetVisitors(lastname, firstname, email, company));
                }
                else
                {
                    throw new VisitorManagerException("VisitorManager - SearchVisitors - Empty fields");
                }
                return visitors;
            }
            catch (Exception ex)
            {
                throw new VisitorManagerException("SearchVisitors", ex);
            }
        }

        public bool VisitorExists(Visitor visitor)
        {
            return _visitorRepo.VisitorExists(visitor);
        }
    }
}
