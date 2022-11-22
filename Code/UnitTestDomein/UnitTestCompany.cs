using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Collections.Generic;

namespace UnitTestDomein
{
    public class UnitTestCompany
    {
        [Theory]
        [InlineData("Hozee", "ATU01234567", "information@example.com")]
        [InlineData("Jantje    ", "BE0012345678", "   info.Brewery@example.com")]
        [InlineData("jos     ke", "NL012345678B12", "   customerService@example.be   ")]
        public void Constructor_WithoutId_Valid(string name, string vatNumber, string email)
        {
            Company b = new Company(name, vatNumber, email);
            
            Assert.Equal(name.Trim(), b.Name);
            Assert.Equal(vatNumber.Trim(), b.VATNumber);
            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        [InlineData("\n", "\n", "\n")]
        [InlineData(null, null, null)]
        public void Constructor_WithoutId_InValid(string name, string vatNumber, string email)
        {
            Assert.ThrowsAny<CompanyException>(() => new Company(name, vatNumber, email));
        }

        [Theory]
        [InlineData(1, "Hozee", "ATU01234567", "information@example.com")]
        [InlineData(96, "Jantje    ", "BE0012345678", "   info.Brewery@example.com")]
        [InlineData(420, "jos     ke", "NL012345678B12", "   customerService@example.be   ")]
        public void Constructor_WithId_Valid(int id, string name, string vatNumber, string email)
        {
            Company b = new Company(id, name, vatNumber, email);

            Assert.Equal(id, b.Id);
            Assert.Equal(name.Trim(), b.Name);
            Assert.Equal(vatNumber.Trim(), b.VATNumber);
            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData(0, "", "", "")]
        [InlineData(-1, " ", " ", " ")]
        [InlineData(-69, "\n", "\n", "\n")]
        [InlineData(-420, null, null, null)]
        public void Constructor_WithId_InValid(int id, string name, string vatNumber, string email)
        {
            Assert.ThrowsAny<CompanyException>(() => new Company(id, name, vatNumber, email));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void SetId_Valid(int id)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.SetId(id);

            Assert.Equal(id, b.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void SetId_InValid(int id)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetId(id));
            Assert.Equal("Company - SetId - Id invalid; Less than 1", ex.Message);
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        [InlineData("jos     ke")]
        public void SetName_Valid(string name)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.SetName(name);

            Assert.Equal(name.Trim(), b.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetName_InValid(string name)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetName(name));
            Assert.Equal("Company - SetName - No name data entry", ex.Message);
        }

        [Theory]
        [InlineData("ATU01234567")]
        [InlineData("BE0012345678")]
        [InlineData("NL012345678B12")]
        public void SetVATNumber_Valid(string vatNumber)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.SetVATNumber(vatNumber);

            Assert.Equal(vatNumber, b.VATNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetVATNumber_IsNull(string vatNumber)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetVATNumber(vatNumber));
            Assert.Equal("Company - SetVATNumber - No VATnumber data entry", ex.Message);
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void SetVATNumber_InValidSyntax(string vatNumber)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<VerifyException>(() => b.SetVATNumber(vatNumber));
            Assert.Equal("Company - SetVATNumber - VATnumber does not exist", ex.Message);
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("   info.Brewery@example.com")]
        [InlineData("   customerService@example.be   ")]
        public void SetEmail_Valid(string email)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.SetEmail(email);

            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetEmail_IsNull(string email)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetEmail(email));
            Assert.Equal("Company - SetEmail - No email data entry", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void SetEmail_InValidSyntax(string email)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<VerifyException>(() => b.SetEmail(email));
            Assert.Equal("Company - SetEmail - email does not exist", ex.Message);
        }

        [Theory]
        [InlineData("0412345678")]
        [InlineData("+32412345678")]
        [InlineData("0201234567")]
        [InlineData("+31 020 1234567")]
        public void SetPhoneNumber_Valid(string phoneNumber)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.SetPhoneNumber(phoneNumber);

            Assert.Equal(phoneNumber.Trim(), b.PhoneNumber);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetPhoneNumber_InValid(string phoneNumber)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetPhoneNumber(phoneNumber));
            Assert.Equal("Company - SetPhoneNumber - No phonenumber data entry", ex.Message);
        }

        [Fact]
        public void SetAddress_Valid()
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Address a = new Address("bijvoegstraat", "20", "9530", "Eigem", "Belgie");

            b.SetAddress(a);

            Assert.Equal(a, b.Address);
        }

        [Theory]
        [InlineData(null)]
        public void SetAddress_InValid(Address address)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.SetAddress(address));
            Assert.Equal("Company - SetAddress - No address data entry", ex.Message);
        }

        [Fact]
        public void ChangeAddress_Valid()
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Address a = new Address(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie");
            b.SetAddress(new Address(2, "bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            b.ChangeAddress(a);

            Assert.Equal(a, b.Address);
        }

        [Theory]
        [InlineData(null)]
        public void ChangeAddress_IsNull(Address newAddress)
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.ChangeAddress(newAddress));
            Assert.Equal("Company - ChangeAddress - No address data entry", ex.Message);
        }

        [Fact]
        public void ChangeAddress_IsTheSame()
        {
            Company b = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Address a = new Address(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie");
            b.SetAddress(new Address(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            var ex = Assert.Throws<CompanyException>(() => b.ChangeAddress(a));
            Assert.Equal("Company - ChangeAddress - Address is the same", ex.Message);
        }

        

        [Fact]
        public void HasSameProperties_Valid()
        {
            Company b = new Company(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.True(b.HasSameProperties(new Company(1, "Bosteels brewery", "BE0123123123", "info@example.com")));
        }

        [Fact]
        public void HasSameProperties_CompanyIsNull()
        {
            Company b = new Company(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<CompanyException>(() => b.HasSameProperties(null));
            Assert.Equal("Company - HasSameProperties - No company data entry", ex.Message);
        }

        [Fact]
        public void HasSameProperties_InValid()
        {
            Company b = new Company(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.False(b.HasSameProperties(new Company(2, "Bosteels brewery", "BE0123123123", "info@example.com")));
        }
    }
}
