using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Numerics;

namespace UnitTestDomein {
    public class UnitTestBezoeker {
        private static readonly Bezoeker _josMetId = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");
        private static readonly Bezoeker _josMetIdDezelfdeAnderAdres = new Bezoeker(8, "Joskens", "Jos", "jjjjjjj@mm.com", "Bosteels");
        private static readonly Bezoeker _fredMetId = new Bezoeker(9, "Fred", "je", "fredje@fred.com", "Camping");
        private static readonly Bezoeker _LisaZonderId = new Bezoeker("Lisa", "Kep", "lisa@dmqklsf.com", "RS");
        private static readonly Bezoeker _LisaZonderIdDezelfdeAnderAdres = new Bezoeker("Lisa", "Kep", "lisa@dmqklsf.com", "RS");
        private static readonly Bezoeker _TianaZonderId = new Bezoeker("Tiana", "Roel", "tr@tr.me", "RS");
        private static readonly Bezoeker[] _bezoekers = new Bezoeker[] { _josMetId, _josMetIdDezelfdeAnderAdres, _fredMetId, _LisaZonderId, _TianaZonderId };

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ZetId_Valid(int id) {
            Assert.NotEqual(id, _josMetId.PersoonId);
            _josMetId.ZetId(id);
            Assert.Equal(id, _josMetId.PersoonId);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ZetId_InValid(int id) {
            Assert.Throws<PersoonException>(() => _josMetId.ZetId(id)); ;
        }

        [Theory]
        [InlineData("Dirk", "Dirk")]
        [InlineData("Dirk-Dirksen", "Dirk-Dirksen")]
        [InlineData("    Dirk   ", "Dirk")]
        [InlineData("    Dirk", "Dirk")]
        [InlineData("Dirk   ", "Dirk")]
        public void ZetNaam_Valid(string naamIn, string naamUit) {
            _josMetId.ZetNaam(naamIn);
            Assert.Equal(naamUit, _josMetId.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("Mike   Niels Joshua Robbe Miel")] // spaties mag vb "De Decker", dus dan is de controle maar aangepast op dubbele spatie.
        public void ZetNaam_Invalid(string naam) {
            Assert.Throws<PersoonException>(() => _josMetId.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Dirk", "Dirk")]
        [InlineData("Dirk-Dirksen", "Dirk-Dirksen")]
        [InlineData("    Dirk   ", "Dirk")]
        [InlineData("    Dirk", "Dirk")]
        [InlineData("Dirk   ", "Dirk")]
        public void ZetVoorNaam_Valid(string naamIn, string naamUit) {
            _josMetId.ZetNaam(naamIn);
            Assert.Equal(naamUit, _josMetId.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("Mike   Niels Joshua Robbe Miel")] // spaties mag vb "De Decker", dus dan is de controle maar aangepast op dubbele spatie.
        public void ZetVoorNaam_Invalid(string naam) {
            Assert.Throws<PersoonException>(() => _josMetId.ZetNaam(naam));
        }

        [Fact]
        public void ZetEmail_Valid() {
            Bezoeker b = _fredMetId.Clone();
            b.ZetEmail("random.email@email.com");
            Assert.Equal("random.email@email.com", b.Email);
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

        [Fact]
        public void ZetBedrijf_Valid() {
            Bezoeker b = _josMetId;
            string bedrijf = "Hogent brewery";
            Assert.NotEqual(bedrijf, b.Bedrijf);
            b.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf, b.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid() {
            Bezoeker b = _josMetId;

            Assert.Throws<BezoekerException>(() => b.ZetBedrijf(null));
        }

        [Fact]
        public void VeranderBedrijf_Valid() {
            Bezoeker b = _josMetId;
            string bedrijf = "hogent jajaja";
            Assert.NotEqual(bedrijf, b.Bedrijf);

            b.VeranderBedrijf(bedrijf);
            Assert.Equal(bedrijf, b.Bedrijf);
        }

        [Fact]
        public void VeranderBedrijf_Invalid_HetzelfdeBedrijf() {
            Bezoeker b = _josMetId;
            string bedrijf1 = "hogent";
            string bedrijf2 = "hogent";
            b.ZetBedrijf(bedrijf1);
            Assert.Equal(bedrijf1, b.Bedrijf);
            Assert.Throws<BezoekerException>(() => b.VeranderBedrijf(bedrijf2));
        }

        [Fact]
        public void IsDezelfde_Valid_MetId() {
            Bezoeker bezoeker1 = _josMetId;
            Bezoeker bezoeker2 = _josMetId.Clone();
            Assert.True(bezoeker1.IsDezelfde(bezoeker2));
        }

        [Fact]
        public void IsDezelfde_Valid_ZonderId() {
            Bezoeker bezoeker1 = _LisaZonderId;
            Bezoeker bezoeker2 = _LisaZonderIdDezelfdeAnderAdres;
            Assert.True(bezoeker1.IsDezelfde(bezoeker2));
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
            Bezoeker bezoeker1 = _josMetId;
            Bezoeker bezoeker2 = _josMetIdDezelfdeAnderAdres;
            Assert.True(bezoeker1.Equals(bezoeker2));
        }

        [Fact]
        public void Equals_valid_BezoekerZonderId() {
            Bezoeker bezoeker1 = _LisaZonderId;
            Bezoeker bezoeker2 = _LisaZonderIdDezelfdeAnderAdres;
            Assert.True(bezoeker1.Equals(bezoeker2));
        }

        [Fact]
        public void Equals_Invalid_GeenBezoeker() {
            Assert.False(_josMetId.Equals(null));
            Assert.False(_fredMetId.Equals(new Werknemer("Jansens", "Jan", new Bedrijf("Bosteels", "BE0123123123", "info@info.info"), "FLM Brouwzaal")));
            Assert.False(_LisaZonderId.Equals("strings"));
        }

        // Constructors, Clone, check of alles van persoon getest is
        [Fact]
        public void Clone_valid() {
            Bezoeker bezoeker = _TianaZonderId;
            Bezoeker tianaClone = bezoeker.Clone();
            Assert.True(tianaClone.IsDezelfde(bezoeker));

            Bezoeker bezoekerMetid = _fredMetId;
            Bezoeker fredClone = bezoekerMetid.Clone();
            Assert.True(fredClone.IsDezelfde(bezoekerMetid));
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