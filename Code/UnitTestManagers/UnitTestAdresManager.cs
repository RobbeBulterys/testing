using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using System.Collections;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestAdresManager
    {
        [Theory]
        [InlineData("Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData("Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData("Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void VoegAdresToe_AdresZonderId(string straat, string nummer, string postcode, string plaats, string land)
        {
            Adres a = new Adres(straat, nummer, postcode, plaats, land);
            Mock<IAdresRepository> adresRepoMock = new Mock<IAdresRepository>();
            Mock<IBedrijfRepository> bedrijfRepoMock = new Mock<IBedrijfRepository>();
            adresRepoMock.Setup(x => x.BestaatAdresZonderId(a)).Returns(true);
            bedrijfRepoMock.Setup(x => x.BestaatBedrijfZonderId("Allphi", "BE0123123123", "allphi@info.be")).Returns(true);
            AdresManager AM = new AdresManager(adresRepoMock.Object, bedrijfRepoMock.Object);

            Assert.Equal(adresRepoMock.Object.GeefAdresId(a), AM.VoegAdresToe(a));
        }

        [Theory]
        [InlineData(null)]
        public void VoegAdresToe_IsNull(Adres value)
        {
            Adres a = value;
            Mock<IAdresRepository> adresRepoMock = new Mock<IAdresRepository>();
            Mock<IBedrijfRepository> bedrijfRepoMock = new Mock<IBedrijfRepository>();
            adresRepoMock.Setup(x => x.BestaatAdresZonderId(a)).Returns(true);
            bedrijfRepoMock.Setup(x => x.BestaatBedrijfZonderId("Allphi", "BE0123123123", "allphi@info.be")).Returns(true);
            AdresManager AM = new AdresManager(adresRepoMock.Object, bedrijfRepoMock.Object);

            var ex = Assert.Throws<AdresManagerException>(() => AM.VoegAdresToe(a));
            Assert.Equal("AdresManager - VoegAdresToe - Adres is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void VerwijderAdres_BestaatNiet(int id)
        {
            Mock<IAdresRepository> adresRepoMock = new Mock<IAdresRepository>();
            Mock<IBedrijfRepository> bedrijfRepoMock = new Mock<IBedrijfRepository>();
            adresRepoMock.Setup(x => x.BestaatAdresMetId(id)).Returns(false);
            bedrijfRepoMock.Setup(x => x.BedrijvenOpAdresAanwezig(id)).Returns(true);
            AdresManager AM = new AdresManager(adresRepoMock.Object, bedrijfRepoMock.Object);

            var ex = Assert.Throws<AdresManagerException>(() => AM.VerwijderAdres(id));
            Assert.Equal("AdresManager - VerwijderAdres - Onbestaand Adres", ex.InnerException.Message);
        }

        [Fact]
        public void VerwijderAdres_BedrijfAanwezig()
        {
            Mock<IAdresRepository> adresRepoMock = new Mock<IAdresRepository>();
            Mock<IBedrijfRepository> bedrijfRepoMock = new Mock<IBedrijfRepository>();
            adresRepoMock.Setup(x => x.BestaatAdresMetId(1)).Returns(true);
            bedrijfRepoMock.Setup(x => x.BedrijvenOpAdresAanwezig(1)).Returns(false);
            AdresManager AM = new AdresManager(adresRepoMock.Object, bedrijfRepoMock.Object);

            var ex = Assert.Throws<AdresManagerException>(() => AM.VerwijderAdres(1));
            Assert.Equal("AdresManager - VerwijderAdres - Kan geen adres verwijderen waar er nog steeds bedrijven aanwezig zijn", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData(69, "Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData(420, "Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void UpdateAdres_InValid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            Adres a = new Adres(id, straat, nummer, postcode, plaats, land);
            Mock<IAdresRepository> adresRepoMock = new Mock<IAdresRepository>();
            Mock<IBedrijfRepository> bedrijfRepoMock = new Mock<IBedrijfRepository>();
            adresRepoMock.Setup(x => x.BestaatAdresZonderId(a)).Returns(true);
            bedrijfRepoMock.Setup(x => x.BestaatBedrijfZonderId("Allphi", "BE0123123123", "allphi@info.be")).Returns(true);
            AdresManager AM = new AdresManager(adresRepoMock.Object, bedrijfRepoMock.Object);

            var ex = Assert.Throws<AdresManagerException>(() => AM.UpdateAdres(id, straat, nummer, postcode, plaats, land));
            Assert.Equal("AdresManager - UpdateAdres - Adres bestaat niet", ex.InnerException.Message);
        }
    }
}