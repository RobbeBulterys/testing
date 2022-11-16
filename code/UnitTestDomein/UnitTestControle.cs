using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{
    public class UnitTestControle
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void IsGoedeEmailSyntax_InValid_EmailIsNull(string email)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsGoedeEmailSyntax(email));
            Assert.Equal("Controle - IsGoedeEmailSyntax - Geen email ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void IsGoedeEmailSyntax_InValid(string email)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsGoedeEmailSyntax(email));
            Assert.Equal("Controle - IsGoedeEmailSyntax - Ongeldige email", ex.Message);
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("info.Brewery@example.com")]
        [InlineData("customerService@example.be")]
        public void IsGoedeEmailSyntax_Valid(string email)
        {
            Assert.True(Controle.IsGoedeEmailSyntax(email));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void IsBestaandBTWnummer_InValid_BtwNummerIsNull(string btwNummer)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsBestaandBTWnummer(btwNummer));
            Assert.Equal("Controle - IsBestaandBTWnummer - Geen BTWNummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void IsBestaandBTWnummer_InValid(string btwNummer)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsBestaandBTWnummer(btwNummer));
            Assert.Equal("Controle - IsBestaandBTWnummer - Ongeldig BTW Nummer", ex.Message);
        }

        [Theory]
        [InlineData("ATU01234567")]
        [InlineData("BE0012345678")]
        [InlineData("NL012345678B01")]
        public void IsBestaandBTWnummer_Valid(string btwNummer)
        {
            Assert.True(Controle.IsBestaandBTWnummer(btwNummer));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void IsGoedeAdresNummerSyntax_InValid_nummerIsNull(string nummer)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
            Assert.Equal("Controle - IsGoedeAdresNummerSyntax - Geen nummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("b")]
        [InlineData("gg")]
        [InlineData("hallo")]
        public void IsGoedeAdresNummerSyntax_InValid_NoGigit(string nummer)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
            Assert.Equal("Controle - IsGoedeAdresNummerSyntax - Geen geldig nummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("1b dier 5g")]
        [InlineData("589g hey 3v")]
        [InlineData("20 bussen gaan rond en rond")]
        [InlineData("1b bus")]
        [InlineData("589g bus")]
        [InlineData("20 bus")]
        public void IsGoedeAdresNummerSyntax_InValid_NotGoodSyntax_GeenBus(string nummer)
        {
            var ex = Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
            Assert.Equal("Controle - IsGoedeAdresNummerSyntax - Geen geldige syntax ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("1b bus 5g")]
        [InlineData("589g bus 3v")]
        [InlineData("20 bus 8")]
        [InlineData("1b")]
        [InlineData("589")]
        [InlineData("20")]
        public void IsGoedeAdresNummerSyntax_Valid(string nummer)
        {
            Assert.True(Controle.IsGoedeAdresNummerSyntax(nummer));
        }
    }
}
