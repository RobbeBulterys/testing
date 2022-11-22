using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{

    public class UnitTestVisit
    {
        [Fact]
        public void Constructor_WithoutId_Valid()
        {
            Visitor visitor = new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent");
            Company company = new Company(1, "Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be");
            Employee employee = new Employee(1, "Bert", "Bertens");
            DateTime startingtime = DateTime.Today;
            Visit b = new Visit(visitor, company, employee, startingtime);

            Assert.Equal(visitor, b.Visitor);
            Assert.Equal(company, b.Company);
            Assert.Equal(employee, b.Contact);
            Assert.Equal(startingtime, b.StartingTime);
        }

        [Fact]
        public void Constructor_WithoutId_InValid()
        {
            Visitor visitor = null;
            Company company = null;
            Employee employee = null;
            DateTime startingtime = new DateTime();

            Assert.ThrowsAny<VisitException>(() => new Visit(visitor, company, employee, startingtime));
        }

        [Fact]
        public void Constructor_WithId_Valid()
        {
            int id = 1;
            Visitor visitor = new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent");
            Company company = new Company(1, "Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be");
            Employee employee = new Employee(1, "Bert", "Bertens");
            DateTime startingtime = DateTime.Today;
            DateTime endingtime = startingtime.AddHours(4);
            Visit b = new Visit(id, visitor, company, employee, startingtime, endingtime);

            Assert.Equal(id, b.VisitId);
            Assert.Equal(visitor, b.Visitor);
            Assert.Equal(company, b.Company);
            Assert.Equal(employee, b.Contact);
            Assert.Equal(startingtime, b.StartingTime);
            Assert.Equal(endingtime, b.EndingTime);
        }

        [Fact]
        public void Constructor_WithId_InValid()
        {
            int id = 0;
            Visitor visitor = null;
            Company company = null;
            Employee employee = null;
            DateTime startingtime = new DateTime();
            DateTime endingtime = new DateTime();

            Assert.ThrowsAny<VisitException>(() => new Visit(id, visitor, company, employee, startingtime, endingtime));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void SetId_Valid(int id)
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.SetId(id);

            Assert.Equal(id, visit.VisitId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void SetId_InValid(int id)
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetId(id));
            Assert.Equal("Visit - SetId - Id invalid; Less than 1", ex.Message);
        }

        [Fact]
        public void SetVisitor_Valid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.SetVisitor(new Visitor("Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Visitor("Niels", "Nelson", "nn@nn.na", "hogent"), visit.Visitor);
        }

        [Fact]
        public void SetVisitor_InValid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetVisitor(null));
            Assert.Equal("Visit - SetVisitor - No visitor data entry", ex.Message);
        }

        [Fact]
        public void SetContact_Valid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.SetContact(new Employee("tom", "vdw"));

            Assert.Equal(new Employee("tom", "vdw"), visit.Contact);
        }

        [Fact]
        public void SetContact_InValid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetContact(null));
            Assert.Equal("Visit - SetContact - No contact data entry", ex.Message);
        }

        [Fact]
        public void SetCompany_Valid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            Company company = new Company(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            visit.SetCompany(company);

            Assert.Equal(company, visit.Company);
        }

        [Fact]
        public void SetCompany_InValid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetCompany(null));
            Assert.Equal("Visit - SetCompany - No company data entry", ex.Message);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        [InlineData("22/5/2001")]
        public void SetStartingTime_Valid(string startingtime)
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.SetStartingTime(DateTime.Parse(startingtime));

            Assert.Equal(DateTime.Parse(startingtime), visit.StartingTime);
        }

        private DateTime startingtime;
        [Fact]
        public void SetStartingTime_InValid()
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            

            var ex = Assert.Throws<VisitException>(() => visit.SetStartingTime(startingtime));
            Assert.Equal("Visit - SetStartingTime - no startingtime data entry", ex.Message);
        }

        [Fact]
        public void SetEndingTime_Valid()
        {
            string startingtime = "2023/03/21 05:23:12";
            string endingtime = "2023/03/23 05:24:12";

            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.SetEndingTime(DateTime.Parse(endingtime), DateTime.Parse(startingtime));

            Assert.Equal(DateTime.Parse(endingtime), visit.EndingTime);
        }

        private DateTime endingtime;
        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        public void SetEndingTime_NotFiledIn(string startingtime)
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetEndingTime(endingtime, DateTime.Parse(startingtime)));
            Assert.Equal("Visit - SetEndingTime - No endingtime data entry", ex.Message);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12", "2023/03/19 05:23:12")]
        public void SetEndingTime_BeforeStartingtime(string startingtime, string endingtime)
        {
            Visit visit = new Visit(8, new Visitor("Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.SetEndingTime(DateTime.Parse(endingtime), DateTime.Parse(startingtime)));
            Assert.Equal("Visit - SetEndingTime - Startingtime must always be earlier than the endingtime", ex.Message);
        }

        [Fact]
        public void ChangeVisitor_Valid()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.ChangeVisitor(new Visitor(2, "Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Visitor(2, "Niels", "Nelson", "nn@nn.na", "hogent"), visit.Visitor);
        }

        [Fact]
        public void ChangeVisitor_IsTheSame()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));


            var ex = Assert.Throws<VisitException>(() => visit.ChangeVisitor(new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent")));
            Assert.Equal("Visit - ChangeVisitor - Visitor is the same", ex.Message);
        }

        [Fact]
        public void ChangeVisitor_Isnull()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.ChangeVisitor(null));
            Assert.Equal("Visit - ChangeVisitor - No visitor data entry", ex.Message);
        }

        [Fact]
        public void ChangeCompany_Valid()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            Company company = new Company("hogent", "BE0231231231", "info@hogent.be");

            visit.ChangeCompany(company);

            Assert.Equal(company, visit.Company);
        }

        [Fact]
        public void ChangeCompany_IsTheSame()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.ChangeCompany(new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be")));
            Assert.Equal("Visit - ChangeCompany - Company is the same", ex.Message);
        }

        [Fact]
        public void ChangeCompany_IsNull()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.ChangeCompany(null));
            Assert.Equal("Visit - ChangeCompany - No company data entry", ex.Message);
        }

        [Fact]
        public void ChangeContact_Valid()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            visit.ChangeContact(new Employee("tom", "vdw"));

            Assert.Equal(new Employee("tom", "vdw"), visit.Contact);
        }

        [Fact]
        public void ChangeContact_IsTheSame()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.ChangeContact(new Employee("Bert", "Bertens")));
            Assert.Equal("Visit - ChangeContact - Contact is the same", ex.Message);
        }

        [Fact]
        public void ChangeContact_IsNull()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<VisitException>(() => visit.ChangeContact(null));
            Assert.Equal("Visit - ChangeContact - No contact data entry", ex.Message);
        }

        [Fact]
        public void HasSameProperties_Valid()
        {
            Visit visit = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            
            Assert.True(visit.HasSameProperties(new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8))));
        }

        [Fact]
        public void HasSameProperties_Invalid()
        {
            Visit visit1 = new Visit(8, new Visitor(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Company("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Employee("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            Visit visit2 = new Visit(88, new Visitor(10, "John", "Doe", "john@doe.me", "kwak"), new Company("Hogent", "BE0424524527", "info@hogent.be"), new Employee("Jos", "Josens"), DateTime.Today.AddDays(4), DateTime.Today.AddDays(4).AddHours(8));

            Assert.False(visit1.HasSameProperties(visit2));
        }
    }
}