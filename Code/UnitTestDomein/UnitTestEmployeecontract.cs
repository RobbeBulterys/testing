using BL_Projectwerk.Domein;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using Xunit;

namespace UnitTestDomein
{
    public class UnitTestEmployeecontract
    {
        [Fact]
        public void Constructor_Valid()
        {
            Company company = new Company("Jantje", "BE0012345678", "info.Brewery@example.com");
            Employee employee = new Employee("Doe", "John");
            string function = "programeur";
            Employeecontract ec = new Employeecontract(company, employee, function);

            Assert.Equal(company, ec.Company);
            Assert.Equal(employee, ec.Employee);
            Assert.Equal(function, ec.Function);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\n", "\n")]
        [InlineData(null, null)]
        public void Constructor_InValid(string lastname, string firstname)
        {
            Assert.ThrowsAny<PersonException>(() => new Employee(lastname, firstname));
        }

        [Theory]
        [InlineData("information@example.com")]
        [InlineData("   info.Brewery@example.com")]
        [InlineData("   customerService@example.be   ")]
        public void SetEmail_Valid(string email)
        {
            Company company = new Company("Jantje", "BE0012345678", "info.Brewery@example.com");
            Employee employee = new Employee("Doe", "John");
            string function = "programeur";
            Employeecontract ec = new Employeecontract(company, employee, function);

            ec.SetEmail(email);

            Assert.Equal(email.Trim(), ec.Email);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetEmail_IsNull(string email)
        {
            Company company = new Company("Jantje", "BE0012345678", "info.Brewery@example.com");
            Employee employee = new Employee("Doe", "John");
            string function = "programeur";
            Employeecontract ec = new Employeecontract(company, employee, function);

            var ex = Assert.Throws<EmployeecontractException>(() => ec.SetEmail(email));
            Assert.Equal("Employeecontract - SetEmail - No email data entry", ex.Message);
        }

        [Theory]
        [InlineData("informationexample.com")]
        [InlineData("@example.com")]
        [InlineData("customerService@examplebe")]
        public void SetEmail_InValidSyntax(string email)
        {
            Company company = new Company("Jantje", "BE0012345678", "info.Brewery@example.com");
            Employee employee = new Employee("Doe", "John");
            string function = "programeur";
            Employeecontract ec = new Employeecontract(company, employee, function);

            var ex = Assert.Throws<VerifyException>(() => ec.SetEmail(email));
            Assert.Equal("Employeecontract - SetEmail - Invalid email", ex.Message);
        }
        [Theory]
        [InlineData("Programeur")]
        [InlineData("Boek   houder")]
        [InlineData("     Receptioniste")]
        public void SetFunction_Valid(string function)
        {
            Company company = new Company("Allphi", "BE0123123123", "allphi@info.be");
            Employee employee = new Employee("VanDeWiele", "Tom");
            Employeecontract ec = new Employeecontract(company, employee, "werkloos");

            ec.SetFunction(function);

            Assert.Equal(function.Trim(), ec.Function);
        }
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetFunction_InValid(string function)
        {
            Company company = new Company("Allphi", "BE0123123123", "allphi@info.be");
            Employee employee = new Employee("VanDeWiele", "Tom");
            Employeecontract ec = new Employeecontract(company, employee, "werkloos");

            var ex = Assert.Throws<EmployeecontractException>(() => ec.SetFunction(function));
            Assert.Equal("Employeecontract - SetFunction - No function data entry", ex.Message);
        }

        [Fact]
        public void HasSameProperties_Valid()
        {
            Company company = new Company("Allphi", "BE0123123123", "allphi@info.be");
            Employee employee = new Employee("VanDeWiele", "Tom");
            Employeecontract ec = new Employeecontract(company, employee, "werkloos");

            Assert.True(ec.HasSameProperties(new Employeecontract(new Company("Allphi", "BE0123123123", "allphi@info.be"), new Employee("VanDeWiele", "Tom"), "werkloos")));
        }

        [Fact]
        public void HasSameProperties_Invalid()
        {
            Company company1 = new Company("Allphi", "BE0123123123", "allphi@info.be");
            Employee employee1 = new Employee("VanDeWiele", "Tom");
            Employeecontract ec1 = new Employeecontract(company1, employee1, "werkloos");
            Company company2 = new Company("Hogent", "BE0321321321", "hogent@info.be");
            Employee employee2 = new Employee("doe", "john");
            Employeecontract ec2 = new Employeecontract(company2, employee2, "programeur");

            Assert.False(ec1.HasSameProperties(ec2));
        }
    }
}
