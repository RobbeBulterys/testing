using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using System.Collections;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestAdresManager
    {
        private IAdresRepository adresRepo;
        private IBedrijfRepository bedrijfRepo;

        [Theory]
        [InlineData(1, "Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData(99, "Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData(420, "Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void VoegAdresToe_Valid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            Adres a = new Adres(id, straat, nummer, postcode, plaats, land);
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.IsNotType<AdresManagerException>(() => adresManager.VoegAdresToe(a));
        }
        [Theory]
        [InlineData(null)]
        public void VoegAdresToe_InValid(Adres value)
        {
            Adres a = value;
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.Throws<AdresManagerException>(() => adresManager.VoegAdresToe(a));
        }
        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(420)]
        public void VerwijderAdres_Valid(int id)
        {
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.IsNotType<AdresManagerException>(() => adresManager.VerwijderAdres(id));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void VerwijderAdres_InValid(int id)
        {
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.Throws<AdresManagerException>(() => adresManager.VerwijderAdres(id));
        }
        [Theory]
        [InlineData(1, "Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData(99, "Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData(420, "Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void UpdateAdres_Valid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.IsNotType<AdresManagerException>(() => adresManager.UpdateAdres(id, straat, nummer, postcode, plaats, land));
        }
        [Theory]
        [InlineData(0, "Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData(-1, "Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData(420, "Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void UpdateAdres_InValid(int id, string straat, string nummer, string postcode, string plaats, string land)
        {
            AdresManager adresManager = new AdresManager(adresRepo, bedrijfRepo);

            Assert.Throws<AdresManagerException>(() => adresManager.UpdateAdres(id, straat, nummer, postcode, plaats, land));
        }
    }
}