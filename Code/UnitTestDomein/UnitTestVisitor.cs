using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Numerics;

namespace UnitTestDomein 
{
    public class UnitTestVisitor 
    {
        [Fact]
        public void Constructor_WithoutId_Valid()
        {
            Visitor visitor = new Visitor("Van Damme", "Magda", "info@info.info", "Pensioen");
            Assert.Equal("Van Damme", visitor.LastName);
            Assert.Equal("Magda", visitor.FirstName);
            Assert.Equal("info@info.info", visitor.Email);
            Assert.Equal("Pensioen", visitor.Company);
        }

        [Theory]
        [InlineData("", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("   ", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("\n", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("     \r        ", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(null, "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", "", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", "   ", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", "\n", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", "   \r ", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", null, "jan.janssens@gmail.com", "Bosteels")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmailcom", "Bosteels")]
        [InlineData("Jan", "Janssens", "", "Bosteels")]
        [InlineData("Jan", "Janssens", " ", "Bosteels")]
        [InlineData("Jan", "Janssens", "\n", "Bosteels")]
        [InlineData("Jan", "Janssens", "  \r   ", "Bosteels")]
        [InlineData("Jan", "Janssens", null, "Bosteels")]
        [InlineData("Jan", "Janssens", "jan@", "Bosteels")]
        [InlineData("Jan", "Janssens", "@gmailcom", "Bosteels")]
        [InlineData("Jan", "Janssens", "jan.janssens@.gmailcom", "Bosteels")]
        [InlineData("Jan", "Janssens", "jan@gmailcom", "Bosteels")]
        [InlineData("Jan", "Janssens", "jangmailcom", "Bosteels")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", "")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", " ")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", "\n")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", "\t")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", "   \r")]
        [InlineData("Jan", "Janssens", "jan.janssens@gmail.com", null)]
        public void Constructor_WithoutId_InValid(string firstname, string lastname, string email, string company)
        {
            Assert.ThrowsAny<Exception>(() => new Visitor(lastname, firstname, email, company));
        }

        [Fact]
        public void Constructor_WithId_Valid()
        {
            Visitor visitor = new Visitor(33, "Van Damme", "Magda", "info@info.info", "Pensioen");
            Assert.Equal(33, visitor.PersonId);
            Assert.Equal("Van Damme", visitor.LastName);
            Assert.Equal("Magda", visitor.FirstName);
            Assert.Equal("info@info.info", visitor.Email);
            Assert.Equal("Pensioen", visitor.Company);
        }

        [Theory]
        [InlineData(-1, "Jan", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(0, "Jan", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "   ", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "\n", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "     \r        ", "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, null, "Janssens", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", "", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", "   ", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", "\n", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", "   \r ", "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", null, "jan.janssens@gmail.com", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmailcom", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", " ", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "\n", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "  \r   ", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", null, "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jan@", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "@gmailcom", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@.gmailcom", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jan@gmailcom", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jangmailcom", "Bosteels")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", "")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", " ")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", "\n")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", "\t")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", "   \r")]
        [InlineData(1, "Jan", "Janssens", "jan.janssens@gmail.com", null)]
        public void Constructor_WithId_InValid(int id, string firstname, string lastname, string email, string company)
        {
            Assert.ThrowsAny<Exception>(() => new Visitor(id, lastname, firstname, email, company));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void SetId_Valid(int id) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.SetId(id);

            Assert.Equal(id, josWithId.PersonId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void SetId_InValid(int id) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<PersonException>(() => josWithId.SetId(id));
            Assert.Equal("Person - SetId - Id invalid; Less than 1", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        [InlineData("Dirk Dirksen")]
        public void SetLastName_Valid(string name) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.SetLastName(name);

            Assert.Equal(name.Trim(), josWithId.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetLastName_Invalid(string naam) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<PersonException>(() => josWithId.SetLastName(naam));
            Assert.Equal("Person - SetLastName - No lastname data entry", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        public void SetFirstName_Valid(string firstname) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.SetFirstName(firstname);
            
            Assert.Equal(firstname.Trim(), josWithId.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetFirstName_Invalid(string name) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<PersonException>(() => josWithId.SetFirstName(name));
            Assert.Equal("Person - SetFirstName - No firstname data entry", ex.Message);
        }

        [Theory]
        [InlineData("random.email@email.com")]
        [InlineData("    random.email@email.com")]
        [InlineData("random.email@email.com    ")]
        [InlineData("    random.email@email.com    ")]
        public void SetEmail_Valid(string email)
        {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.SetEmail(email);

            Assert.Equal(email.Trim(), josWithId.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetEmail_IsNull(string email) {
            Visitor b = new Visitor(3, "De Decker", "Mike", "ik@gmail.com", "Hogent");

            var ex = Assert.Throws<PersonException>(() => b.SetEmail(email));
            Assert.Equal("Person - SetEmail - No email data entry", ex.Message);
        }

        [Theory]
        [InlineData("mike")]
        [InlineData("mike@im")]
        [InlineData("@im")]
        [InlineData("mike@")]
        [InlineData("mike@.be")]
        public void SetEmail_InValidSyntax(string email)
        {
            Visitor b = new Visitor(3, "De Decker", "Mike", "ik@gmail.com", "Hogent");

            var ex = Assert.Throws<VerifyException>(() => b.SetEmail(email));
            Assert.Equal("Person - SetEmail - Invalid email", ex.Message);
        }

        [Theory]
        [InlineData("Telenet")]
        [InlineData("    Telenet")]
        [InlineData("Telenet    ")]
        [InlineData("    Telenet    ")]
        public void SetCompany_Valid(string company) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.SetCompany(company);

            Assert.Equal(company.Trim(), josWithId.Company);
        }

        [Fact]
        public void SetCompany_InValid() {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<VisitorException>(() => josWithId.SetCompany(null));
            Assert.Equal("Visitor - SetCompany - No company data entry", ex.Message);
        }

        [Theory]
        [InlineData("Telenet")]
        [InlineData("    Telenet")]
        [InlineData("Telenet    ")]
        [InlineData("    HoGent    ")]
        public void ChangeCompany_Valid(string company) {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            josWithId.ChangeCompany(company);

            Assert.Equal(company.Trim(), josWithId.Company);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ChangeCompany_IsNull(string company)
        {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<VisitorException>(() => josWithId.ChangeCompany(company));
            Assert.Equal("Visitor - ChangeCompany - No company data entry", ex.Message);
        }

        [Fact]
        public void ChangeCompany_IsTheSame() {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            var ex = Assert.Throws<VisitorException>(() => josWithId.ChangeCompany("Bosteels"));
            Assert.Equal("Visitor - ChangeCompany - Company is the same", ex.Message);
        }

        [Fact]
        public void HasSameProperties_Valid()
        {
            Visitor josWithId = new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.True(josWithId.HasSameProperties(new Visitor(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels")));
        }

        [Theory] 
        [InlineData(13, "XXXXX", "Miel", "tm@tm.tm", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "XXXX", "tm@tm.tm", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "Miel", "xx@xx.xx", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "Miel", "tm@tm.tm", "xxxxxx", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        public void HasSameProperties_Invalid(int id1, string naam1, string voornaam1, string email1, string bedrijf1, int id2, string naam2, string voornaam2, string email2, string bedrijf2) {
            Visitor bezoeker1 = new Visitor(id1, naam1, voornaam1, email1, bedrijf1);
            Visitor bezoeker2 = new Visitor(id2, naam2, voornaam2, email2, bedrijf2);

            Assert.False(bezoeker1.HasSameProperties(bezoeker2));
        }
    }
}