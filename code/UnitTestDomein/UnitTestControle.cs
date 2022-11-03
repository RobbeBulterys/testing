using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{
    public class UnitTestControle
    {
        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void IsGoedeEmailSyntax_InValid_EmailIsNull(string email)
        {
            Assert.Throws<ControleException>(() => Controle.IsGoedeEmailSyntax(email));
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void IsGoedeEmailSyntax_InValid(string email)
        {
            Assert.Throws<ControleException>(() => Controle.IsGoedeEmailSyntax(email));
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
        [InlineData("        ")]
        [InlineData(null)]
        public void IsBestaandBTWnummer_InValid_BtwNummerIsNull(string btwNummer)
        {
            Assert.Throws<ControleException>(() => Controle.IsBestaandBTWnummer(btwNummer));
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void IsBestaandBTWnummer_InValid(string btwNummer)
        {
            Assert.Throws<ControleException>(() => Controle.IsBestaandBTWnummer(btwNummer));
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
        [InlineData("        ")]
        [InlineData(null)]
        public void IsGoedeAdresNummerSyntax_InValid_nummerIsNull(string nummer)
        {
            Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
        }

        [Theory]
        [InlineData("b")]
        [InlineData("gg")]
        [InlineData("hallo")]
        public void IsGoedeAdresNummerSyntax_InValid_NoGigit(string nummer)
        {
            Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
        }

        [Theory]
        [InlineData("1b dier 5g")]
        [InlineData("589g hey 3v")]
        [InlineData("20 bussen gaan rond en rond")]
        [InlineData("1b bus")]
        [InlineData("589g bus")]
        [InlineData("20 bus")]
        public void IsGoedeAdresNummerSyntax_InValid_NotGoodSyntax(string nummer)
        {
            Assert.Throws<ControleException>(() => Controle.IsGoedeAdresNummerSyntax(nummer));
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
