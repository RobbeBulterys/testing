using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Moq;
using System.Xml.Linq;
using Xunit;

namespace UnitTestManagers
{
    public class UnitTestEmployeeManager
    {
        [Theory]
        [InlineData(null)]
        public void AddEmployee_InValid(Employee employee)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.AddEmployee(employee));
            Assert.Equal("EmployeeManager - AddEmployee - Employee is null", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0)]
        public void EmployeeExists_Id_InValid(int employeeId)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.EmployeeExists(employeeId));
            Assert.Equal("EmployeeManager - EmployeeExists - Invalid Employeeid", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        public void EmployeeExists_Name_InValid(string lastname, string firstname)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.EmployeeExists(lastname, firstname));
            Assert.Equal("EmployeeManager - EmployeeExists - No Lastname and/or firstname data entry", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0)]
        public void DeleteEmployee_IsNull(int employeeId)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.DeleteEmployee(employeeId));
            Assert.Equal("EmployeeManager - DeleteEmployee - Invalid employeeid", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteEmployee_DoesNotExist(int employeeId)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            employeeRepoMock.Setup(x => x.EmployeeExists(employeeId)).Returns(false);
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.DeleteEmployee(employeeId));
            Assert.Equal("EmployeeManager - DeleteEmployee - employeeid does not exist", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(1, null, null)]
        [InlineData(2, "", "")]
        [InlineData(69, "      ", "   ")]
        [InlineData(420, "\n", "\n")]
        [InlineData(710, "   \r   ", "   \r   ")]
        public void UpdateEmployee_InValid(int employeeId, string? name, string? FirstName)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.UpdateEmployee(employeeId, name, FirstName));
            Assert.Equal("EmployeeManager - UpdateEmployee - No data entry", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "")]
        public void SearchEmployees_InValid(string? lastname, string? firstname)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.SearchEmployees(lastname, firstname));
            Assert.Equal("EmployeeManager - SearchEmployees - Fields not filled in", ex.InnerException.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void GetEmployee_InValid(int employeeId)
        {
            Mock<IEmployeeRepository> employeeRepoMock = new Mock<IEmployeeRepository>();
            EmployeeManager EM = new EmployeeManager(employeeRepoMock.Object);

            var ex = Assert.Throws<EmployeeManagerException>(() => EM.GetEmployee(employeeId));
            Assert.Equal("EmployeeManager - GetEmployee - Invalid employeeid", ex.InnerException.Message);
        }  
    }
}