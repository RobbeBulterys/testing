using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using System.ComponentModel.Design;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestEmployeecontractManager
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, -1)]
        [InlineData(-2, 2)]
        public void ContractExists_InValid(int employeeId, int companyId)
        {
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.ContractExists(employeeId, companyId));
            Assert.Equal("EmployeecontractManager - ContractExists - Invalid id's", ex.InnerException.Message);
        }

        [Theory]
        [InlineData("Doe", "John", "Allphi", "BE0123321123", "info@allphi.com", "developer", "John@allphi.com")]
        [InlineData("Doe", "Jane", "Cobus", "BE0123451234", "info@cobus.com", "nitwit", "Jane@Cobus.com")]
        [InlineData("Doe", "Jake", "Hogent", "BE0543211234", "info@hogent.com", "Teacher", "Jake@Hogent.com")]
        public void UpdateContract_IdInValid(string lastname, string firstname, string name, string vatNumber, string companyEmail, string function, string email)
        {
            Employee employee = new Employee(lastname, firstname);
            Company company = new Company(name, vatNumber, companyEmail);
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.UpdateContract(employee, company, function, email));
            Assert.Equal("UpdateContract - employeeid or companyid invalid", ex.Message);
        }

        [Theory]
        [InlineData(1, "Doe", "John", 1, "Allphi", "BE0123321123", "info@allphi.com", "developer", "John@allphi.com")]
        [InlineData(2, "Doe", "Jane", 2, "Cobus", "BE0123451234", "info@cobus.com", "nitwit", "Jane@Cobus.com")]
        [InlineData(420, "Doe", "Jake", 69, "Hogent", "BE0543211234", "info@hogent.com", "Teacher", "Jake@Hogent.com")]
        public void UpdateContract_DoesNotExist(int personId, string lastname, string firstname, int companyId, string name, string vatNumber, string companyEmail, string function, string email)
        {
            Employee employee = new Employee(personId, lastname, firstname);
            Company company = new Company(companyId, name, vatNumber, companyEmail);
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            employeecontractRepoMock.Setup(x => x.ContractExists(personId, companyId)).Returns(false);
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.UpdateContract(employee, company, function, email));
            Assert.Equal("UpdateContract - contract does not exist", ex.Message);
        }

        [Theory]
        [InlineData(1, "Doe", "John", 1, "Allphi", "BE0123321123", "info@allphi.com", null, null)]
        [InlineData(2, "Doe", "Jane", 2, "Cobus", "BE0123451234", "info@cobus.com", "", "")]
        [InlineData(420, "Doe", "Jake", 69, "Hogent", "BE0543211234", "info@hogent.com", "     ", "       ")]
        public void UpdateContract_NoFunctionOrEmailFildIn(int personId, string lastname, string firstname, int companyId, string name, string vatNumber, string companyEmail, string function, string email)
        {
            Employee employee = new Employee(personId, lastname, firstname);
            Company company = new Company(companyId, name, vatNumber, companyEmail);
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            employeecontractRepoMock.Setup(x => x.ContractExists(personId, companyId)).Returns(true);
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.UpdateContract(employee, company, function, email));
            Assert.Equal("UpdateContract - no function or email parameter data entries" , ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void DeleteContract_IsNull(Employeecontract employeecontract)
        {
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.DeleteContract(employeecontract));
            Assert.Equal("EmployeecontractManager - DeleteContract - no contract data entry", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "Doe", "John", 1, "Allphi", "BE0123321123", "info@allphi.com", "developer")]
        [InlineData(2, "Doe", "Jane", 2, "Cobus", "BE0123451234", "info@cobus.com", "nitwit")]
        [InlineData(420, "Doe", "Jake", 69, "Hogent", "BE0543211234", "info@hogent.com", "Teacher")]
        public void DeleteContract_DoesNotExist(int personId, string lastname, string firstname, int companyId, string name, string vatNumber, string companyEmail, string function)
        {
            Employee employee = new Employee(personId, lastname, firstname);
            Company company = new Company(companyId, name, vatNumber, companyEmail);
            Employeecontract employeecontract = new Employeecontract(company, employee, function);
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            employeecontractRepoMock.Setup(x => x.ContractExists(personId, companyId)).Returns(false);
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.DeleteContract(employeecontract));
            Assert.Equal("EmployeecontractManager - DeleteContract - Contract does not exist", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null)]
        public void AddContract_IsNull(Employeecontract employeecontract)
        {
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.AddContract(employeecontract));
            Assert.Equal("EmployeecontractManager - AddContract - no contract data entry", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, "Doe", "John", 1, "Allphi", "BE0123321123", "info@allphi.com", "developer")]
        [InlineData(2, "Doe", "Jane", 2, "Cobus", "BE0123451234", "info@cobus.com", "nitwit")]
        [InlineData(420, "Doe", "Jake", 69, "Hogent", "BE0543211234", "info@hogent.com", "Teacher")]
        public void AddContract_AllreadyExists(int personId, string lastname, string firstname, int companyId, string name, string vatNumber, string companyEmail, string function)
        {
            Employee employee = new Employee(personId, lastname, firstname);
            Company company = new Company(companyId, name, vatNumber, companyEmail);
            Employeecontract employeecontract = new Employeecontract(company, employee, function);
            Mock<IEmployeecontractRepository> employeecontractRepoMock = new Mock<IEmployeecontractRepository>();
            employeecontractRepoMock.Setup(x => x.ContractExists(personId, companyId)).Returns(true);
            EmployeecontractManager EM = new EmployeecontractManager(employeecontractRepoMock.Object);

            var ex = Assert.Throws<EmployeecontractManagerException>(() => EM.AddContract(employeecontract));
            Assert.Equal(("EmployeecontractManager - AddContract - Employeecontract already exists"), ex.InnerException.Message);
        } 
    }
}