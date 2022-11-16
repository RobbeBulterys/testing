using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein {
    public class UnitTestAdres {

        [Theory]
        [InlineData("Molenstraat", "1", "9000", "Gent", "België")]
        [InlineData("Kerkstraat            ", "999", "10  00", "Antwerpen            ", "Nederland            ")]
        [InlineData("Nieuw      straat", "9", "    8300", "Bru     gge", "Fra    nkrijk")]
        [InlineData("     Stationstraat    ", "9 bus 3", "9030", "       Brussel       ", "      Duitsland      ")]
        public void Constructor_ZonderId_Valid(string straat, string nummer, string postcode, string plaats, string land)
        {
            Adres a = new Adres(straat, nummer, postcode, plaats, land);

            Assert.Equal(straat.Trim(), a.Straat);
            Assert.Equal(nummer.Trim(), a.Nummer);
            Assert.Equal(postcode.Trim(), a.Postcode);
            Assert.Equal(plaats.Trim(), a.Plaats);
            Assert.Equal(land.Trim(), a.Land);
        }

        [Theory]
        [InlineData("", "", "", "", "")]
        [InlineData(" ", " ", " ", " ", " ")]
        [InlineData("\n", "\n", "\n", "\n", "\n")]
        [InlineData(null, null, null, null, null)]
        [InlineData("", "B", " ", "\n", null)]
        [InlineData(" ", "hallo", "\n", null, "")]
        [InlineData("\n", "CCC", null, "", " ")]
        [InlineData(null, "9B Wereldbus", "", " ", "\n")]
        public void Constructor_ZonderId_InValid(string straat, string nummer, string postcode, string plaats, string land)
        {
            Assert.ThrowsAny<AdresException>(() => new Adres(straat, nummer, postcode, plaats, land));
        }

        [Theory]
        [InlineData(1, "Molenstraat", "1", "9000", "Gent", "België")]
        [InlineData(2, "Kerkstraat            ", "999", "10  00", "Antwerpen            ", "Nederland            ")]
        [InlineData(69, "Nieuw      straat", "9B", "    8300", "Bru     gge", "Fra    nkrijk")]
        [InlineData(420, "     Stationstraat    ", "9B bus 3", "9030     ", "       Brussel       ", "      Duitsland      ")]
        public void Constructor_MetId_Valid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            Adres a = new Adres(id, straat, nummer, postcode, plaats, land);

            Assert.Equal(id, a.Id);
            Assert.Equal(straat.Trim(), a.Straat);
            Assert.Equal(nummer.Trim(), a.Nummer);
            Assert.Equal(postcode.Trim(), a.Postcode);
            Assert.Equal(plaats.Trim(), a.Plaats);
            Assert.Equal(land.Trim(), a.Land);
        }

        [Theory]
        [InlineData(0, "", "", "", "", "")]
        [InlineData(-1, " ", " ", " ", " ", " ")]
        [InlineData(-2, "\n", "\n", "\n", "\n", "\n")]
        [InlineData(-3, null, null, null, null, null)]
        [InlineData(-69, "", "B", " ", "\n", null)]
        [InlineData(-99, " ", "hallo", "\n", null, "")]
        [InlineData(-420, "\n", "CCC", null, "", " ")]
        [InlineData(-999, null, "9B Wereldbus", "", " ", "\n")]
        public void Constructor_MetId_InValid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            Assert.ThrowsAny<AdresException>(() => new Adres(id, straat, nummer, postcode, plaats, land));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetId(id);

            Assert.Equal(id, a.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetId(id));
            Assert.Equal("Adres - ZetId - Id ongeldig; Kleiner dan 1", ex.Message);
        }

        [Theory]
        [InlineData("Molenstraat")]
        [InlineData("Kerkstraat            ")]
        [InlineData("Nieuw      straat")]
        [InlineData("     Stationstraat    ")]
        public void ZetStraat_Valid(string straatnaam) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetStraat(straatnaam);

            Assert.Equal(straatnaam.Trim(), a.Straat);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetStraat_InValid(string straatnaam) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetStraat(straatnaam));
            Assert.Equal("Adres - ZetStraat - Geen straat ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("999")]
        [InlineData("9B")]
        [InlineData("9B bus 3")]
        public void ZetNummer_Valid(string nr) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetNummer(nr);

            Assert.Equal(nr, a.Nummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetNummer_IsNull(string nr)
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetNummer(nr));
            Assert.Equal("Adres - ZetNummer - Geen nummer ingevuld", ex.Message);

        }

        [Theory]
        [InlineData("B")]
        [InlineData("hallo")]
        [InlineData("CCC")]
        public void ZetNummer_OngeldigeSyntax(string nr) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<ControleException>(() => a.ZetNummer(nr));
            Assert.Equal("Controle - IsGoedeAdresNummerSyntax - Geen geldig nummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("9000")]
        [InlineData("10  00")]
        [InlineData("    8300")]
        public void ZetPostcode_Valid(string postcode)
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetPostcode(postcode);

            Assert.Equal(postcode.Trim(), a.Postcode);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetPostcode_InValid(string postcode)
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetPostcode(postcode));
            Assert.Equal("Adres - ZetPostcode - Geen postcode ingevuld", ex.Message);
        }
        [Theory]
        [InlineData("Gent")]
        [InlineData("Antwerpen            ")]
        [InlineData("Bru     gge")]
        [InlineData("       Brussel       ")]
        public void ZetPlaats_Valid(string plaats) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetPlaats(plaats);

            Assert.Equal(plaats.Trim(), a.Plaats);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetPlaats_InValid(string plaats) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetPlaats(plaats));
            Assert.Equal("Adres - ZetPlaats - Geen plaats ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("België")]
        [InlineData("Nederland            ")]
        [InlineData("Fra    nkrijk")]
        [InlineData("      Duitsland      ")]
        public void ZetLand_Valid(string land) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetLand(land);

            Assert.Equal(land.Trim(), a.Land);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetLand_InValid(string land) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AdresException>(() => a.ZetLand(land));
            Assert.Equal("Adres - ZetLand - Geen land ingevuld", ex.Message);
        }

        [Fact]
        public void IsDezelfde_Valid()
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.True(a.IsDezelfde(new Adres(1, "Kompasplein", "19", "9000", "Gent", "België")));
        }

        [Fact]
        public void IsDezelfde_InValid()
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.False(a.IsDezelfde(new Adres(1, "Kerkstraat", "15", "1000", "Brussel", "België")));
        }

    }
}