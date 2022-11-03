using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Numerics;

namespace UnitTestDomein {
    public class UnitTestBezoeker {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.ZetId(id);

            Assert.Equal(id, _josMetId.PersoonId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.Throws<PersoonException>(() => _josMetId.ZetId(id)); ;
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        [InlineData("Dirk Dirksen")]
        public void ZetNaam_Valid(string naamIn) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.ZetNaam(naamIn);

            Assert.Equal(naamIn.Trim(), _josMetId.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetNaam_Invalid(string naam) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.Throws<PersoonException>(() => _josMetId.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        public void ZetVoorNaam_Valid(string naamIn) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.ZetNaam(naamIn);
            
            Assert.Equal(naamIn.Trim(), _josMetId.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("Mike   Niels Joshua Robbe Miel")]
        public void ZetVoorNaam_Invalid(string naam) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.Throws<PersoonException>(() => _josMetId.ZetVoorNaam(naam));
        }

        [Theory]
        [InlineData("random.email@email.com")]
        [InlineData("    random.email@email.com")]
        [InlineData("random.email@email.com    ")]
        [InlineData("    random.email@email.com    ")]
        public void ZetEmail_Valid(string email)
        {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.ZetEmail(email);

            Assert.Equal(email.Trim(), _josMetId.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("mike")]
        [InlineData("mike@im")]
        [InlineData("@im")]
        [InlineData("mike@")]
        [InlineData("mike@.be")]
        public void ZetEmail_Invalid(string email) {
            Bezoeker b = new Bezoeker(3, "De Decker", "Mike", "ik@gmail.com", "Hogent");

            Assert.Throws<PersoonException>(() => b.ZetEmail(email));
        }

        [Theory]
        [InlineData("Telenet")]
        [InlineData("    Telenet")]
        [InlineData("Telenet    ")]
        [InlineData("    Telenet    ")]
        public void ZetBedrijf_Valid(string bedrijf) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf.Trim(), _josMetId.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid() {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.Throws<BezoekerException>(() => _josMetId.ZetBedrijf(null));
        }

        [Theory]
        [InlineData("Telenet")]
        [InlineData("    Telenet")]
        [InlineData("Telenet    ")]
        [InlineData("    HoGent    ")]
        public void VeranderBedrijf_Valid(string bedrijf) {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            _josMetId.VeranderBedrijf(bedrijf);

            Assert.Equal(bedrijf.Trim(), _josMetId.Bedrijf);
        }

        [Fact]
        public void VeranderBedrijf_Invalid_HetzelfdeBedrijf() {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.Throws<BezoekerException>(() => _josMetId.VeranderBedrijf("Bosteels"));
        }

        [Fact]
        public void IsDezelfde_Valid_MetId()
        {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.True(_josMetId.IsDezelfde(new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels")));
        }

        [Fact]
        public void IsDezelfde_Valid_ZonderId() {
            Bezoeker _josMetId = new Bezoeker("Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.True(_josMetId.IsDezelfde(new Bezoeker("Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels")));
        }

        [Theory] 
        [InlineData(13, "XXXXX", "Miel", "tm@tm.tm", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "XXXX", "tm@tm.tm", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "Miel", "xx@xx.xx", "hogent", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        [InlineData(13, "Troch", "Miel", "tm@tm.tm", "xxxxxx", 13, "Troch", "Miel", "tm@tm.tm", "hogent")]
        public void IsDezelfde_Invalid(int id1, string naam1, string voornaam1, string email1, string bedrijf1, int id2, string naam2, string voornaam2, string email2, string bedrijf2) {
            Bezoeker bezoeker1ZonderId = new Bezoeker(naam1, voornaam1, email1, bedrijf1);
            Bezoeker bezoeker1MetId = new Bezoeker(id1, naam1, voornaam1, email1, bedrijf1);
            Bezoeker bezoeker2ZonderId = new Bezoeker(naam2, voornaam2, email2, bedrijf2);
            Bezoeker bezoeker2MetId = new Bezoeker(id2, naam2, voornaam2, email2, bedrijf2);
            Assert.False(bezoeker1MetId.IsDezelfde(bezoeker2MetId));
            Assert.False(bezoeker1ZonderId.IsDezelfde(bezoeker2ZonderId));
            Assert.False(bezoeker1ZonderId.IsDezelfde(bezoeker2MetId));
            Assert.False(bezoeker2ZonderId.IsDezelfde(bezoeker1MetId));
        }

        [Fact]
        public void Equals_valid_BezoekerMetId() {
            Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");
            
            Assert.True(_josMetId.Equals(new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels")));
        }

        [Fact]
        public void Equals_valid_BezoekerZonderId() {
            Bezoeker _josMetId = new Bezoeker("Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");

            Assert.True(_josMetId.Equals(new Bezoeker("Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels")));
        }

        [Fact]
        public void Ctor_MetId_valid() {
            Bezoeker bezoeker = new Bezoeker(33, "Van Damme", "Magda", "info@info.info", "Pensioen");
            Assert.Equal(33, bezoeker.PersoonId);
            Assert.Equal("Van Damme", bezoeker.Naam);
            Assert.Equal("Magda", bezoeker.Voornaam);
            Assert.Equal("info@info.info", bezoeker.Email);
            Assert.Equal("Pensioen", bezoeker.Bedrijf);
        }

        [Fact]
        public void Ctor_ZondertId_valid() {
            Bezoeker bezoeker = new Bezoeker("Van Damme", "Magda", "info@info.info", "Pensioen");
            Assert.Equal("Van Damme", bezoeker.Naam);
            Assert.Equal("Magda", bezoeker.Voornaam);
            Assert.Equal("info@info.info", bezoeker.Email);
            Assert.Equal("Pensioen", bezoeker.Bedrijf);
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
        public void Ctor_MetId_invalid(int id, string voornaam, string achternaam, string email, string bedrijf) {
            Assert.ThrowsAny<PersoonException>(() => new Bezoeker(id, achternaam, voornaam, email, bedrijf));
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
        public void Ctor_ZonderId_invalid(string voornaam, string achternaam, string email, string bedrijf) {
            Assert.ThrowsAny<PersoonException>(() => new Bezoeker(achternaam, voornaam, email, bedrijf));
        }
    }
}