using BL_Projectwerk.Domein;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Interfaces
{
    public interface IVisitorRepository
    {
        public bool VisitorExists(Visitor visitor);
        bool VisitorExists(int value);
        Visitor GetVisitor(int personId);
        List<Visitor> GetVisitors();
        IEnumerable<Visitor> GetVisitors(string? lastname, string? firstname, string? email, string? company);
        void UpdateVisitor(int visitorid, string? lastname, string? firstname, string? email, string? company);
        void DeleteVisitor(Visitor visitor);
        void AddVisitor(Visitor visitor);
    }
}
