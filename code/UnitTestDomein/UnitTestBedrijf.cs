using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using System.Collections.Generic;

namespace UnitTestDomein
{
    public class UnitTestBedrijf
    {
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

            Assert.Throws<BedrijfException>(() => b.ZetId(id));
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
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetNaam_InValid(string naam)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.ZetNaam(naam));
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
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetBtwNummer_InValid_BedrijfException(string btwNummer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.ZetBTWNummer(btwNummer));
        }

        [Theory]
        [InlineData("AT01234567")]
        [InlineData("BE012345678")]
        [InlineData("PL50123456789")]
        public void ZetBtwNummer_InValid_ControleException(string btwNummer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<ControleException>(() => b.ZetBTWNummer(btwNummer));
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
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetEmail_InValid_BedrijfException(string email)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.ZetEmail(email));
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void ZetEmail_InValid_ControleException(string email)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<ControleException>(() => b.ZetEmail(email));
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

            Assert.Throws<BedrijfException>(() => b.ZetAdres(adres));
        }

        [Theory]
        [InlineData(null)]
        public void VeranderAdres_InValid_BedrijfIsNull(Adres nieuwAdres)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.VeranderAdres(nieuwAdres));
        }

        [Fact]
        public void VeranderAdres_InValid_ZelfdeAdres()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Adres a = new Adres(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie");
            b.ZetAdres(new Adres(1, "bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.Throws<BedrijfException>(() => b.VeranderAdres(a));
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

        [Fact]
        public void GeefWerknemers_Valid()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Equal(0, b.GeefWerknemers().Count);
        }

        [Theory]
        [InlineData(null)]
        public void VoegWerknemerToe_InValid_WerknemerIsNull(Werknemer werknemer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.VoegWerknemerToe(werknemer));
        }

        [Fact]
        public void VoegWerknemerToe_InValid_WerknemerBestaatAl()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Werknemer w = new Werknemer(1, "Jan", "Baetens", "programmer");

            b.VoegWerknemerToe(w);

            Assert.Throws<BedrijfException>(() => b.VoegWerknemerToe(w));
        }

        [Fact]
        public void VoegWerknemerToe_Valid()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Werknemer w = new Werknemer(1, "Jan", "Baetens", "programmer");

            b.VoegWerknemerToe(w);

            IReadOnlyList<Werknemer> werknemers = b.GeefWerknemers();

            Assert.Equal(1, werknemers[0].PersoonId);
        }

        [Theory]
        [InlineData(0)]
        public void VerwijderWerknemer_InValid_WerknemerIsNull(int werknemer)
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");

            Assert.Throws<BedrijfException>(() => b.VerwijderWerknemer(werknemer));
        }

        [Fact]
        public void VerwijderWerknemer_InValid_WerknemerBestaatNiet()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Werknemer w = new Werknemer(2, "Jan", "Baetens", "programmer");

            Assert.Throws<BedrijfException>(() => b.VerwijderWerknemer(w.PersoonId));
        }

        [Fact]
        public void VerwijderWerknemer_Valid()
        {
            Bedrijf b = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
            Werknemer w = new Werknemer(1, "Jan", "Baetens", "programmer");

            b.VoegWerknemerToe(w);

            Assert.Equal(1, b.GeefWerknemers().Count);

            b.VerwijderWerknemer(w.PersoonId);

            Assert.Equal(0, b.GeefWerknemers().Count);
        }

        [Fact]
        public void IsDezelfde_Valid()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.True(b.IsDezelfde(b2));
        }

        [Fact]
        public void IsDezelfde_InValid_BedrijfNull()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.Throws<BedrijfException>(() => b.IsDezelfde(null));
        }

        [Fact]
        public void IsDezelfde_InValid_DifferentId()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(2, "Bosteels brewery", "BE0123123123", "info@example.com");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.False(b.IsDezelfde(b2));
        }

        [Fact]
        public void IsDezelfde_InValid_DifferentNaam()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(1, "Bosteelse Harbor", "BE0123123123", "info@example.com");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.False(b.IsDezelfde(b2));
        }

        [Fact]
        public void IsDezelfde_InValid_DifferentBtwNummer()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(1, "Bosteels brewery", "BE0123123158", "info@example.com");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.False(b.IsDezelfde(b2));
        }

        [Fact]
        public void IsDezelfde_InValid_DifferentTelefoon()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetTelefoon("0491732014");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b2.ZetTelefoon("0491784014");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.False(b.IsDezelfde(b2));
        }

        [Fact]
        public void IsDezelfde_InValid_DifferentEmail()
        {
            Bedrijf b = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
            Bedrijf b2 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info.Customers@example.com");
            b2.ZetAdres(new Adres("bijvoegstraat", "20", "9530", "Eigem", "Belgie"));

            Assert.False(b.IsDezelfde(b2));
        }
    }
}
