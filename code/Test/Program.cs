using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk;
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





        // TESTS ADO -----------------------------------------------------------------------------------------------------------------------------------------------
        string connectionString = "Data Source=FRENK\\SQLEXPRESS;Initial Catalog=\"projectwerk test\";Integrated Security=True";
        AdresRepoADO adresRepo = new AdresRepoADO(connectionString);

            // Test BestaatAdresZonderID---------------------------------------------------------
            //Adres a = new Adres("Kerkstraat", "3", "9000", "Gent", "Belgie");
            //Console.WriteLine(adresRepo.BestaatAdresZonderId(a));             // werkt

            // Test BestaatAdresMetID------------------------------------------------------------
            //Adres a = new Adres(1,"Kerkstraat", "4", "9000", "Gent", "Belgie");
            //Console.WriteLine(adresRepo.BestaatAdresMetId(a));               // werkt

            // Test VoegAdresToe-----------------------------------------------------------------
            //Adres a = new Adres("Dorp", "3", "9810", "Nazareth", "Belgie");
            //Console.WriteLine(adresRepo.VoegAdresToe(a).ToString());         // werkt

            // Test GeefAdresMetId
            //Adres a = new Adres("Dorp", "3", "9810", "Nazareth", "Belgie");
            //Console.WriteLine(adresRepo.GeefAdresMetId(a).Id);         // werkt

            // Test VerwijderAdres
            //Adres a = new Adres(10,"Dorp", "3", "9810", "Nazareth", "Belgie");
            //adresRepo.VerwijderAdres(a);        // werkt

            // Test UpdateAdres
            //string? straat = "Guldensporenpark";
            //string? nummer = "24";
            //string? postcode = "9820";
            //string? plaats = "Merelbeke";
            //string? land = "Belgie";

            //adresRepo.UpdateAdres(5, straat, nummer, postcode, plaats, land); //werkt

        }
        private IAdresRepository ar;

        Adres a = new Adres("stationstraat", "36C", "9000", "Gent", "Belgie");


        //AdresManager am = new AdresManager(ar);



        
    } 
}




