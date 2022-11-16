using BL_Projectwerk.Domein;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using Xunit;

namespace UnitTestDomein
{
    public class UnitTestWerknemercontract
    {
        [Fact]
        public void Constructor_Valid()
        {
            Bedrijf bedrijf = new Bedrijf("Jantje", "BE0012345678", "info.Brewery@example.com");
            Werknemer werknemer = new Werknemer("Doe", "John");
            string functie = "programeur";
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            Assert.Equal(bedrijf, wc.Bedrijf);
            Assert.Equal(werknemer, wc.Werknemer);
            Assert.Equal(functie, wc.Functie);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\n", "\n")]
        [InlineData(null, null)]
        public void Constructor_InValid(string naam, string voornaam)
        {
            Assert.ThrowsAny<PersoonException>(() => new Werknemer(naam, voornaam));
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("   info.Brewery@example.com")]
        [InlineData("   customerService@example.be   ")]
        public void ZetEmail_Valid(string email)
        {
            Bedrijf bedrijf = new Bedrijf("Jantje", "BE0012345678", "info.Brewery@example.com");
            Werknemer werknemer = new Werknemer("Doe", "John");
            string functie = "programeur";
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            wc.ZetEmail(email);

            Assert.Equal(email.Trim(), wc.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetEmail_IsNull(string email)
        {
            Bedrijf bedrijf = new Bedrijf("Jantje", "BE0012345678", "info.Brewery@example.com");
            Werknemer werknemer = new Werknemer("Doe", "John");
            string functie = "programeur";
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            var ex = Assert.Throws<WerknemercontractException>(() => wc.ZetEmail(email));
            Assert.Equal("Werknemercontract - ZetEmail - Geen email ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void ZetEmail_Ongeldig(string email)
        {
            Bedrijf bedrijf = new Bedrijf("Jantje", "BE0012345678", "info.Brewery@example.com");
            Werknemer werknemer = new Werknemer("Doe", "John");
            string functie = "programeur";
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            var ex = Assert.Throws<ControleException>(() => wc.ZetEmail(email));
            Assert.Equal("Controle - IsGoedeEmailSyntax - Ongeldige email", ex.Message);
        }
        [Theory]
        [InlineData("Programeur")]
        [InlineData("Boek   houder")]
        [InlineData("     Receptioniste")]
        public void ZetFunctie_Valid(string functie)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "werkloos");

            wc.ZetFunctie(functie);

            Assert.Equal(functie.Trim(), wc.Functie);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetFunctie_InValid(string functie)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "werkloos");

            var ex = Assert.Throws<WerknemercontractException>(() => wc.ZetFunctie(functie));
            Assert.Equal("Werknemercontract - ZetFunctie - Geen functie ingevuld", ex.Message);
        }

        [Fact]
        public void IsDezelfde_Valid_()
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "werkloos");

            Assert.True(wc.IsDezelfde(new Werknemercontract(new Bedrijf("Allphi", "BE0123123123", "allphi@info.be"), new Werknemer("VanDeWiele", "Tom"), "werkloos")));
        }

        [Fact]
        public void IsDezelfde_Invalid()
        {
            Bedrijf bedrijf1 = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer1 = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc1 = new Werknemercontract(bedrijf1, werknemer1, "werkloos");
            Bedrijf bedrijf2 = new Bedrijf("Hogent", "BE0321321321", "hogent@info.be");
            Werknemer werknemer2 = new Werknemer("doe", "john");
            Werknemercontract wc2 = new Werknemercontract(bedrijf2, werknemer2, "programeur");

            Assert.False(wc1.IsDezelfde(wc2));
        }
    }
}
