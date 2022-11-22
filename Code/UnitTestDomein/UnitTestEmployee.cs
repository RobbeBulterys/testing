using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{
    public class UnitTestEmployee
    {
        //private static readonly Company b_Bosteels = new Company(10, "Bosteels brewery", "BE0123123123", "info@example.com");
        //private static readonly Employee werknemer_Josh = new Employee(10, "Baetens", "Josh", b_Bosteels, "programmer");
        [Theory]
        [InlineData("Doe", "John")]
        [InlineData("Doe    ", "    Jane")]
        [InlineData("D   oe", "Ja  ke")]
        public void Constructor_WithoutId_Valid(string name, string firstname)
        {
            Employee w = new Employee(name, firstname);

            Assert.Equal(name.Trim(), w.LastName);
            Assert.Equal(firstname.Trim(), w.FirstName);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("\n", "\n")]
        [InlineData(null, null)]
        public void Constructor_WithoutId_InValid(string name, string firstname)
        {
            Assert.ThrowsAny<PersonException>(() => new Employee(name, firstname));
        }

        [Theory]
        [InlineData(1, "Doe", "John")]
        [InlineData(69, "Doe    ", "    Jane")]
        [InlineData(420, "D   oe", "Ja  ke")]
        public void Constructor_WithId_Valid(int personId, string name, string firstname)
        {
            Employee w = new Employee(personId, name, firstname);

            Assert.Equal(personId, w.PersonId);
            Assert.Equal(name.Trim(), w.LastName);
            Assert.Equal(firstname.Trim(), w.FirstName);
        }

        [Theory]
        [InlineData(0, "", "")]
        [InlineData(-1, " ", " ")]
        [InlineData(-69, "\n", "\n")]
        [InlineData(-420, null, null)]
        public void Constructor_WithId_InValid(int personId, string name, string firstname)
        {
            Assert.ThrowsAny<PersonException>(() => new Employee(personId, name, firstname));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(99)]
        public void SetId_Valid(int id)
        {
            Employee josWithId = new Employee(8, "Joskens", "Jos");

            josWithId.SetId(id);

            Assert.Equal(id, josWithId.PersonId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void SetId_InValid(int id)
        {
            Employee josWithId = new Employee(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersonException>(() => josWithId.SetId(id));
            Assert.Equal("Person - SetId - Id invalid; Less than 1", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        [InlineData("Dirk Dirksen")]
        public void SetLastName_Valid(string name)
        {
            Employee josWithtId = new Employee(8, "Joskens", "Jos");

            josWithtId.SetLastName(name);

            Assert.Equal(name.Trim(), josWithtId.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetLastName_Invalid(string name)
        {
            Employee josWithtId = new Employee(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersonException>(() => josWithtId.SetLastName(name));
            Assert.Equal("Person - SetLastName - No lastname data entry", ex.Message);
        }

        [Theory]
        [InlineData("Dirk")]
        [InlineData("Dirk-Dirksen")]
        [InlineData("    Dirk   ")]
        [InlineData("    Dirk")]
        [InlineData("Dirk   ")]
        public void SetFirstName_Valid(string firstname)
        {
            Employee josWithtId = new Employee(8, "Joskens", "Jos");

            josWithtId.SetFirstName(firstname);

            Assert.Equal(firstname.Trim(), josWithtId.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("\n")]
        [InlineData("   \r   ")]
        [InlineData(null)]
        public void SetFirstName_InValid(string firstname)
        {
            Employee josWithtId = new Employee(8, "Joskens", "Jos");

            var ex = Assert.Throws<PersonException>(() => josWithtId.SetFirstName(firstname));
            Assert.Equal("Person - SetFirstName - No firstname data entry", ex.Message);
        }

        [Fact]
        public void HasSameProperties_Valid()
        {
            Employee josWithtId = new Employee(8, "Joskens", "Jos");
            Assert.True(josWithtId.HasSameProperties(new Employee(8, "Joskens", "Jos")));
        }

        [Theory]
        [InlineData(13, "XXXXX", "Miel", 13, "Troch", "Miel")]
        [InlineData(13, "Troch", "XXXX", 13, "Troch", "Miel")]
        [InlineData(13, "Troch", "Miel", 1, "Troch", "Miel")]
        public void HasSameProperties_Invalid(int id1, string name1, string firstname1, int id2, string name2, string firstname2)
        {
            Employee employee1 = new Employee(id1, name1, firstname1);
            Employee employee2 = new Employee(id2, name2, firstname2);

            Assert.False(employee1.HasSameProperties(employee2));
        }
    }
}
