using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using Xunit;

namespace UnitTestManagers {
    public class UnitTestCompanyManager {
        
        [Theory]
        [InlineData("BE0123321123", "Allphi", "info@allphi.com", "+320412345678", "Belgie", "Kompasplein", "19", "9000", "Gent")]
        [InlineData("BE0123451234", "Cobus", "info@cobus.com", "+320412344321", "Belgie", "Quai des Charbonnages", "23", "1030", "Molenbeek")]
        [InlineData("BE0543211234", "Hogent", "info@hogent.com", "+320487654321", "Belgie", "Taborastraat", "13", "8300", "Knokke-Heist")]
        public void AddCompany_InValid(string vatnumber, string name, string email, string? phonenumber, string? country, string? street, string? number, string? postalcode, string? place) {
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<IEmployeecontractRepository> employeeRepoMock = new Mock<IEmployeecontractRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);
            EmployeecontractManager ECM = new EmployeecontractManager(employeeRepoMock.Object);
            companyRepoMock.Setup(x => x.CompanyExistsWithoutId(vatnumber, name, email)).Returns(true);
            CompanyManager CM = new CompanyManager(companyRepoMock.Object, AM, ECM);

            var ex = Assert.Throws<CompanyManagerException>(() => CM.AddCompany(vatnumber, name, email, phonenumber, country, street, number, postalcode, place));
            Assert.Equal("CompanyManager - AddCompany - Company already exists", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "BE0123321123", "Allphi", "info@allphi.com", 1)]
        [InlineData(3, "BE0123451234", "Cobus", "info@cobus.com", 2)]
        [InlineData(420, "BE0543211234", "Hogent", "info@hogent.com", 69)]
        public void DeleteCompany_InValid(int id, string vatNumber, string name, string email, int? addressid) {

            Company company = new Company(id, name, vatNumber, email);
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<IEmployeecontractRepository> employeeRepoMock = new Mock<IEmployeecontractRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);
            EmployeecontractManager ECM = new EmployeecontractManager(employeeRepoMock.Object);
            companyRepoMock.Setup(x => x.CompanyExistsWithId(id)).Returns(false);
            CompanyManager CM = new CompanyManager(companyRepoMock.Object, AM, ECM);

            var ex = Assert.Throws<CompanyManagerException>(() => CM.DeleteCompany(company, addressid));
            Assert.Equal("CompanyManager - DeleteCompany - Company does not exist", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "BE0123321123", "Allphi", "info@allphi.com", "+320412345678")]
        [InlineData(2, "BE0123451234", "Cobus", "info@cobus.com", "+320412344321")]
        [InlineData(99, "BE0543211234", "Hogent", "info@hogent.com", "+320487654321")]
        public void UpdateCompany_InValid(int id, string? vatnumber, string? name, string? email, string? phonenumber) {
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<IEmployeecontractRepository> employeeRepoMock = new Mock<IEmployeecontractRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);
            EmployeecontractManager ECM = new EmployeecontractManager(employeeRepoMock.Object);
            companyRepoMock.Setup(x => x.CompanyExistsWithId(id)).Returns(false);
            CompanyManager CM = new CompanyManager(companyRepoMock.Object, AM, ECM);

            var ex = Assert.Throws<CompanyManagerException>(() => CM.UpdateCompany(id, vatnumber, name, email, phonenumber));
            Assert.Equal("CompanyManager - Updatecompany - Company is the same", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 2)]
        [InlineData(69, 420)]
        public void UpdateCompanyAddress_InValid(int id, int adresId) {
            Mock<ICompanyRepository> companyRepoMock = new Mock<ICompanyRepository>();
            Mock<IAddressRepository> addressRepoMock = new Mock<IAddressRepository>();
            Mock<IEmployeecontractRepository> employeeRepoMock = new Mock<IEmployeecontractRepository>();
            AddressManager AM = new AddressManager(addressRepoMock.Object, companyRepoMock.Object);
            EmployeecontractManager ECM = new EmployeecontractManager(employeeRepoMock.Object);
            companyRepoMock.Setup(x => x.CompanyExistsWithId(id)).Returns(false);
            CompanyManager CM = new CompanyManager(companyRepoMock.Object, AM, ECM);

            var ex = Assert.Throws<CompanyManagerException>(() => CM.UpdateCompanyAddress(id, adresId));
            Assert.Equal("CompanyManager - UpdateCompanyAddress - Company is the same", ex.InnerException.Message);
        }
    }
}