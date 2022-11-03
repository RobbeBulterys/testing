using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{
    public class UnitTestWerknemer
    {
        //private static readonly Bedrijf b_Bosteels = new Bedrijf(10, "Bosteels brewery", "BE0123123123", "info@example.com");
        //private static readonly Werknemer werknemer_Josh = new Werknemer(10, "Baetens", "Josh", b_Bosteels, "programmer");


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id)
        {
            Werknemer werknemer_Josh = new Werknemer(10, "Baetens", "Josh", "programmer");

            werknemer_Josh.ZetId(id);

            Assert.Equal(id, werknemer_Josh.PersoonId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id)
        {
            Werknemer werknemer_Josh = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<PersoonException>(() => werknemer_Josh.ZetId(id));
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        [InlineData("     Zjeff    ")]
        [InlineData("     Zj   eff    ")]
        public void ZetNaam_Valid(string naam)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            w.ZetNaam(naam);

            Assert.Equal(naam.Trim(), w.Naam);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetNaam_InValid(string naam)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<PersoonException>(() => w.ZetNaam(naam));
        }

        [Theory]
        [InlineData("Hozee")]
        [InlineData("Jantje    ")]
        [InlineData("     Zjeff")]
        [InlineData("     Zjeff    ")]
        public void ZetVoorNaam_Valid(string voornaam)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

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
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<PersoonException>(() => w.ZetVoorNaam(voornaam));
        }

        [Theory]
        [InlineData("Jantje.Hozee@gmail.com")]
        [InlineData("Jantje.Hozee@gmail.com    ")]
        [InlineData("    Jantje.Hozee@gmail.com")]
        [InlineData("    JantjeZjeff@hotmail.be    ")]
        public void ZetEmail_Valid(string email)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            w.ZetEmail(email);

            Assert.Equal(email.Trim(), w.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetEmail_InValid(string email)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<PersoonException>(() => w.ZetEmail(email));
        }

        [Theory]
        [InlineData("Jantje.Hozeegmail.com")]
        [InlineData("   JantjeZjeff@hotmailbe    ")]
        public void ZetEmail_InValidSyntax(string email)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<PersoonException>(() => w.ZetEmail(email));
        }

        [Theory]
        [InlineData("Programmer")]
        [InlineData("    Boss")]
        [InlineData("Programmer    ")]
        [InlineData("    Programmer    ")]
        [InlineData("Progr    ammer")]
        public void ZetFunctie_Valid(string functie)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            w.ZetFunctie(functie);

            Assert.Equal(functie.Trim(), w.Functie);
        }

        [Theory]
        [InlineData("")]
        [InlineData("        ")]
        [InlineData(null)]
        public void ZetFunctie_InValid(string functie)
        {
            Werknemer w = new Werknemer(10, "Baetens", "Josh", "programmer");

            Assert.Throws<WerknemerException>(() => w.ZetFunctie(functie));
        }
    }
}
