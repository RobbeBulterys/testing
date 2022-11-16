using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{
    public class UnitTestWerknemer
    {
        //private static readonly Bedrijf b_Bosteels = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
        //private static readonly Werknemer werknemer_Josh = new Werknemer(10, "Baetens", "Josh", b_Bosteels, "programmer");
        [Theory]
        [InlineData("Doe", "John")]
        [InlineData("Doe    ", "    Jane")]
        [InlineData("D   oe", "Ja  ke")]
        public void Constructor_ZonderId_Valid(string naam, string voornaam)
        {
            Werknemer w = new Werknemer(naam, voornaam);

            Assert.Equal(naam.Trim(), w.Naam);
            Assert.Equal(voornaam.Trim(), w.Voornaam);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\n", "\n")]
        [InlineData(null, null)]
        public void Constructor_ZonderId_InValid(string naam, string voornaam)
        {
            Assert.ThrowsAny<PersoonException>(() => new Werknemer(naam, voornaam));
        }

        [Theory]
        [InlineData(1, "Doe", "John")]
        [InlineData(69, "Doe    ", "    Jane")]
        [InlineData(420, "D   oe", "Ja  ke")]
        public void Constructor_MetId_Valid(int persoonId, string naam, string voornaam)
        {
            Werknemer w = new Werknemer(persoonId, naam, voornaam);

            Assert.Equal(persoonId, w.PersoonId);
            Assert.Equal(naam.Trim(), w.Naam);
            Assert.Equal(voornaam.Trim(), w.Voornaam);
        }

        [Theory]
        [InlineData(0, "", "")]
        [InlineData(-1, " ", " ")]
        [InlineData(-69, "\n", "\n")]
        [InlineData(-420, null, null)]
        public void Constructor_MetId_InValid(int persoonId, string naam, string voornaam)
        {
            Assert.ThrowsAny<PersoonException>(() => new Werknemer(persoonId, naam, voornaam));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id)
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersoonException>(() => _josMetId.ZetId(id));
            Assert.Equal("Persoon - ZetId - Id ongeldig; Kleiner dan 1", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        [InlineData("Dirk Dirksen")]
        public void ZetNaam_Valid(string naamIn)
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");

            _josMetId.ZetNaam(naamIn);

            Assert.Equal(naamIn.Trim(), _josMetId.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetNaam_Invalid(string naam)
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersoonException>(() => _josMetId.ZetNaam(naam));
            Assert.Equal("Persoon - ZetNaam - Geen naam ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        public void ZetVoorNaam_Valid(string voornaam)
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");

            _josMetId.ZetVoorNaam(voornaam);

            Assert.Equal(voornaam.Trim(), _josMetId.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetVoorNaam_Invalid(string naam)
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersoonException>(() => _josMetId.ZetVoorNaam(naam));
            Assert.Equal("Persoon - ZetVoorNaam - Geen voornaam ingevuld", ex.Message);
        }

        [Fact]
        public void IsDezelfde_Valid_()
        {
            Werknemer _josMetId = new Werknemer(8, "Joskens", "Jos");
            Assert.True(_josMetId.IsDezelfde(new Werknemer(8, "Joskens", "Jos")));
        }

        [Theory]
        [InlineData(13, "XXXXX", "Miel", 13, "Troch", "Miel")]
        [InlineData(13, "Troch", "XXXX", 13, "Troch", "Miel")]
        [InlineData(13, "Troch", "Miel", 1, "Troch", "Miel")]
        public void IsDezelfde_Invalid(int id1, string naam1, string voornaam1, int id2, string naam2, string voornaam2)
        {
            Werknemer werknemer1 = new Werknemer(id1, naam1, voornaam1);
            Werknemer werknemer2 = new Werknemer(id2, naam2, voornaam2);

            Assert.False(werknemer1.IsDezelfde(werknemer2));
        }
    }
}
