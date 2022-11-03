using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Xunit;


namespace UnitTestManagers
{
    public class UnitTestBezoekManager
    {
        private IBezoekRepository bezoekRepo;

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void VoegBezoekToe_Valid(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.IsNotType<BezoekManagerException>(() => bezoekManager.VoegBezoekToe(bezoek));
        }
        [Theory]
        [InlineData(null)]
        public void VoegBezoekToe_InValid(Bezoek bezoek)
        {
            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.Throws<BezoekManagerException>(() => bezoekManager.VoegBezoekToe(bezoek));
        }
        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void VerwijderBezoek_Valid(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.IsNotType<BezoekManagerException>(() => bezoekManager.VerwijderBezoek(bezoek));
        }
        [Theory]
        [InlineData(null)]
        public void VerwijderBezoek_InValid(Bezoek bezoek)
        {
            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.Throws<BezoekManagerException>(() => bezoekManager.VerwijderBezoek(bezoek));
        }
        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
         public void UpdateBezoek_Valid(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.IsNotType<BezoekManagerException>(() => bezoekManager.UpdateBezoek(bezoek));
        }
        [Theory]
        [InlineData(null)]
        public void UpdateBezoek_InValid(Bezoek bezoek)
        {
            BezoekManager bezoekManager = new BezoekManager(bezoekRepo);

            Assert.Throws<BezoekManagerException>(() => bezoekManager.UpdateBezoek(bezoek));
        }
    }
}