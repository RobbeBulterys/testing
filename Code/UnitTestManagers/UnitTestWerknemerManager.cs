using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestWerknemerManager
    {
        [Theory]
        [InlineData(null)]
        public void VoegWerknemerToe_InValid(Werknemer werknemer)
        {

        }

        [Theory]
        [InlineData(0)]
        public void BestaatWerknemer_Id_InValid(int werknemerId)
        {

        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void BestaatWerknemer_Naam_InValid(string naam, string voornaam)
        {

        }

        [Theory]
        [InlineData(0)]
        public void VerwijderWerknemer_IsNull(int werknemerId)
        {

        }

        [Theory]
        [InlineData(0)]
        public void VerwijderWerknemer_Onbestaand(int werknemerId)
        {

        }

        [Theory]
        [InlineData(1, null, null)]
        [InlineData(99, "", "")]
        [InlineData(420, "      ", "   ")]
        public void UpdateWerknemer_IsNull(int werknemerId, string? naam, string? voornaam)
        {

        }

        [Theory]
        [InlineData(1, null, null)]
        [InlineData(99, "", "")]
        [InlineData(420, "      ", "   ")]
        public void UpdateWerknemer_IsDezelfde(int werknemerId, string? naam, string? voornaam)
        {

        }

        [Fact]
        public void ZoekWerknemers_InValid()
        {

        }

        [Fact]
        public void GeefWerknemer_InValid()
        {

        }  
    }
}