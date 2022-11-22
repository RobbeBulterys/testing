using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using System.Collections;
using Xunit;

namespace UnitTestManagers {
    public class UnitTestAddressManager {

        //private static readonly Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
        //private static readonly Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
        ////private static readonly Mock<IEmployeecontractRepository> employeeRepoMock = new Mock<IEmployeecontractRepository>();
        //public static readonly AddressManager _am = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);
        //private static int getal = 5;
        //public UnitTestAddressManager() {
        //    _am.AddAddress(new Address("Street", "2", "9000", "Gent", "Belgium"));
        //    Console.WriteLine(getal);
        //}

        //[Fact]
        //public void AddCompany_invalid() {
        //    UnitTestAddressManager addressManager = new UnitTestAddressManager();
        //    addressManager.AddCompany_invalid();
        //    Assert.ThrowsAny<Exception>(() => _am.AddAddress(new Address("Street", "2", "9000", "Gent", "Belgium")));
        //}

        [Theory]
        [InlineData("Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData("Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData("Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void AddAddress_AddresswithoutId(string street, string number, string postalcode, string place, string country) {
            Address a = new Address(street, number, postalcode, place, country);
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            addressRepoMock.Setup(x => x.AddressExistsWithoutId(a)).Returns(true);
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);

            Assert.Equal(addressRepoMock.Object.GetAddressId(a), AM.AddAddress(a));
        }

        [Theory]
        [InlineData(null)]
        public void AddAddress_IsNull(Address value) {
            Address a = value;
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);

            var ex = Assert.Throws<AddressManagerException>(() => AM.AddAddress(a));
            Assert.Equal("AddressManager - AddAddress - Address is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-99)]
        public void DeleteAddress_DoesNotExist(int id) {
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            addressRepoMock.Setup(x => x.AddressExistsWithId(id)).Returns(false);
            companyRepoMock.Setup(x => x.CompaniesPresentAtAddress(id)).Returns(true);
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);

            var ex = Assert.Throws<AddressManagerException>(() => AM.DeleteAddress(id));
            Assert.Equal("AddressManager - DeleteAddress - Address does not exist", ex.InnerException.Message);
        }

        [Fact]
        public void DeleteAddress_CompanyNotPresent() {
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            addressRepoMock.Setup(x => x.AddressExistsWithId(1)).Returns(true);
            companyRepoMock.Setup(x => x.CompaniesPresentAtAddress(1)).Returns(false);
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);

            var ex = Assert.Throws<AddressManagerException>(() => AM.DeleteAddress(1));
            Assert.Equal("AddressManager - DeleteAddress - Not able to delete an address when a company is still present", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "Kompasplein", "19", "9000", "Gent", "België")]
        [InlineData(69, "Quai des Charbonnages", "23", "1030", "Molenbeek", "België")]
        [InlineData(420, "Taborastraat", "13", "8300", "Knokke-Heist", "België")]
        public void UpdateAdres_InValid(int id, string street, string number, string postalcode, string place, string country) {
            Address a = new Address(id, street, number, postalcode, place, country);
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);

            var ex = Assert.Throws<AddressManagerException>(() => AM.UpdateAddress(id, street, number, postalcode, place, country));
            Assert.Equal("AddressManager - UpdateAddress - Address does not exist", ex.InnerException.Message);
        }
    }
}