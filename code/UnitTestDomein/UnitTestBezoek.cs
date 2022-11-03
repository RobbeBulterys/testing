using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein
{

    public class UnitTestBezoek
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(999)]
        public void ZetId_Valid(int id)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetId(id);

            Assert.Equal(id, _bezoek1.BezoekId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-999)]
        public void ZetId_InValid(int id)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.ZetId(id));
        }

        [Fact]
        public void ZetBezoeker_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetBezoeker(new Bezoeker("Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Bezoeker("Niels", "Nelson", "nn@nn.na", "hogent"), _bezoek1.Bezoeker);
        }

        [Fact]
        public void ZetBezoeker_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.ZetBezoeker(null));
        }

        [Fact]
        public void ZetContactpersoon_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetContactpersoon(new Werknemer("tom", "vdw", "lector"));

            Assert.Equal(new Werknemer("tom", "vdw", "lector"), _bezoek1.Contactpersoon);
        }

        [Fact]
        public void ZetContactpersoon_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.ZetBezoeker(null));
        }

        [Fact]
        public void ZetBedrijf_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Bedrijf bedrijf = new Bedrijf(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            _bezoek1.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf, _bezoek1.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.ZetBedrijf(null));
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        [InlineData("22/5/2001")]
        public void ZetStartTijd_Valid(string startTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetStartTijd(DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(startTijd), _bezoek1.StartTijd);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\n")]
        [InlineData(" qdf  ")]
        public void ZetStartTijd_InValidFormat(string startTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<FormatException>(() => _bezoek1.ZetStartTijd(DateTime.Parse(startTijd)));
        }

        [Fact]
        public void ZetEindTijd_Valid()
        {
            string startTijd = "2023/03/21 05:23:12";
            string eindTijd = "2023/03/23 05:24:12";

            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(eindTijd), _bezoek1.EindTijd);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12", "2023/03/19 05:23:12")]
        public void ZetEindTijd_InValid(string startTijd, string eindTijd)
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd)));

        }

        [Fact]
        public void VeranderBezoeker_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.VeranderBezoeker(new Bezoeker(2, "Niels", "Nelson", "nn@nn.na", "hogent"));

            Assert.Equal(new Bezoeker(2, "Niels", "Nelson", "nn@nn.na", "hogent"), _bezoek1.Bezoeker);
        }

        [Fact]
        public void VeranderBezoeker_Invalid_DezelfdeBezoeker()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderBezoeker(new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent")));
        }

        [Fact]
        public void VeranderBezoeker_Invalid_null()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderBezoeker(null));
        }

        [Fact]
        public void VeranderBedrijf_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));
            Bedrijf bedrijf = new Bedrijf("hogent", "BE0231231231", "info@hogent.be");

            _bezoek1.VeranderBedrijf(bedrijf);

            Assert.Equal(bedrijf, _bezoek1.Bedrijf);
        }

        [Fact]
        public void VeranderBedrijf_Invalid_HetzelfdeBedrijf()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderBedrijf(new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be")));
        }

        [Fact]
        public void VeranderBedrijf_Invalid_null()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderBedrijf(null));
        }

        [Fact]
        public void VeranderContacpersoon_Valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            _bezoek1.VeranderContactpersoon(new Werknemer("tom", "vdw", "lector"));

            Assert.Equal(new Werknemer("tom", "vdw", "lector"), _bezoek1.Contactpersoon);
        }

        [Fact]
        public void VeranderContactpersoon_Invalid_HetzelfdeContactpersoon()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderContactpersoon(new Werknemer("Bert", "Bertens", "luierik")));
        }

        [Fact]
        public void VeranderContactpersoon_Invalid_null()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.Throws<BezoekException>(() => _bezoek1.VeranderBezoeker(null));
        }

        [Fact]
        public void HeeftDezelfdeProperties_valid()
        {
            Bezoek _bezoek1 = new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8));

            Assert.True(_bezoek1.IsDezelfde(new Bezoek(8, new Bezoeker(1, "Mike", "Mikes", "mm@mm.me", "hogent"), new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be"), new Werknemer("Bert", "Bertens", "luierik"), DateTime.Today, DateTime.Today.AddHours(8))));
        }
    }
}