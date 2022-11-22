using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using Xunit;


namespace UnitTestManagers
{
    public class UnitTestVisitManager
    {
        [Theory]
        [InlineData(null)]
        public void AddVisitor_IsNull(Visit visit)
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.AddVisit(visit));
            Assert.Equal("VisitManager - AddVisit - Visit is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void AddVisitor_VisitExists(string visitorName, string visitorFirstName, string visitorEmail, string visitorCompany, string companyName, string companyVATnumber, string companyEmail, int personId, string employeeName, string employeeFirstName, DateTime startingtime)
        {
            Visitor visitor = new Visitor(visitorName, visitorFirstName, visitorEmail, visitorCompany);
            Company company = new Company(companyName, companyVATnumber, companyEmail);
            Employee employee = new Employee(personId, employeeName, employeeFirstName);
            Visit visit = new Visit(visitor, company, employee, startingtime);

            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            visitRepoMock.Setup(x => x.VisitExists(visit)).Returns(true);
            visitRepoMock.Setup(x => x.IsLoggedIn(visit.Visitor.Email)).Returns(true);
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.AddVisit(visit));
            Assert.Equal("VisitManager - AddVisit - Visit already exists", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void DeleteVisit_IsNull(Visit visit)
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.DeleteVisit(visit));
            Assert.Equal("VisitManager - DeleteVisit - Visit is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void DeleteVisit_VisitDoesNotExist(string visitorName, string visitorFirstName, string visitorEmail, string visitorCompany, string companyName, string companyVATnumber, string companyEmail, int personId, string employeeName, string employeeFirstName, DateTime startingtime)
        {
            Visitor visitor = new Visitor(visitorName, visitorFirstName, visitorEmail, visitorCompany);
            Company company = new Company(companyName, companyVATnumber, companyEmail);
            Employee employee = new Employee(personId, employeeName, employeeFirstName);
            Visit visit = new Visit(visitor, company, employee, startingtime);

            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            visitRepoMock.Setup(x => x.VisitExists(visit)).Returns(false);
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.DeleteVisit(visit));
            Assert.Equal("VisitManager - DeleteVisit - Visit does not exist", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void UpdateVisit_Invalid(Visit visit)
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.UpdateVisit(visit));
            Assert.Equal("VisitManager - UpdateVisit - Visit is null", ex.InnerException.Message);
        }

        [Fact]
        public void SearchVisits_InValid()
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.SearchVisits(null, null, null, null));
            Assert.Equal("VisitManager - SearchVisits - fields not filled in", ex.InnerException.Message);    
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void LogOutVisit_IsNull(string email)
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.LogoutVisit(email));
            Assert.Equal("No email data entry", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void LogOutBezoeken_EmailInValid(string email)
        {
            Mock<IVisitRepository> visitRepoMock = new Mock<IVisitRepository>();
            VisitManager VM = new VisitManager(visitRepoMock.Object);

            var ex = Assert.Throws<VisitManagerException>(() => VM.LogoutVisit(email));
            Assert.Equal("Invalid email", ex.Message);
        }
    }
}