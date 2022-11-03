using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using Xunit;

namespace UnitTestDomein
{
    public class UnitTestWerknemercontract
    {
        [Theory]
        [InlineData("Allphi", "BE0123123123", "allphi@info.be", "VanDeWiele", "Tom")]
        public void ZetBedrijfEnWerknemer_Valid(string bedrijfNaam, string bedrijfBTWnummer, string bedrijfEmail, string WerknemerNaam, string WerknemerVoornaam)
        {
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBTWnummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(WerknemerNaam, WerknemerVoornaam);
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "");

            Assert.Equal(werknemer, wc.Werknemer);
            Assert.Equal(bedrijf, wc.Bedrijf);
        }
        //[Theory]
        //[InlineData(null, null)]
        //public void ZetBedrijfEnWerknemer_InValid(Bedrijf bedrijf, Werknemer werknemer)
        //{
        //    Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "");

        //    Assert.Throws<WerknemercontractException>(() => wc.ZetBedrijfEnWerknemer(bedrijf, werknemer));
        //}
        [Theory]
        [InlineData("allphi@info.be")]
        [InlineData("hogent   @info.be")]
        [InlineData("     cobus@info.be")]
        public void ZetEmail_Valid(string email)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "");
            wc.ZetEmail(email);

            Assert.Equal(email, wc.Email);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("allphi.be")]
        public void ZetEmail_InValid(string email)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, "");
            wc.ZetEmail(email);

            Assert.Throws<PersoonException>(() => wc.ZetEmail(email));
        }
        [Theory]
        [InlineData("Programeur")]
        [InlineData("Boek   houder")]
        [InlineData("     Receptioniste")]
        public void ZetFunctie_Valid(string functie)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            Assert.Equal(functie, wc.Functie);
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void ZetFunctie_InValid(string functie)
        {
            Bedrijf bedrijf = new Bedrijf("Allphi", "BE0123123123", "allphi@info.be");
            Werknemer werknemer = new Werknemer("VanDeWiele", "Tom");
            Werknemercontract wc = new Werknemercontract(bedrijf, werknemer, functie);

            Assert.Throws<WerknemercontractException>(() => wc.ZetFunctie(functie));
        }
    }
}
