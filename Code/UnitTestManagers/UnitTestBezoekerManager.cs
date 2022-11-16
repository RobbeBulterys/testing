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
        [Theory]
        [InlineData(null)]
        public void VoegBezoekerToe_IsNull(Bezoeker bezoeker)
        {

        }

        [Fact]
        public void VoegBezoekerToe_IsDezelfde()
        {

        }

        [Theory]
        [InlineData(null)]
        public void VerwijderBezoeker_IsNull(Bezoeker bezoeker)
        {

        }

        [Fact]
        public void VerwijderBezoeker_Onbestaand()
        {

        }

        [Theory]
        [InlineData(0, "Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData(0, "Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData(0, "Doe", "Jake", "Jake@Doe.be", "Cobus")]
        public void UpdateBezoeker_InValid(int id, string? naam, string? voornaam, string? email, string? bedrijf)
        {

        }

        [Fact]
        public void ZoekBezoekers_InValid()
        {

        }

        [Fact]
        public void BestaatBezoeker_InValid()
        {

        }
    }
}