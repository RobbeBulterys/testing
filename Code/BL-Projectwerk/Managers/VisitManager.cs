
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System.ServiceModel.Channels;

namespace BL_Projectwerk.Managers
{
    public class VisitManager
    {
        private IVisitRepository _visitRepo;

        public VisitManager(IVisitRepository visitRepo)
        {
            _visitRepo = visitRepo;
        }

        public void AddVisit(Visit visit)
        {
            try
            {
                if (visit == null)  throw new VisitManagerException("VisitManager - AddVisit - Visit is null"); 
                if (_visitRepo.VisitExists(visit))  throw new VisitManagerException("VisitManager - AddVisit - Visit already exists");
                if (_visitRepo.IsLoggedIn(visit.Visitor.Email)) throw new VisitManagerException("VisitManager - AddVisit - Visitor Already Logged In");
                _visitRepo.AddVisit(visit);
            } 
            catch (Exception ex)
            {
                throw new VisitManagerException("AddVisit", ex);
            }
        }

        public void DeleteVisit(Visit visit)
        {
            try
            {
                if (visit == null)  throw new VisitManagerException("VisitManager - DeleteVisit - Visit is null"); 
                if (!_visitRepo.VisitExists(visit))  throw new VisitManagerException("VisitManager - DeleteVisit - Visit does not exist"); 
                _visitRepo.DeleteVisit(visit);
            } 
            catch (Exception ex)
            {
                throw new VisitManagerException("DeleteVisit", ex);
            } 
        }

        public void UpdateVisit(Visit visit) 
        {
            try
            {

                if (visit == null) { throw new VisitManagerException("VisitManager - UpdateVisit - Visit is null"); }
                Visit dbVisit = _visitRepo.GetVisit(visit.VisitId);
                if (dbVisit.HasSameProperties(visit)) throw new VisitManagerException("VisitManager - UpdateVisit - Visit is the same");
                _visitRepo.UpdateVisit(visit);
            } 
            catch (Exception)
            {
                throw;
            }     
        }

        public IReadOnlyList<Visit> GetVisits()
        {
            try
            {
                List<Visit> visits = _visitRepo.GetVisits().ToList();
                return visits;
            } 
            catch (Exception ex)
            {
                throw new VisitManagerException("GetVisits", ex);
            }
        }

        //TODO: Filteren perfectioneren
        public IReadOnlyList<Visit> SearchVisits(Visitor? Visitor, Company? company, Employee? contact, string? Startingtime)
        {
            try
            {
                List<Visit> visits = new List<Visit>();
                if (Visitor != null || company != null || contact != null || Startingtime != null)
                {
                    visits = _visitRepo.GetVisits(Visitor, company, contact, Startingtime);
                } else
                {
                    throw new VisitManagerException("VisitManager - SearchVisits - fields not filled in");
                }
                return visits;
            } 
            catch (Exception ex)
            {
                throw new VisitManagerException("SearchVisits", ex);
            }
        }

        public IReadOnlyList<Visit> SearchSpecificVisits(string Startingtime, string Endingtime)
        {
            try
            {
                List<Visit> visits = new List<Visit>();
                if (Startingtime != null && Endingtime != null)
                {
                    visits = _visitRepo.GetSpecificVisits(Startingtime, Endingtime);
                }
                else
                {
                    throw new VisitManagerException("VisitManager - SearchVisits - fields not filled in");
                }
                return visits;
            }
            catch (Exception ex)
            {
                throw new VisitManagerException("SearchVisits", ex);
            }
        }

        public void LogoutVisit(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) { throw new VisitManagerException("No email data entry"); }
                if (!Verify.IsValidEmailSyntax(email)) { throw new VisitManagerException("Invalid email");  }
                if (!_visitRepo.IsLoggedIn(email)) throw new VisitManagerException("Is not logged in");
                _visitRepo.LogoutVisit(email);
            } 
            catch (VisitManagerException)
            {
                throw;
            }
            
        }

        public void AlreadyLoggedIn(string email) {
            try {
                if (_visitRepo.IsLoggedIn(email)) throw new VisitManagerException("Is Already Logged In");
            } catch (Exception) {
                throw;
            }
        }
    }
}
