using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein {
    public class UnitTestAddress {

        [Theory]
        [InlineData("Molenstraat", "1", "9000", "Gent", "België")]
        [InlineData("Kerkstraat            ", "999", "10  00", "Antwerpen            ", "Nederland            ")]
        [InlineData("Nieuw      street", "9", "    8300", "Bru     gge", "Fra    nkrijk")]
        [InlineData("     Stationstraat    ", "9 bus 3", "9030", "       Brussel       ", "      Duitsland      ")]
        public void Constructor_WithoutId_Valid(string street, string number, string postalcode, string place, string country)
        {
            Address a = new Address(street, number, postalcode, place, country);

            Assert.Equal(street.Trim(), a.Street);
            Assert.Equal(number.Trim(), a.Number);
            Assert.Equal(postalcode.Trim(), a.Postalcode);
            Assert.Equal(place.Trim(), a.Place);
            Assert.Equal(country.Trim(), a.Country);
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
        public void Constructor_WithoutId_InValid(string street, string number, string postalcode, string place, string country)
        {
            Assert.ThrowsAny<AddressException>(() => new Address(street, number, postalcode, place, country));
        }

        [Theory]
        [InlineData(1, "Molenstraat", "1", "9000", "Gent", "België")]
        [InlineData(2, "Kerkstraat            ", "999", "10  00", "Antwerpen            ", "Nederland            ")]
        [InlineData(69, "Nieuw      street", "9B", "    8300", "Bru     gge", "Fra    nkrijk")]
        [InlineData(420, "     Stationstraat    ", "9B bus 3", "9030     ", "       Brussel       ", "      Duitsland      ")]
        public void Constructor_WithId_Valid(int id, string street, string number, string postalcode, string place, string country)
        {
            Address a = new Address(id, street, number, postalcode, place, country);

            Assert.Equal(id, a.Id);
            Assert.Equal(street.Trim(), a.Street);
            Assert.Equal(number.Trim(), a.Number);
            Assert.Equal(postalcode.Trim(), a.Postalcode);
            Assert.Equal(place.Trim(), a.Place);
            Assert.Equal(country.Trim(), a.Country);
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
        public void Constructor_WithId_InValid(int id, string street, string number, string postalcode, string place, string country)
        {
            Assert.ThrowsAny<AddressException>(() => new Address(id, street, number, postalcode, place, country));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void SetId_Valid(int id) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetId(id);

            Assert.Equal(id, a.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void SetId_InValid(int id) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetId(id));
            Assert.Equal("Adress - SetId - Id invalid; Less than 1", ex.Message);
        }

        [Theory]
        [InlineData("Molenstraat")]
        [InlineData("Kerkstraat            ")]
        [InlineData("Nieuw      street")]
        [InlineData("     Stationstraat    ")]
        public void SetStreet_Valid(string streetname) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetStreet(streetname);

            Assert.Equal(streetname.Trim(), a.Street);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetStreet_InValid(string streetname) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetStreet(streetname));
            Assert.Equal("Adress - SetStreet - No street data entry", ex.Message);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("999")]
        [InlineData("9B")]
        [InlineData("9B bus 3")]
        public void SetNumber_Valid(string nr) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetNumber(nr);

            Assert.Equal(nr, a.Number);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetNumber_IsNull(string nr)
        {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetNumber(nr));
            Assert.Equal("Adress - SetNumber - No number data entry", ex.Message);

        }

        [Theory]
        [InlineData("B")]
        [InlineData("hallo")]
        [InlineData("CCC")]
        public void SetNumber_InValidSyntax(string nr) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<VerifyException>(() => a.SetNumber(nr));
            Assert.Equal("Verify - IsValidAdressNumberSyntax - Invalid number", ex.Message);
        }

        [Theory]
        [InlineData("9000")]
        [InlineData("10  00")]
        [InlineData("    8300")]
        public void SetPostalcode_Valid(string postalcode)
        {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetPostalcode(postalcode);

            Assert.Equal(postalcode.Trim(), a.Postalcode);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetPostalcode_InValid(string postalcode)
        {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetPostalcode(postalcode));
            Assert.Equal("Adress - SetPostalcode - No postalcode data entry", ex.Message);
        }
        [Theory]
        [InlineData("Gent")]
        [InlineData("Antwerpen            ")]
        [InlineData("Bru     gge")]
        [InlineData("       Brussel       ")]
        public void SetPlace_Valid(string place) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetPlace(place);

            Assert.Equal(place.Trim(), a.Place);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetPlace_InValid(string place) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetPlace(place));
            Assert.Equal("Adress - SetPlace - No place data entry", ex.Message);
        }

        [Theory]
        [InlineData("België")]
        [InlineData("Nederland            ")]
        [InlineData("Fra    nkrijk")]
        [InlineData("      Duitsland      ")]
        public void SetCountry_Valid(string country) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            a.SetCountry(country);

            Assert.Equal(country.Trim(), a.Country);
            
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetCountry_InValid(string country) {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            var ex = Assert.Throws<AddressException>(() => a.SetCountry(country));
            Assert.Equal("Adress - SetCountry - No country data entry", ex.Message);
        }

        [Fact]
        public void HasSameProperties_Valid()
        {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.True(a.HasSameProperties(new Address(1, "Kompasplein", "19", "9000", "Gent", "België")));
        }

        [Fact]
        public void HasSameProperties_InValid()
        {
            Address a = new Address(1, "Kompasplein", "19", "9000", "Gent", "België");

            Assert.False(a.HasSameProperties(new Address(1, "Kerkstraat", "15", "1000", "Brussel", "België")));
        }

    }
}