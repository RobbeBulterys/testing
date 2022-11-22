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
using System.Reflection;

namespace UnitTestManagers
{
    public class UnitTestVisitorManager
    {
        [Theory]
        [InlineData(null)]
        public void AddVisitor_InValid(Visitor visitor)
        {
            Mock<IVisitorRepository> visitorRepoMock = new Mock<IVisitorRepository>();
            VisitorManager visitorManager = new VisitorManager(visitorRepoMock.Object);

            var ex = Assert.Throws<VisitorManagerException>(() => visitorManager.AddVisitor(visitor));
            Assert.Equal("VisitorManager - AddVisitor - Visitor is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void DeleteVisitor_IsNull(Visitor visitor)
        {
            Mock<IVisitorRepository> visitorRepoMock = new Mock<IVisitorRepository>();
            VisitorManager visitorManager = new VisitorManager(visitorRepoMock.Object);

            var ex = Assert.Throws<VisitorManagerException>(() => visitorManager.DeleteVisitor(visitor));
            Assert.Equal("VisitorManager - DeleteVisitor - No visitor data entry", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "John", "John@Doe.be", "Allphi")]
        [InlineData("Doe", "Jane", "Jane@Doe.be", "Cobus")]
        [InlineData("Doe", "Jake", "Jake@Doe.be", "Hogent")]
        public void DeleteVisitor_DoesNotExist(string lastname, string firstname, string email, string company)
        {
            Visitor visitor = new Visitor(lastname, firstname, email, company);
            Mock<IVisitorRepository> visitorRepoMock = new Mock<IVisitorRepository>();
            visitorRepoMock.Setup(x => x.VisitorExists(visitor)).Returns(false);
            VisitorManager visitorManager = new VisitorManager(visitorRepoMock.Object);

            var ex = Assert.Throws<VisitorManagerException>(() => visitorManager.DeleteVisitor(visitor));
            Assert.Equal("VisitorManager - DeleteVisitor - Visitor does not exist", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0, "Doe", "John", "John@Doe.be", "Allphi")]
        public void UpdateBezoeker_InValid(int visitorid, string? lastname, string? firstname, string? email, string? company)
        {
            Mock<IVisitorRepository> visitorRepoMock = new Mock<IVisitorRepository>();
            VisitorManager visitorManager = new VisitorManager(visitorRepoMock.Object);

            var ex = Assert.Throws<VisitorManagerException>(() => visitorManager.UpdateVisitor(visitorid, lastname, firstname, email, company));
            Assert.Equal("VisitorManager - UpdateVisitor - No id data entry", ex.Message);
        }

        [Theory]
        [InlineData(null, null, null, null)]
        [InlineData("", "", "", "")]
        public void SearchVisitors_InValid(string? lastname, string? firstname, string? email, string? company)
        {
            Mock<IVisitorRepository> visitorRepoMock = new Mock<IVisitorRepository>();
            VisitorManager visitorManager = new VisitorManager(visitorRepoMock.Object);

            var ex = Assert.Throws<VisitorManagerException>(() => visitorManager.SearchVisitors(lastname, firstname, email, company));
            Assert.Equal("VisitorManager - SearchVisitors - Empty fields", ex.InnerException.Message);
        }
    }
}