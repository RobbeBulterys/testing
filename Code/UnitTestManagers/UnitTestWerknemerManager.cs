using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestWerknemerManager
    {
        private IWerknemerRepository werknemerRepo;

        [Theory]
        [InlineData(1, "Doe", "John")]
        [InlineData(99, "Doe", "Jane")]
        [InlineData(420, "Doe", "Jake")]
        public void VoegWerknemerToe_Valid(int persoonId, string naam, string voornaam)
        {
            Werknemer werknemer = new Werknemer(persoonId, naam, voornaam);
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.IsNotType<WerknemerManagerException>(() => werknemerManager.VoegWerknemerToe(werknemer));
        }
        [Theory]
        [InlineData(null)]
        public void VoegWerknemerToe_InValid(Werknemer werknemer)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.Throws<WerknemerManagerException>(() => werknemerManager.VoegWerknemerToe(werknemer));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(420)]
        public void BestaatWerknemer_IdValid(int werknemerId)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.IsNotType<WerknemerManagerException>(() => werknemerManager.BestaatWerknemer(werknemerId));
        }
        [Theory]
        [InlineData(0)]
        public void BestaatWerknemer_IdInValid(int werknemerId)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.Throws<WerknemerManagerException>(() => werknemerManager.BestaatWerknemer(werknemerId));
        }
        [Theory]
        [InlineData("Doe", "John")]
        [InlineData("Doe", "Jane")]
        [InlineData("Doe", "Jake")]
        public void BestaatWerknemer_NaamValid(string naam, string voornaam)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.IsNotType<WerknemerManagerException>(() => werknemerManager.BestaatWerknemer(naam, voornaam));
        }
        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void BestaatWerknemer_NaamInValid(string naam, string voornaam)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.Throws<WerknemerManagerException>(() => werknemerManager.BestaatWerknemer(naam, voornaam));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(420)]
        public void VerwijderWerknemer_Valid(int werknemerId)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.IsNotType<WerknemerManagerException>(() => werknemerManager.VerwijderWerknemer(werknemerId));
        }
        [Theory]
        [InlineData(0)]
        public void VerwijderWerknemer_InValid(int werknemerId)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.Throws<WerknemerManagerException>(() => werknemerManager.VerwijderWerknemer(werknemerId));
        }
        [Theory]
        [InlineData(1, "Doe", "John")]
        [InlineData(99, "Doe", "Jane")]
        [InlineData(420, "Doe", "Jake")]
        public void UpdateWerknemer_Valid(int werknemerId, string? naam, string? voornaam)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.IsNotType<WerknemerManagerException>(() => werknemerManager.UpdateWerknemer(werknemerId, naam, voornaam));
        }
        [Theory]
        [InlineData(1, null, null)]
        [InlineData(99, "", "")]
        [InlineData(420, "      ", "   ")]
        public void UpdateWerknemer_InValid(int werknemerId, string? naam, string? voornaam)
        {
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepo);

            Assert.Throws<WerknemerManagerException>(() => werknemerManager.UpdateWerknemer(werknemerId, naam, voornaam));
        }
    }
}