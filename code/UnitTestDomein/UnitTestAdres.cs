using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein {
    public class UnitTestAdres {

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

            Assert.Throws<AdresException>(() => a.ZetId(id));
        }

        [Theory]
        [InlineData("Molenstraat")]
        [InlineData("Kerkstraat            ")]
        [InlineData("           Nieuwstraat")]
        [InlineData("     Statio   nstraat    ")]
        public void ZetStraat_Valid(string straatnaam) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetStraat(straatnaam);

            Assert.Equal(straatnaam.Trim(), a.Straat);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetStraat_InValid(string straatnaam) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.Throws<AdresException>(() => a.ZetStraat(straatnaam));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
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
        public void ZetNummer_InValid_AdresException(string nr)
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.Throws<AdresException>(() => a.ZetNummer(nr));
        }

        [Theory]
        [InlineData("B")]
        [InlineData("hallo")]
        [InlineData("CCC")]
        [InlineData("9B Wereldbus")]
        public void ZetNummer_InValid_ControleException(string nr) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.Throws<ControleException>(() => a.ZetNummer(nr));
        }
        [Theory]
        [InlineData("9000")]
        [InlineData("10  00")]
        [InlineData("    8300")]
        public void ZetPostcode_Valid(string postcode)
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetPostcode(postcode);

            Assert.Equal(postcode, a.Postcode);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void ZetPostcode_InValid(string postcode)
        {

        }
        [Theory]
        [InlineData("Gent")]
        [InlineData("Antwerpen            ")]
        [InlineData("               Brugge")]
        [InlineData("       Brussel       ")]
        public void ZetPlaats_Valid(string plaats) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetPlaats(plaats);

            Assert.Equal(plaats.Trim(), a.Plaats);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetPlaats_InValid(string plaats) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.Throws<AdresException>(() => a.ZetPlaats(plaats));
        }

        [Theory]
        [InlineData("België")]
        [InlineData("Nederland            ")]
        [InlineData("            Frankrijk")]
        [InlineData("      Duitsland      ")]
        public void ZetLand_Valid(string land) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.ZetLand(land);

            Assert.Equal(land.Trim(), a.Land);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]

        public void ZetLand_InValid(string land) {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.Throws<AdresException>(() => a.ZetLand(land));
        }

        [Fact]
        public void IsDezelfde_InValid()
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.False(a.IsDezelfde(new Adres(1, "Kompasplein", "159", "9000", "Gent", "België")));
        }

        [Fact]
        public void IsDezelfde_IsValid()
        {
            Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.True(a.IsDezelfde(new Adres(1, "Kompasplein", "19", "9000", "Gent", "België")));
        }
    }
}