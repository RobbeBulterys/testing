using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestBedrijfManager
    {
        private IBedrijfRepository bedrijfRepo;
        private AdresManager AM;

        [Theory]
        [InlineData("BE0123321123", "Allphi", "info@allphi.com", "+320412345678", "Belgie", "Kompasplein", "19", "9000", "Gent")]
        [InlineData("BE0123451234", "Cobus", "info@cobus.com", "+320412344321", "Belgie", "Quai des Charbonnages", "23", "1030", "Molenbeek")]
        [InlineData("BE0543211234", "Hogent", "info@hogent.com", "+320487654321", "Belgie", "Taborastraat", "13", "8300", "Knokke-Heist")]
        public void VoegBedrijfToe_Valid(string btwnummer, string naam, string email, string? telefoon, string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.IsNotType<BedrijfManagerException>(() => bedrijfManager.VoegBedrijfToe(btwnummer, naam, email, telefoon, land, straat, nummer, postcode, plaats));
        }
        [Theory]
        [InlineData("BE0123321123", "Allphi", "info@allphi.com", "+320412345678", "Belgie", "Kompasplein", "19", "9000", "Gent")]
        [InlineData("BE0123451234", "Cobus", "info@cobus.com", "+320412344321", "Belgie", "Quai des Charbonnages", "23", "1030", "Molenbeek")]
        [InlineData("BE0543211234", "Hogent", "info@hogent.com", "+320487654321", "Belgie", "Taborastraat", "13", "8300", "Knokke-Heist")]
        public void VoegBedrijfToe_InValid(string btwnummer, string naam, string email, string? telefoon, string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.IsType<BedrijfManager>(bedrijfManager);
        }
        [Theory]
        [InlineData(1, 2)]
        [InlineData(99, 100)]
        [InlineData(69, 420)]
        public void VerwijderBedrijf_Valid(int bedrijfid, int? adresid)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.IsNotType<BedrijfManagerException>(() => bedrijfManager.VerwijderBedrijf(bedrijfid, adresid));
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(-69, -420)]
        public void VerwijderBedrijf_InValid(int bedrijfid, int? adresid)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.Throws<BedrijfManagerException>(() => bedrijfManager.VerwijderBedrijf(bedrijfid, adresid));
        }
        [Theory]
        [InlineData(1, "BE0123321123", "Allphi", "info@allphi.com", "+320412345678")]
        [InlineData(99, "BE0123451234", "Cobus", "info@cobus.com", "+320412344321")]
        [InlineData(420, "BE0543211234", "Hogent", "info@hogent.com", "+320487654321")]
        public void UpdateBedrijf_Valid(int id, string? btwnummer, string? naam, string? email, string? telefoon)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.IsNotType<BedrijfManagerException>(() => bedrijfManager.UpdateBedrijf(id, btwnummer, naam, email, telefoon));
        }
        [Theory]
        [InlineData(0, "BE0123321123", "Allphi", "info@allphi.com", "+320412345678")]
        [InlineData(-1, "BE0123451234", "Cobus", "info@cobus.com", "+320412344321")]
        [InlineData(-99, "BE0543211234", "Hogent", "info@hogent.com", "+320487654321")]
        public void UpdateBedrijf_InValid(int id, string? btwnummer, string? naam, string? email, string? telefoon)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.Throws<BedrijfManagerException>(() => bedrijfManager.UpdateBedrijf(id, btwnummer, naam, email, telefoon));
        }
        [Theory]
        [InlineData(1, 1)]
        [InlineData(99, 100)]
        [InlineData(69, 420)]
        public void UpdateBedrijfAdres_Valid(int id, int adresId)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.IsNotType<BedrijfManagerException>(() => bedrijfManager.UpdateBedrijfAdres(id, adresId));
        }
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(-69, -420)]
        public void UpdateBedrijfAdres_InValid(int id, int adresId)
        {
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepo, AM);

            Assert.Throws<BedrijfManagerException>(() => bedrijfManager.UpdateBedrijfAdres(id, adresId));
        }
    }
}