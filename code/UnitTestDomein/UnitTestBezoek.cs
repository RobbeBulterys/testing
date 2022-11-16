using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{

    public class UnitTestBezoek
    {
        [Fact]
        public void Constructor_ZonderId_Valid()
        {
            Bezoeker bezoeker = new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent");
            Bedrijf bedrijf = new Bedrijf(1, "Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be");
            Werknemer werknemer = new Werknemer(1, "Bert", "Bertens");
            DateTime Starttijd = DateTime.Today;
            Bezoek b = new Bezoek(bezoeker, bedrijf, werknemer, Starttijd);

            Assert.Equal(bezoeker, b.Bezoeker);
            Assert.Equal(bedrijf, b.Bedrijf);
            Assert.Equal(werknemer, b.Contactpersoon);
            Assert.Equal(Starttijd, b.StartTijd);
        }

        [Fact]
        public void Constructor_ZonderId_InValid()
        {
            Bezoeker bezoeker = null;
            Bedrijf bedrijf = null;
            Werknemer werknemer = null;
            DateTime Starttijd = new DateTime();

            Assert.ThrowsAny<BezoekException>(() => new Bezoek(bezoeker, bedrijf, werknemer, Starttijd));
        }

        [Fact]
        public void Constructor_MetId_Valid()
        {
            int id = 1;
            Bezoeker bezoeker = new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent");
            Bedrijf bedrijf = new Bedrijf(1, "Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be");
            Werknemer werknemer = new Werknemer(1, "Bert", "Bertens");
            DateTime Starttijd = DateTime.Today;
            DateTime Eindtijd = Starttijd.AddHours(4);
            Bezoek b = new Bezoek(id, bezoeker, bedrijf, werknemer, Starttijd, Eindtijd);

            Assert.Equal(id, b.BezoekId);
            Assert.Equal(bezoeker, b.Bezoeker);
            Assert.Equal(bedrijf, b.Bedrijf);
            Assert.Equal(werknemer, b.Contactpersoon);
            Assert.Equal(Starttijd, b.StartTijd);
            Assert.Equal(Eindtijd, b.EindTijd);
        }

        [Fact]
        public void Constructor_MetId_InValid()
        {
            int id = 0;
            Bezoeker bezoeker = null;
            Bedrijf bedrijf = null;
            Werknemer werknemer = null;
            DateTime Starttijd = new DateTime();
            DateTime Eindtijd = new DateTime();

            Assert.ThrowsAny<BezoekException>(() => new Bezoek(id, bezoeker, bedrijf, werknemer, Starttijd, Eindtijd));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetId(id);

            Assert.Equal(id, _bezoek1.BezoekId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetId(id));
            Assert.Equal("Bezoek - ZetId - Id ongeldig; Kleiner dan 1", ex.Message);
        }

        [Fact]
        public void ZetBezoeker_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetBezoeker(new Bezoeker("Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Bezoeker("Niels", "Nelson", "nn@nn.na", "hogent"), _bezoek1.Bezoeker);
        }

        [Fact]
        public void ZetBezoeker_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetBezoeker(null));
            Assert.Equal("Bezoek - ZetBezoeker - Geen bezoeker ingevuld", ex.Message);
        }

        [Fact]
        public void ZetContactpersoon_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetContactpersoon(new Werknemer("tom", "vdw"));

            Assert.Equal(new Werknemer("tom", "vdw"), _bezoek1.Contactpersoon);
        }

        [Fact]
        public void ZetContactpersoon_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetContactpersoon(null));
            Assert.Equal("Bezoek - ZetContactpersoon - Geen contactpersoon ingevuld", ex.Message);
        }

        [Fact]
        public void ZetBedrijf_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            Bedrijf bedrijf = new Bedrijf(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            _bezoek1.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf, _bezoek1.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetBedrijf(null));
            Assert.Equal("Bezoek - ZetBedrijf - Geen bedrijf ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        [InlineData("22/5/2001")]
        public void ZetStartTijd_Valid(string startTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetStartTijd(DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(startTijd), _bezoek1.StartTijd);
        }

        private DateTime startTijd;
        [Fact]
        public void ZetStartTijd_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetStartTijd(startTijd));
            Assert.Equal("Bezoek - ZetStartTijd - Geen starttijd ingevuld", ex.Message);
        }

        [Fact]
        public void ZetEindTijd_Valid()
        {
            string startTijd = "2023/03/21 05:23:12";
            string eindTijd = "2023/03/23 05:24:12";

            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(eindTijd), _bezoek1.EindTijd);
        }

        private DateTime eindTijd;
        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        public void ZetEindTijd_NietIngevuld(string startTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetEindTijd(eindTijd, DateTime.Parse(startTijd)));
            Assert.Equal("Bezoek - ZetEindTijd - Geen eindtijd ingevuld", ex.Message);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12", "2023/03/19 05:23:12")]
        public void ZetEindTijd_VoorStartTijd(string startTijd, string eindTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd)));
            Assert.Equal("Bezoek - ZetEindTijd - Eindtijd kan niet voor starttijd liggen", ex.Message);
        }

        [Fact]
        public void VeranderBezoeker_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.VeranderBezoeker(new Bezoeker(2, "Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Bezoeker(2, "Niels", "Nelson", "nn@nn.na", "hogent"), _bezoek1.Bezoeker);
        }

        [Fact]
        public void VeranderBezoeker_IsDezelfde()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));


            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderBezoeker(new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent")));
            Assert.Equal("Bezoek - VeranderBezoeker - Bezoeker is hetzelfde", ex.Message);
        }

        [Fact]
        public void VeranderBezoeker_Isnull()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderBezoeker(null));
            Assert.Equal("Bezoek - VeranderBezoeker - Geen bezoeker ingevuld", ex.Message);
        }

        [Fact]
        public void VeranderBedrijf_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            Bedrijf bedrijf = new Bedrijf("hogent", "BE0231231231", "info@hogent.be");

            _bezoek1.VeranderBedrijf(bedrijf);

            Assert.Equal(bedrijf, _bezoek1.Bedrijf);
        }

        [Fact]
        public void VeranderBedrijf_Isdezelfde()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderBedrijf(new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be")));
            Assert.Equal("Bezoek - VeranderBedrijf - Bedrijf is hetzelfde", ex.Message);
        }

        [Fact]
        public void VeranderBedrijf_Isnull()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderBedrijf(null));
            Assert.Equal("Bezoek - VeranderBedrijf - Geen bedrijf ingevuld", ex.Message);
        }

        [Fact]
        public void VeranderContacpersoon_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.VeranderContactpersoon(new Werknemer("tom", "vdw"));

            Assert.Equal(new Werknemer("tom", "vdw"), _bezoek1.Contactpersoon);
        }

        [Fact]
        public void VeranderContactpersoon_IsDezelfde()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderContactpersoon(new Werknemer("Bert", "Bertens")));
            Assert.Equal("Bezoek - VeranderContactpersoon - Contactpersoon is hetzelfde", ex.Message);
        }

        [Fact]
        public void VeranderContactpersoon_IsNull()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));

            var ex = Assert.Throws<BezoekException>(() => _bezoek1.VeranderContactpersoon(null));
            Assert.Equal("Bezoek - VeranderContactpersoon - Geen contactpersoon ingevuld", ex.Message);
        }

        [Fact]
        public void IsDezelfde_Valid_()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            
            Assert.True(_bezoek1.IsDezelfde(new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8))));
        }

        [Fact]
        public void IsDezelfde_Invalid()
        {
            Bezoek bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens"), DateTime.Today, DateTime.Today.AddHours(8));
            Bezoek bezoek2 = new Bezoek(88, new Bezoeker(10, "John", "Doe", "john@doe.me", "kwak"), new Bedrijf("Hogent", "BE0424524527", "info@hogent.be"), new Werknemer("Jos", "Josens"), DateTime.Today.AddDays(4), DateTime.Today.AddDays(4).AddHours(8));

            Assert.False(bezoek1.IsDezelfde(bezoek2));
        }
    }
}