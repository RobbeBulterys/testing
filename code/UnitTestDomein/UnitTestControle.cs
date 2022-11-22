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
        public void IsValidEmailSyntax_InValid_EmailIsNull(string email)
        {
            var ex = Assert.Throws<VerifyException>(() => Verify.IsValidEmailSyntax(email));
            Assert.Equal("Verify - IsValidEmailSyntax - No email data entry", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void IsValidEmailSyntax_InValid(string email)
        {
            Assert.False(Verify.IsValidEmailSyntax(email));
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("info.Brewery@example.com")]
        [InlineData("customerService@example.be")]
        public void IsValidEmailSyntax_Valid(string email)
        {
            Assert.True(Verify.IsValidEmailSyntax(email));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void IsExistingVATnumber_InValid_vatNumberIsNull(string vatNumber)
        {
            var ex = Assert.Throws<VerifyException>(() => Verify.IsExistingVATnumber(vatNumber));
            Assert.Equal("Controle - IsBestaandBTWnummer - Geen BTWNummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void IsExistingVATnumber_InValid(string vatNumber)
        {
            Assert.False(Verify.IsExistingVATnumber(vatNumber));
        }

        [Theory]
        [InlineData("ATU01234567")]
        [InlineData("BE0012345678")]
        [InlineData("NL012345678B01")]
        public void IsExistingVATnumber_Valid(string vatNumber)
        {
            Assert.True(Verify.IsExistingVATnumber(vatNumber));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void IsValidAdressNumberSyntax_InValid_numberIsNull(string number)
        {
            var ex = Assert.Throws<VerifyException>(() => Verify.IsValidAdressNumberSyntax(number));
            Assert.Equal("Verify - IsValidAdressNumberSyntax -  No number data entry", ex.Message);
        }

        [Theory]
        [InlineData("b")]
        [InlineData("gg")]
        [InlineData("hallo")]
        public void IsValidAdressNumberSyntax_InValid_NoDigit(string number)
        {
            var ex = Assert.Throws<VerifyException>(() => Verify.IsValidAdressNumberSyntax(number));
            Assert.Equal("Verify - IsValidAdressNumberSyntax - Invalid number", ex.Message);
        }

        [Theory]
        [InlineData("1b dier 5g")]
        [InlineData("589g hey 3v")]
        [InlineData("20 bussen gaan rond en rond")]
        [InlineData("1b bus")]
        [InlineData("589g bus")]
        [InlineData("20 bus")]
        public void IsValidAdressNumberSyntax_InValid_NotGoodSyntax_NoBus(string number)
        {
            var ex = Assert.Throws<VerifyException>(() => Verify.IsValidAdressNumberSyntax(number));
            Assert.Equal("Verify - IsValidAdressNumberSyntax - Invalid syntax", ex.Message);
        }

        [Theory]
        [InlineData("1b bus 5g")]
        [InlineData("589g bus 3v")]
        [InlineData("20 bus 8")]
        [InlineData("1b")]
        [InlineData("589")]
        [InlineData("20")]
        public void IsValidAdressNumberSyntax_Valid(string number)
        {
            Assert.True(Verify.IsValidAdressNumberSyntax(number));
        }
    }
}
