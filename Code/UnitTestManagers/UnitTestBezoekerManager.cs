using BL_Projectwerk.Interfaces;
using Xunit;
using System;
using System.Linq;
using System.Collections.Generic;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using BL_Projectwerk.Exceptions;
using DL_Projectwerk;
using System.Data.Common;
using Moq;

namespace UnitTestManagers
{
    public class UnitTestBezoekerManager
    {
        private IBezoekerRepository bezoekerRepo;

        [Theory]
        [InlineData("Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData("Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Cobus")]
        public void VoegBezoekerToe_Valid(string naam, string voornaam, string email, string bedrijf)
        {
            Bezoeker bezoeker = new Bezoeker(naam, voornaam, email, bedrijf);
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.IsNotType<BezoekerManagerException>(() => bezoekerManager.VoegBezoekerToe(bezoeker));
        }
        [Theory]
        [InlineData(null)]
        public void VoegBezoekerToe_InValid(Bezoeker bezoeker)
        {
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.Throws<BezoekerManagerException>(() => bezoekerManager.VoegBezoekerToe(bezoeker));
        }
        [Theory]
        [InlineData("Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData("Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Cobus")]
        public void VerwijderBezoeker_Valid(string naam, string voornaam, string email, string bedrijf)
        {
            Bezoeker bezoeker = new Bezoeker(naam, voornaam, email, bedrijf);
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.IsNotType<BezoekerManagerException>(() => bezoekerManager.VerwijderBezoeker(bezoeker));
        }
        [Theory]
        [InlineData(null)]
        public void VerwijderBezoeker_InValid(Bezoeker bezoeker)
        {
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.Throws<BezoekerManagerException>(() => bezoekerManager.VerwijderBezoeker(bezoeker));
        }
        [Theory]
        [InlineData(1, "Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData(99, "Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData(420, "Doe", "Jake", "Jake@Doe.be", "Cobus")]
        public void UpdateBezoeker_Valid(int id, string? naam, string? voornaam, string? email, string? bedrijf)
        {
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.IsNotType<BezoekerManagerException>(() => bezoekerManager.UpdateBezoeker(id, naam, voornaam, email, bedrijf));
        }
        [Theory]
        [InlineData(0, "Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData(0, "Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData(0, "Doe", "Jake", "Jake@Doe.be", "Cobus")]
        public void UpdateBezoeker_InValid(int id, string? naam, string? voornaam, string? email, string? bedrijf)
        {
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);

            Assert.Throws<BezoekerManagerException>(() => bezoekerManager.UpdateBezoeker(id, naam, voornaam, email, bedrijf));
        }
    }
}