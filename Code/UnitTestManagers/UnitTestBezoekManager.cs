using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using Xunit;


namespace UnitTestManagers
{
    public class UnitTestBezoekManager
    {
        [Theory]
        [InlineData(null)]
        public void VoegBezoekToe_IsNull(Bezoek bezoek)
        {
            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(true);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);

            var ex = Assert.Throws<BezoekManagerException>(() => BM.VoegBezoekToe(bezoek));
            Assert.Equal("BezoekManager - VoegBezoek - Bezoek is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void VoegBezoekToe_BezoekBestaat(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(true);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);

            var ex = Assert.Throws<BezoekManagerException>(() => BM.VoegBezoekToe(bezoek));
            Assert.Equal("BezoekManager - VoegBezoekToe - Bezoek bestaat al", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void VerwijderBezoek_IsNull(Bezoek bezoek)
        {
            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(true);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);

            var ex = Assert.Throws<BezoekManagerException>(() => BM.VerwijderBezoek(bezoek));
            Assert.Equal("BezoekManager - VerwijderBezoek - Bezoek is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void VerwijderBezoek_BezoekBestaatNiet(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(false);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);
            
            var ex = Assert.Throws<BezoekManagerException>(() => BM.VerwijderBezoek(bezoek));
            Assert.Equal("BezoekManager - VerwijderBezoek - Onbestaand bezoek", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void UpdateBezoek_IsNull(Bezoek bezoek)
        {
            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(true);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);

            var ex = Assert.Throws<BezoekManagerException>(() => BM.UpdateBezoek(bezoek));
            Assert.Equal("BezoekManager - UpdateBezoek - Bezoek is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent", "Allphi", "BE0123321123", "info@Allphi.com", 420, "Doe", "Jake", "4/5/2022")]
        public void UpdateBezoek_BezoekBestaatNiet(string bezoekerNaam, string bezoekerVoornaam, string bezoekerEmail, string bezoekerBedrijf, string bedrijfNaam, string bedrijfBtwNummer, string bedrijfEmail, int persoonId, string werknemernaam, string werknemerVoornaam, DateTime startTijd)
        {
            Bezoeker bezoeker = new Bezoeker(bezoekerNaam, bezoekerVoornaam, bezoekerEmail, bezoekerBedrijf);
            Bedrijf bedrijf = new Bedrijf(bedrijfNaam, bedrijfBtwNummer, bedrijfEmail);
            Werknemer werknemer = new Werknemer(persoonId, werknemernaam, werknemerVoornaam);
            Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, startTijd);

            Mock<IBezoekRepository> bezoekRepoMock = new Mock<IBezoekRepository>();
            bezoekRepoMock.Setup(x => x.BestaatBezoek(bezoek)).Returns(true);
            BezoekManager BM = new BezoekManager(bezoekRepoMock.Object);

            var ex = Assert.Throws<BezoekManagerException>(() => BM.UpdateBezoek(bezoek));
            Assert.NotEqual("BezoekManager - UpdateBezoek - Bezoek is dezelfde", ex.InnerException.Message);
        }

        [Fact]
        public void ZoekBezoeken_InValid()
        {

        }

        [Fact]
        public void LogOutBezoeken_IsNull()
        {

        }
        [Fact]
        public void LogOutBezoeken_Ongeldig()
        {

        }
    }
}