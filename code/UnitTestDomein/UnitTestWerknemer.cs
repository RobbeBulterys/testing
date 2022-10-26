using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein {
    public class UnitTestWerknemer {
        private static readonly Bedrijf b_Bosteels = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
        private static readonly Werknemer werknemer_Josh = new Werknemer(10, "Baetens", "Josh", b_Bosteels, "programmer");
        

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void ZetId_Valid(int id)
        {
            Werknemer w = werknemer_Josh;

            w.ZetId(id);

            Assert.Equal(id, w.PersoonId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ZetId_InValid(int id)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<PersoonException>(() => w.ZetId(id));
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        public void ZetNaam_Valid(string naam)
        {
            Werknemer w = werknemer_Josh;

            w.ZetNaam(naam);

            Assert.Equal(naam.Trim(), w.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        [InlineData("jos     ke")]
        public void ZetNaam_InValid(string naam)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<PersoonException>(() => w.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        public void ZetVoorNaam_Valid(string voornaam)
        {
            Werknemer w = werknemer_Josh;

            w.ZetVoorNaam(voornaam);

            Assert.Equal(voornaam.Trim(), w.Voornaam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData("     \n   ")]
        [InlineData("     \r   ")]
        [InlineData("jos     ke")]
        [InlineData(null)]
        public void ZetVoorNaam_InValid(string voornaam)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<PersoonException>(() => w.ZetVoorNaam(voornaam));
        }

        [Theory]
        [InlineData("Jantje.Hozee@gmail.com")]
        [InlineData("   JantjeZjeff@hotmail.be    ")]
        public void ZetEmail_Valid(string email)
        {
            Werknemer w = werknemer_Josh;

            w.ZetEmail(email);

            Assert.Equal(email.Trim(), w.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetEmail_InValid(string email)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<PersoonException>(() => w.ZetEmail(email));
        }

        [Theory]
        [InlineData("Jantje.Hozeegmail.com")]
        [InlineData("   JantjeZjeff@hotmailbe    ")]
        public void ZetEmail_InValidSyntax(string email)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<PersoonException>(() => w.ZetEmail(email));
        }

        [Theory]
        [InlineData("Programmer")]
        [InlineData("Boss")]
        public void ZetFunctie_Valid(string functie)
        {
            Werknemer w = werknemer_Josh;

            w.ZetFunctie(functie);

            Assert.Equal(functie.Trim(), w.Functie);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetFunctie_InValid(string functie)
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<WerknemerException>(() => w.ZetFunctie(functie));
        }

        [Fact]
        public void ZetBedrijf_Valid()
        {
            Werknemer w = werknemer_Josh;
            Bedrijf bedrijf = new Bedrijf(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            w.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf, w.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid()
        {
            Werknemer w = werknemer_Josh;

            Assert.Throws<WerknemerException>(() => w.ZetBedrijf(null));
        }

        [Fact]
        public void VeranderBedrijf_Valid()
        {
            Werknemer w = werknemer_Josh;
            Bedrijf bedrijf = new Bedrijf(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            Bedrijf bedrijf1 = new Bedrijf(4, "brewery", "BE0123123189", "info.brewery@hotmail.com");
            w.ZetBedrijf(bedrijf);
            w.VeranderBedrijf(bedrijf1);

            Assert.True(w.Bedrijf.IsDezelfde(bedrijf1));
        }
    }
}
