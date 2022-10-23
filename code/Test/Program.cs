using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using Microsoft.IdentityModel.Tokens;
using TriggerMe.VAT;


namespace Program {
    public class Program {

        static void Main(string[] args)
        {
            //var vatQuery = new VATQuery();

            //var vatResult = vatQuery.CheckVATNumberAsync("IE", "3041081MH");
            //Console.WriteLine(vatResult);


            // Testen met datetime
            //Bezoeker bezoeker = new Bezoeker("Jos", "Jos", "jj@e.be", "Bos");
            //Bedrijf bedrijf = new Bedrijf("Jupi", "BE123", "info@blmkqjfmlkjak.be");
            //Werknemer werknemer = new Werknemer("Engelke", "Bengelke", bedrijf, "packaging manager en boekhouding");
            //Bezoek bezoek = new Bezoek(bezoeker, bedrijf, werknemer, DateTime.Now);
            //bezoek.ZetEindTijd(new DateTime(), DateTime.Now);
            //Console.WriteLine();
            // Dus DateTime kan niet null zijn

            // Test AdresManager
        }
                        private IAdresRepository ar;

        Adres a = new Adres("stationstraat", "36C", "9000", "Gent", "Belgie");


        //AdresManager am = new AdresManager(ar);
    } 
}




