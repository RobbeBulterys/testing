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
            Adres a = new Adres();

            a.ZetId(id);

            Assert.Equal(id, a.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id) {
            Adres a = new Adres();

            Assert.Throws<AdresException>(() => a.ZetId(id));
        }

        [Theory]
        [InlineData("Molenstraat")]
        [InlineData("Kerkstraat            ")]
        [InlineData("           Nieuwstraat")]
        [InlineData("     Statio   nstraat    ")]
        public void ZetStraat_Valid(string straatnaam) {
            Adres a = new Adres();

            a.ZetStraat(straatnaam);

            Assert.Equal(straatnaam.Trim(), a.Straat);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetStraat_InValid(string straatnaam) {
            Adres a = new Adres();

            Assert.Throws<AdresException>(() => a.ZetStraat(straatnaam));
        }

        [Theory]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("999")]
        [InlineData("9B")]
        [InlineData("9B bus 3")]
        public void ZetNummer_Valid(string nr) {
            Adres a = new Adres();

            a.ZetNummer(nr);

            Assert.Equal(nr, a.Nummer);
        }

        [Theory]
        [InlineData("B")]
        [InlineData("hallo")]
        [InlineData("CCC")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        [InlineData("9B Wereldbus")]
        public void ZetNummer_InValid(string nr) {
            Adres a = new Adres();

            Assert.Throws<ControleException>(() => a.ZetNummer(nr));
        }

        [Theory]
        [InlineData("Gent")]
        [InlineData("Antwerpen            ")]
        [InlineData("               Brugge")]
        [InlineData("       Brussel       ")]
        public void ZetPlaats_Valid(string plaats) {
            Adres a = new Adres();

            a.ZetPlaats(plaats);

            Assert.Equal(plaats.Trim(), a.Plaats);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetPlaats_InValid(string plaats) {
            Adres a = new Adres();

            Assert.Throws<AdresException>(() => a.ZetPlaats(plaats));
        }

        [Theory]
        [InlineData("Belgi�")]
        [InlineData("Nederland            ")]
        [InlineData("            Frankrijk")]
        [InlineData("      Duitsland      ")]
        public void ZetLand_Valid(string land) {
            Adres a = new Adres();

            a.ZetLand(land);

            Assert.Equal(land.Trim(), a.Land);

        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]

        public void ZetLand_InValid(string land) {
            Adres a = new Adres();

            Assert.Throws<AdresException>(() => a.ZetLand(land));
        }

    }
}