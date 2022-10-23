using Xunit;
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;

namespace UnitTestDomein {

    public class UnitTestBezoek {
        private readonly static Bedrijf _bosteels = new Bedrijf("Bosteels Brewery", "BE0461231231", "info@kwak.karmeliet.be");
        private readonly static Werknemer _werknemerTomZonderId = new Werknemer("tom", "vdw", new Bedrijf("hogent", "BE0456456456", "i@kd.be"), "lector");
        private readonly static Werknemer _werknemerBertZonderId = new Werknemer("Bert", "Bertens", _bosteels, "luierik");
        private readonly static Bezoeker _bezoekerMikeZonderId = new Bezoeker("Mike", "Mikes", "mm@mm.me", "hogent");
        private readonly static Bezoeker _bezoekerNielsZonderId = new Bezoeker("Niels", "Nelson", "nn@nn.na", "hogent");
        private readonly Bezoek _bezoek1 = new Bezoek(2, _bezoekerMikeZonderId, _werknemerBertZonderId.Bedrijf, _werknemerBertZonderId, DateTime.Today, DateTime.Today.AddHours(8));
        //private readonly static Bezoek _bezoek1anderAdres = new Bezoek(2, _mike, _bert.Bedrijf, _bert, DateTime.Today, DateTime.Today.AddHours(8));

        [Theory]
        [InlineData(1)]
        [InlineData(299)]
        public void ZetId_Valid(int id) {
            // Fact is infeite voldoende: randwaarde 1 tot +oneindig
            Bezoek b = _bezoek1;

            b.ZetId(id);

            Assert.Equal(id, b.BezoekId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ZetId_InValid(int id) {
            Bezoek b = _bezoek1;

            Assert.Throws<BezoekException>(() => b.ZetId(id));
        }

        [Fact]
        public void ZetBezoeker_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bezoekerMikeZonderId, b.Bezoeker);

            b.ZetBezoeker(_bezoekerNielsZonderId);

            Assert.Equal(_bezoekerNielsZonderId, b.Bezoeker);
        }

        [Fact]
        public void ZetBezoeker_InValid() {
            Bezoek b = _bezoek1;

            Assert.Throws<BezoekException>(() => b.ZetBezoeker(null));
        }


        [Fact]
        public void ZetContactpersoon_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_werknemerBertZonderId, b.Contactpersoon);
            

            b.ZetContactpersoon(_werknemerTomZonderId);

            Assert.Equal(_werknemerTomZonderId, b.Contactpersoon);
        }

        [Fact]
        public void ZetContactpersoon_InValid() {
            Bezoek b = _bezoek1;

            Assert.Throws<BezoekException>(() => b.ZetBezoeker(null));
        }

        [Fact]
        public void ZetBedrijf_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bosteels, b.Bedrijf);
            Bedrijf bedrijf = new Bedrijf(3, "Bosteels brewery", "BE0123123123", "info@example.com");
            b.ZetBedrijf(bedrijf);

            Assert.Equal(bedrijf, b.Bedrijf);
        }

        [Fact]
        public void ZetBedrijf_InValid() {
            Bezoek b = _bezoek1;

            Assert.Throws<BezoekException>(() => b.ZetBedrijf(null));
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12")]
        [InlineData("22 januari 2022")]
        [InlineData("22/5/2001")]
        public void ZetStartTijd_Valid(string startTijd) {
            Bezoek b = _bezoek1;
            Assert.NotEqual(DateTime.Parse(startTijd), b.StartTijd);
            b.ZetStartTijd(DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(startTijd), b.StartTijd);
        }

        [Theory]
        [InlineData("")]
        [InlineData("\n")]
        [InlineData(" qdf  ")]
        public void ZetStartTijd_InValidFormat(string startTijd) {
            Bezoek b = _bezoek1;
            // DateTime

            Assert.Throws<FormatException>(() => b.ZetStartTijd(DateTime.Parse(startTijd)));
        }

        [Fact]
        public void ZetEindTijd_Valid() {
            string startTijd = "2023/03/21 05:23:12";
            string eindTijd = "2023/03/23 05:24:12";

            Bezoek b = _bezoek1;
            
            b.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd));

            Assert.Equal(DateTime.Parse(eindTijd), b.EindTijd);
        }

        [Theory]
        [InlineData("2023/03/21 05:23:12", "2023/03/19 05:23:12")]
        [InlineData("2023/03/21 05:23:12", "2022/03/19 05:23:12")]
        public void ZetEindTijd_InValid(string startTijd, string eindTijd) {
            Bezoek b = _bezoek1;

            Assert.Throws<BezoekException>(() => b.ZetEindTijd(DateTime.Parse(eindTijd), DateTime.Parse(startTijd)));

        }

        [Fact]
        public void VeranderBezoeker_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bezoekerMikeZonderId, b.Bezoeker);
            b.VeranderBezoeker(_bezoekerNielsZonderId);
            Assert.Equal(_bezoekerNielsZonderId, b.Bezoeker);
        }

        [Fact]
        public void VeranderBezoeker_Invalid_DezelfdeBezoeker() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bezoekerMikeZonderId, b.Bezoeker);
            Assert.Throws<BezoekException>(() => b.VeranderBezoeker(_bezoekerMikeZonderId));
        }
        [Fact]
        public void VeranderBezoeker_Invalid_null() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bezoekerMikeZonderId, b.Bezoeker);
            Assert.Throws<BezoekException>(() => b.VeranderBezoeker(null));
        }

        [Fact]
        public void VeranderBedrijf_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bosteels, b.Bedrijf);

            Bedrijf bedrijf = new Bedrijf("hogent", "BE0231231231", "info@hogent.be");
            b.VeranderBedrijf(bedrijf);
            Assert.Equal(bedrijf, b.Bedrijf);
        }

        [Fact]
        public void VeranderBedrijf_Invalid_HetzelfdeBedrijf() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bosteels, b.Bedrijf);
            Assert.Throws<BezoekException>(() => b.VeranderBedrijf(_bosteels));
        }
        [Fact]
        public void VeranderBedrijf_Invalid_null() {
            Bezoek b = _bezoek1;
            Assert.Equal(_bosteels, b.Bedrijf);
            Assert.Throws<BezoekException>(() => b.VeranderBedrijf(null));
        }

        [Fact]
        public void VeranderContacpersoon_Valid() {
            Bezoek b = _bezoek1;
            Assert.Equal(_werknemerBertZonderId, b.Contactpersoon);
            b.VeranderContactpersoon(_werknemerTomZonderId);
            Assert.Equal(_werknemerTomZonderId, b.Contactpersoon);
        }

        [Fact]
        public void VeranderContactpersoon_Invalid_HetzelfdeContactpersoon() {
            Bezoek b = _bezoek1;
            Assert.Equal(_werknemerBertZonderId, b.Contactpersoon);
            Assert.Throws<BezoekException>(() => b.VeranderContactpersoon(_werknemerBertZonderId));
        }
        [Fact]
        public void VeranderContactpersoon_Invalid_null() {
            Bezoek b = _bezoek1;
            Assert.Equal(_werknemerBertZonderId, b.Contactpersoon);
            Assert.Throws<BezoekException>(() => b.VeranderBezoeker(null));
        }

        [Fact]
        public void HeeftDezelfdeProperties_valid() {
            // Bezoek 1 en 2 hebben dezelfde properties
            Bezoek b1 = _bezoek1;
            Bezoek b2 = new Bezoek(2, _bezoekerMikeZonderId, _werknemerBertZonderId.Bedrijf, _werknemerBertZonderId, DateTime.Today, DateTime.Today.AddHours(8));
            Assert.True(b1.IsDezelfde(b2));
        }
    }
}