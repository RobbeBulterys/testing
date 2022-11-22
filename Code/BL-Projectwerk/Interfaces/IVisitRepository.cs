
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IVisitRepository
    {
        bool VisitExists(Visit visit);
        Visit GetVisit(int visitId);
        List<Visit> GetVisits();
        List<Visit> GetVisits(Visitor? visitor, Company? company, Employee? contact, string? StartingTime);
        void UpdateVisit(Visit visit);
        void DeleteVisit(Visit visit);
        void AddVisit(Visit visit);
        void LogoutVisit(string Email);
        bool IsLoggedIn(string email);
        List<Visit> GetSpecificVisits(string startingtime, string endingtime);
    }
}
