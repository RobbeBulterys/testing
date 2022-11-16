using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Collections.Generic;

namespace UnitTestDomein
{
    public class UnitTestBedrijf
    {
        [Theory]
        [InlineData("Hozee", "ATU01234567", "information@example.com")]
        [InlineData("Jantje    ", "BE0012345678", "   info.Brewery@example.com")]
        [InlineData("jos     ke", "NL012345678B12", "   customerService@example.be   ")]
        public void Constructor_ZonderId_Valid(string naam, string btwNummer, string email)
        {
            Bedrijf b = new Bedrijf(naam, btwNummer, email);
            
            Assert.Equal(naam.Trim(), b.Naam);
            Assert.Equal(btwNummer.Trim(), b.BTWNummer);
            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", " ")]
        [InlineData("\n", "\n", "\n")]
        [InlineData(null, null, null)]
        public void Constructor_ZonderId_InValid(string naam, string btwNummer, string email)
        {
            Assert.ThrowsAny<BedrijfException>(() => new Bedrijf(naam, btwNummer, email));
        }

        [Theory]
        [InlineData(1, "Hozee", "ATU01234567", "information@example.com")]
        [InlineData(96, "Jantje    ", "BE0012345678", "   info.Brewery@example.com")]
        [InlineData(420, "jos     ke", "NL012345678B12", "   customerService@example.be   ")]
        public void Constructor_MetId_Valid(int id, string naam, string btwNummer, string email)
        {
            Bedrijf b = new Bedrijf(id, naam, btwNummer, email);

            Assert.Equal(id, b.Id);
            Assert.Equal(naam.Trim(), b.Naam);
            Assert.Equal(btwNummer.Trim(), b.BTWNummer);
            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData(0, "", "", "")]
        [InlineData(-1, " ", " ", " ")]
        [InlineData(-69, "\n", "\n", "\n")]
        [InlineData(-420, null, null, null)]
        public void Constructor_MetId_InValid(int id, string naam, string btwNummer, string email)
        {
            Assert.ThrowsAny<BedrijfException>(() => new Bedrijf(id, naam, btwNummer, email));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.ZetId(id);

            Assert.Equal(id, b.Id);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetId(id));
            Assert.Equal("Bedrijf - ZetId - Id ongeldig; Kleiner dan 1", ex.Message);
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        [InlineData("jos     ke")]
        public void ZetNaam_Valid(string naam)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.ZetNaam(naam);

            Assert.Equal(naam.Trim(), b.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetNaam_InValid(string naam)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetNaam(naam));
            Assert.Equal("Bedrijf - ZetNaam - Geen naam ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("ATU01234567")]
        [InlineData("BE0012345678")]
        [InlineData("NL012345678B12")]
        public void ZetBtwNummer_Valid(string btwNummer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.ZetBTWNummer(btwNummer);

            Assert.Equal(btwNummer, b.BTWNummer);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetBtwNummer_IsNull(string btwNummer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetBTWNummer(btwNummer));
            Assert.Equal("Bedrijf - ZetBTWNummer - Geen BTWNummer ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void ZetBtwNummer_Ongeldig(string btwNummer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<ControleException>(() => b.ZetBTWNummer(btwNummer));
            Assert.Equal("Controle - IsBestaandBTWnummer - Ongeldig BTW Nummer", ex.Message);
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("   info.Brewery@example.com")]
        [InlineData("   customerService@example.be   ")]
        public void ZetEmail_Valid(string email)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.ZetEmail(email);

            Assert.Equal(email.Trim(), b.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetEmail_IsNull(string email)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetEmail(email));
            Assert.Equal("Bedrijf - ZetEmail - Geen email ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void ZetEmail_Ongeldig(string email)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<ControleException>(() => b.ZetEmail(email));
            Assert.Equal("Controle - IsGoedeEmailSyntax - Ongeldige email", ex.Message);
        }

        [Theory]
        [InlineData("0412345678")]
        [InlineData("+32412345678")]
        [InlineData("0201234567")]
        [InlineData("+31 020 1234567")]
        public void ZetTelefoon_Valid(string telefoon)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            b.ZetTelefoon(telefoon);

            Assert.Equal(telefoon.Trim(), b.Telefoon);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void ZetTelefoon_InValid(string telefoon)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetTelefoon(telefoon));
            Assert.Equal("Bedrijf - ZetTelefoon - Geen telefoon ingevuld", ex.Message);
        }

        [Fact]
        public void ZetAdres_Valid()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Adres a = new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie");

            b.ZetAdres(a);

            Assert.Equal(a, b.Adres);
        }

        [Theory]
        [InlineData(null)]
        public void ZetAdres_InValid(Adres adres)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.ZetAdres(adres));
            Assert.Equal("Bedrijf - ZetAdres - Geen adres ingevuld", ex.Message);
        }

        [Fact]
        public void VeranderAdres_Valid()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Adres a = new Adres(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie");
            b.ZetAdres(new Adres(2, "bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            b.VeranderAdres(a);

            Assert.Equal(a, b.Adres);
        }

        [Theory]
        [InlineData(null)]
        public void VeranderAdres_IsNull(Adres nieuwAdres)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.VeranderAdres(nieuwAdres));
            Assert.Equal("Bedrijf - VeranderAdres - Geen adres ingevuld", ex.Message);
        }

        [Fact]
        public void VeranderAdres_IsDezelfde()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Adres a = new Adres(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie");
            b.ZetAdres(new Adres(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            var ex = Assert.Throws<BedrijfException>(() => b.VeranderAdres(a));
            Assert.Equal("Bedrijf - VeranderAdres - Adres is hetzelfde", ex.Message);
        }

        

        [Fact]
        public void IsDezelfde_Valid()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.True(b.IsDezelfde(new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com")));
        }

        [Fact]
        public void IsDezelfde_BedrijfIsNull()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            var ex = Assert.Throws<BedrijfException>(() => b.IsDezelfde(null));
            Assert.Equal("Bedrijf - IsDezelfde - Geen bedrijf ingevuld", ex.Message);
        }

        [Fact]
        public void IsDezelfde_InValid()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.False(b.IsDezelfde(new Bedrijf(2, "Bosteels brewery", "BE0123123123", "info@example.com")));
        }
    }
}
