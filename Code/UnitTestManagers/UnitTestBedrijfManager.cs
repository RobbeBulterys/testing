using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestBedrijfManager
    {
        [Theory]
        [InlineData("BE0123321123", "Allphi", "info@allphi.com", "+320412345678", "Belgie", "Kompasplein", "19", "9000", "Gent")]
        [InlineData("BE0123451234", "Cobus", "info@cobus.com", "+320412344321", "Belgie", "Quai des Charbonnages", "23", "1030", "Molenbeek")]
        [InlineData("BE0543211234", "Hogent", "info@hogent.com", "+320487654321", "Belgie", "Taborastraat", "13", "8300", "Knokke-Heist")]
        public void VoegBedrijfToe_InValid(string btwnummer, string naam, string email, string? telefoon, string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(-69, -420)]
        public void VerwijderBedrijf_InValid(int bedrijfid, int? adresid)
        {

        }

        [Theory]
        [InlineData(0, "BE0123321123", "Allphi", "info@allphi.com", "+320412345678")]
        [InlineData(-1, "BE0123451234", "Cobus", "info@cobus.com", "+320412344321")]
        [InlineData(-99, "BE0543211234", "Hogent", "info@hogent.com", "+320487654321")]
        public void UpdateBedrijf_InValid(int id, string? btwnummer, string? naam, string? email, string? telefoon)
        {

        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -2)]
        [InlineData(-69, -420)]
        public void UpdateBedrijfAdres_InValid(int id, int adresId)
        {

        }

        [Fact]
        public void ZoekBedrijven_InValid()
        {

        }
    }
}