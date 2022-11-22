using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL_Projectwerk.Domein {
    public class Visit 
    {
        public Visit(Visitor visitor, Company company, Employee contactId, DateTime startingTime) 
        {
            SetVisitor(visitor);
            SetCompany(company);
            SetContact(contactId);
            SetStartingTime(startingTime);
        }

        public Visit(int visitId, Visitor visitor, Company company, Employee contactId, DateTime startingTime)
        {
            SetId(visitId);
            SetVisitor(visitor);
            SetCompany(company);
            SetContact(contactId);
            SetStartingTime(startingTime);
        }

        public Visit(int visitId, Visitor visitor, Company company, Employee contactId, DateTime startingTime, DateTime endingTime) 
        {
            SetId(visitId);
            SetVisitor(visitor);
            SetCompany(company);
            SetContact(contactId);
            SetStartingTime(startingTime);
            SetEndingTime(endingTime, startingTime);
        }

        public int VisitId { get; set; }
        public Visitor Visitor { get; set; }
        public Company Company { get; set; }
        public Employee Contact { get; set; }
        public DateTime StartingTime { get; set; }
        public DateTime EndingTime { get; set; }

        public void SetId(int id) 
        {
            if (id < 1) { throw new VisitException("Visit - SetId - Id invalid; Less than 1"); }
            VisitId = id;
        }

        public void SetVisitor(Visitor visitor) 
        {
            if (visitor == null) { throw new VisitException("Visit - SetVisitor - No visitor data entry"); }
            Visitor = visitor;
        }

        public void SetCompany(Company company) 
        {
            if (company == null) { throw new VisitException("Visit - SetCompany - No company data entry"); }
            Company = company;
        }

        public void ChangeCompany(Company company) 
        {
            if (company == null) { throw new VisitException("Visit - ChangeCompany - No company data entry"); }
            if (company.Equals(Company)) { throw new VisitException("Visit - ChangeCompany - Company is the same"); }
            Company = company;
        }

        public void SetContact(Employee contact) 
        {
            if (contact == null) { throw new VisitException("Visit - SetContact - No contact data entry"); }
            Contact = contact;
        }

        public void ChangeContact(Employee contact) 
        {
            if (contact == null) { throw new VisitException("Visit - ChangeContact - No contact data entry"); }
            if (contact.Equals(Contact)) { throw new VisitException("Visit - ChangeContact - Contact is the same"); }
            Contact = contact;
        }

        public void ChangeVisitor(Visitor newVisitor) 
        {
            if (newVisitor == null) { throw new VisitException("Visit - ChangeVisitor - No visitor data entry"); }
            if (newVisitor.Equals(Visitor)) { throw new VisitException("Visit - ChangeVisitor - Visitor is the same"); }
            Visitor = newVisitor;
        }

        public void SetStartingTime(DateTime startingtime) 
        {
            // TODO: Eventueel extra controles (bvb voor het jaar 2000 mag niet)
            // if (starttijd < DateTime.Now) { throw new BezoekException("Bezoek - ZetStartTijd - starttijd kan niet in het verleden zijn"); }
            // dat datetime met hashcode = 0 een niet ingevoerde datetime is
            if (startingtime.GetHashCode() == 0) { throw new VisitException("Visit - SetStartingTime - no startingtime data entry"); }
            // TODO: controle als tijd hetzelfde is exception
            StartingTime = startingtime;
        }

        public void SetEndingTime(DateTime endingtime, DateTime startingtime) 
        {
            if (endingtime.GetHashCode() == 0) { throw new VisitException("Visit - SetEndingTime - No endingtime data entry"); }
            if (endingtime < startingtime) { throw new VisitException("Visit - SetEndingTime - Startingtime must always be earlier than the endingtime"); }
            // TODO: controle als tijd hetzelfde is exception
            EndingTime = endingtime;
        }

        public bool HasSameProperties(Visit visit)
        {
            return VisitId == visit.VisitId &&
                   visit.Visitor.HasSameProperties(Visitor) &&
                   visit.Company.HasSameProperties(Company) &&
                   visit.Contact.HasSameProperties(Contact) &&
                   StartingTime == visit.StartingTime &&
                   EndingTime == visit.EndingTime;
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(VisitId, Visitor, Company, Contact, StartingTime, EndingTime);
        }
    }
}
