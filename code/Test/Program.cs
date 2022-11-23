using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk.repoADO;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using TriggerMe.VAT;
using System.Xml;
using BL_Projectwerk;

namespace Test {
    public class Program {


        static void Main(string[] args) {
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


            //string connectieString = "Server=tcp:bezoekerregistratiesysteem.database.windows.net,1433;Initial Catalog=bezoekerregistratiesysteemdb;Persist Security Info=False;User ID=Hackerman;Password=RootRoot!69;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            /*
           
            

            AddressManager adresManager = new AddressManager(new AddressRepoADO(connectieStringMysql), bedrijfRepo);
            

            Employee nicole = werknemerManager.GeefWerknemer(1);
            Employee hugo = werknemerManager.GeefWerknemer(2);
            Company vtm = bedrijfManager.GeefBedrijven()[0];
            Employeecontract nicoleAtVtm = new Employeecontract(vtm, nicole, "Zangeres");
            Employeecontract hugoAtVtm = new Employeecontract(vtm, hugo, "Zanger");
            //wcManager.VoegContractToe(nicoleAtVtm);
            //wcManager.VoegContractToe(hugoAtVtm);


            Company allphi = bedrijfManager.ZoekBedrijven(null, "Allphi", null, null)[0];
            Employee alien = new Employee("Carlier", "Alien");
            //werknemerManager.VoegWerknemerToe(alien);
            Employeecontract alienAtAllphi = new Employeecontract(allphi, alien, "Regional consultant coordinator");
            alienAtAllphi.ZetEmail("alien@allphi.be");
            //wcManager.VoegContractToe(alienAtAllphi);
            */

            /*
            string connectieStringMysql = "Server=ID367284_VRS.db.webhosting.be;User ID=ID367284_VRS;Password=RootRoot!69;Database=ID367284_VRS";
            EmployeeManager werknemerManager = new EmployeeManager(new EmployeeRepoADO(connectieStringMysql));
            EmployeecontractManager wcManager = new EmployeecontractManager(new EmployeecontractRepoADO(connectieStringMysql));
            CompanyRepoADO bedrijfRepo = new CompanyRepoADO(connectieStringMysql);
            CompanyManager bedrijfManager = new CompanyManager(bedrijfRepo, wcManager);
            VisitRepoADO _visitRepoAD0 = new VisitRepoADO(connectieStringMysql);
            VisitManager _visitManager = new VisitManager(_visitRepoAD0);


            Company company = bedrijfManager.GetCompanies()[0];
            Employee employee = werknemerManager.GetEmployee(1);
            Visitor _visitor = new Visitor("Jansenss", "Thomas", "thomas.jansenss@gmail.com", "jansenssNV");

            Visit _Visit = new Visit(2, _visitor, company, employee, DateTime.Now);

            _visitManager.UpdateVisit(_Visit);

            Console.WriteLine(employee.LastName);

            Console.WriteLine(_visitManager.GetVisits()[1].Visitor.FirstName);
            */

            ClassVatCheck classVatCheck = new ClassVatCheck();
            classVatCheck.TestVatNumber();
            Console.WriteLine(classVatCheck.Valid);
            Console.WriteLine(classVatCheck.Name);
            Console.WriteLine(classVatCheck.Address);
            Console.WriteLine(classVatCheck.Result);
            Console.ReadLine();















            // OUDE DB TESTS

            //WerknemerRepoADO _werknemerRepo = new WerknemerRepoADO(connectieString);
            //WerknemerManager Werkmanager = new WerknemerManager(_werknemerRepo);

            //Werkmanager.UpdateWerknemer(30, "Dink", "Marcel");

            //IEnumerable<Werknemer> gefilterdeWerknemers = Werkmanager.ZoekWerknemers("", "jansenss");

            //IEnumerable<Werknemer> AlleWerknemers = Werkmanager.GeefWerknemers();

            //foreach (Werknemer werknemer in AlleWerknemers)
            //{
            //    Console.WriteLine(werknemer.Naam);
            //}

            //foreach (Werknemer werknemer in gefilterdeWerknemers)
            //{
            //    Console.WriteLine(werknemer.Naam);
            //}

            //Werkmanager.VoegWerknemerToe(new Werknemer("Walter","Grootaers"));

            //Console.WriteLine(Werkmanager.BestaatWerknemer("Walter","Grootaers"));
            //Werknemer tom = new Werknemer("Vandewiele", "Tom");
            //Werkmanager.VoegWerknemerToe(tom);
            //Console.WriteLine(tom.PersoonId);

            //BezoekerRepoADO bezoekerRepo = new BezoekerRepoADO(connectieString);
            //BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepo);
            //Bezoeker NiueweBezoeker = new Bezoeker(3,"Romy", "Charmed", "RomyCharmed@gmail.com", "StephanFuckyou");
            //bezoekerManager.UpdateBezoeker(2, "De Baets", "Joshua", "Joshua2@gmail.com", "Nosferatu");




            // TESTS ADO -----------------------------------------------------------------------------------------------------------------------------------------------
            //string connectionString = "Data Source=FRENK\\SQLEXPRESS;Initial Catalog=\"projectwerk test\";Integrated Security=True";
            //CompanyRepoADO bedrijfrepo = new CompanyRepoADO(connectieString);
            //////AdresRepoADO adresRepo = new AdresRepoADO(connectionString);
            //BezoekRepoADO bezoekRepo = new BezoekRepoADO(connectieString);

            //Console.WriteLine("==== TEST BezoekRepoADO ==== ");
            //Bezoeker bezoeker = new Bezoeker(1, "Doe", "John", "John@Doe.be", "Allphi");
            //Bedrijf Allphi = new Bedrijf(1, "Allphi", "BE0123321123", "Allphi@info.be");
            //Werknemer werknemer = new Werknemer(1, "John", "Doe");

            //werknemer.ZetEmail("john@doe.be");
            //DateTime StartTijd = new DateTime(2022, 10, 02);
            //DateTime EindTijd = new DateTime(2022, 11, 02);
            //Bezoek bezoek = new Bezoek(bezoeker, Allphi, werknemer, StartTijd);

            //Console.WriteLine(bezoekRepo.BestaatBezoek(bezoek));
            //Console.WriteLine("==== TEST WerknemerRepoADO ==== ");
            ////WerknemerRepoADO repo = new WerknemerRepoADO();
            //Werknemer sarahMetId = new Werknemer(1, "Cools", "Sarah", "SAFETY");
            //Werknemer sarahZonderId = new Werknemer("Cools", "Sarah", "SAFETY");
            //Werknemer annaZonderId = new Werknemer("Engelke", "Anna", "Kaping");
            //Werknemer annaMetId = new Werknemer("Engelke", "Anna", "Kaping");
            //Console.WriteLine(TestWerknemerRepoADO.BestaatWerknemer(sarahMetId));
            //Console.WriteLine(TestWerknemerRepoADO.BestaatWerknemer(sarahZonderId));

            // Werknemer toevoegen
            //Werknemer luc = new Werknemer("Vervoort", "Luc", "Lector - coördinator");
            //TestWerknemerRepoADO.VoegWerknemerToe(luc);
            ////tests Adres========================================================================
            //// Test AddressExistsWithoutId-------------------------------------------------------
            ////Adres a = new Adres("Kerkstraat", "3", "9000", "Gent", "Belgie");
            //Console.WriteLine(adresRepo.AddressExistsWithoutId(a));             // werkt

            // Test BestaatAdresMetID------------------------------------------------------------
            //Adres a = new Adres(1,"Kerkstraat", "4", "9000", "Gent", "Belgie");
            //Console.WriteLine(adresRepo.BestaatAdresMetId(a));               // werkt

            // Test VoegAdresToe-----------------------------------------------------------------
            //Adres a = new Adres("Dorp", "3", "9810", "Nazareth", "Belgie");
            //Console.WriteLine(adresRepo.VoegAdresToe(a).ToString());         // werkt

            // Test GeefAdresMetId---------------------------------------------------------------
            //Adres a = new Adres("Dorp", "3", "9810", "Nazareth", "Belgie");
            //Console.WriteLine(adresRepo.GeefAdresMetId(a).Id);         // werkt

            // Test VerwijderAdres---------------------------------------------------------------
            //Adres a = new Adres(10,"Dorp", "3", "9810", "Nazareth", "Belgie");
            //adresRepo.VerwijderAdres(a);        // werkt

            // Test UpdateAdres------------------------------------------------------------------
            //string? straat = "Guldensporenpark";
            //string? nummer = "24";
            //string? postcode = "9820";
            //string? plaats = "Merelbeke";
            //string? land = "Belgie";

            //adresRepo.UpdateAdres(5, straat, nummer, postcode, plaats, land); //werkt
            ////tests Bedrijf=====================================================================
            // Test BestaatBedrijfZonderId--------------------------------------------------------
            //Bedrijf b = new Bedrijf("Allphi", "BE0123321124", "Allphi@gmail.com");
            //Console.WriteLine(bedrijfrepo.BestaatBedrijfZonderId(b));
            // Test BestaatBedrijfMetId-----------------------------------------------------------
            //int id = 5;
            //Console.WriteLine(bedrijfrepo.BestaatBedrijfMetId(id)); //werkt
            //Test VoegBedrijfToe-----------------------------------------------------------------
            //bedrijfrepo.VoegBedrijfToe("BE1234567890", "Advalvas", "Advalvas@fastmail.com",null,10); //werkt
            // Test VerwijderBedrijf--------------------------------------------------------------

            //WerknemerRepoADO _werknemerRepo = new WerknemerRepoADO(connectieString);
            //WerknemerManager Werkmanager = new WerknemerManager(_werknemerRepo);

            //Console.WriteLine(Werkmanager.BestaatWerknemer("Grootaers", "Walter"));

            //AdresRepoADO adresrepo = new AdresRepoADO(connectieString);
            //WerknemercontractRepoADO wcrepo = new WerknemercontractRepoADO(connectieString);


            //AdresManager AM = new AdresManager(adresrepo, bedrijfrepo);
            //WerknemercontractManager WCM = new WerknemercontractManager(wcrepo);
            //BedrijfManager BM = new BedrijfManager(bedrijfrepo, AM, WCM);
            //Bedrijf b = new Bedrijf(1, "Allphi", "BE0123321123", "Allphi@hotmail.com");
            //BM.VerwijderBedrijf(b, 2);
            //bedrijfrepo.VerwijderBedrijf(9); // werkt
            //Test UpdateBedrijf

            //bedrijfrepo.UpdateBedrijf(2, null, null, "Allphi@hotmail.com", "0987654321");
            //TEST BezoekerREPO
            //Adres a = new Adres(1, "Kompasplein", "19", "9000", "Gent", "België");

            //a.ZetStraat("     Statio   nstraat    ");
            //Console.WriteLine(a.ToString());



            // ===================================================================================
            // CONTRACTREPO
            // conectiestring staat hierboven
            // ===================================================================================

            //WerknemercontractManager wcManager = new WerknemercontractManager(new WerknemercontractRepoADO(connectieString));

            // ===============================
            // Data
            //
            // -------------------------------
            // Bedrijven
            //BedrijfManager bedrijfManager = new BedrijfManager(new CompanyRepoADO(connectieString));

            //IReadOnlyList<Bedrijf> bedrijven = bedrijfManager.GeefBedrijven();
            //Bedrijf allphidb = bedrijfManager.ZoekBedrijven(null, "allphi", null, null)[0];
            //Bedrijf allphi = new Bedrijf(10, "Allphi", "BE0123321123", "Allphi@hotmail.com");
            //Bedrijf bedrijf = bedrijfManager.ZoekBedrijven(null, "Cipal S.", null, null)[0];

            // Werknemers
            //WerknemerManager werknemerManager = new WerknemerManager(new WerknemerRepoADO(connectieString));
            //Werknemer walterdb = werknemerManager.ZoekWerknemers("Grootaers", "Walter")[0];
            //Werknemer gillesdb = werknemerManager.ZoekWerknemers("Blondeel", "Gilles")[0];
            //IReadOnlyList<Werknemer> werknemers = werknemerManager.GeefAlleWerknemers();

            //IReadOnlyList<Werknemercontract> contractenbedrijf = wcManager.GeefContractenVanBedrijf(allphidb);
            //foreach (var item in contractenbedrijf) {
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(wcManager.BestaatContract(1, 1) == true);
            //Console.WriteLine(wcManager.BestaatContract(25, 1) == true);
            //Console.WriteLine(wcManager.BestaatContract(25, 3) == true);
            //Console.WriteLine(wcManager.BestaatContract(77, 7) == false);

            //IReadOnlyList<Werknemercontract> contractenWalter = wcManager.GeefContractenVanWerknemer(walterdb);
            //foreach (var item in contractenWalter) {
            //    Console.WriteLine(item);
            //}

            //IReadOnlyList<Werknemercontract> contractenGilles = wcManager.GeefContractenVanWerknemer(gillesdb);
            //foreach (var item in contractenGilles) {
            //    Console.WriteLine(item);
            //}
            //Console.WriteLine("Geen contracten van gilles");


            // ===============================
            // Watch out : TOEVOEGEN - gelukt
            // ===============================
            //Werknemer gomez = werknemerManager.ZoekWerknemers("Gomez", null)[0];
            //Bedrijf tesla2 = bedrijfManager.ZoekBedrijven(null, "Tesla2.0", null, null)[0];
            //Werknemercontract contractTeslaGomez = new Werknemercontract(tesla2, gomez, "Architectuur");
            //contractTeslaGomez.ZetEmail("gomez.steven@tesla.elon");

            // Contract toevoegen geslaagd
            //wcManager.VoegContractToe(null);
            //Console.WriteLine("Contract Tesla Gomez toegevoegd");



            // ===============================
            // Watch out : VERWIJDEREN - gelukt
            // ===============================
            //Werknemer ted = werknemerManager.ZoekWerknemers(null, "Ted")[0];
            //Bedrijf advalvas = bedrijfManager.ZoekBedrijven(null, "Advalvas", null, null)[0];
            //Werknemercontract contractAdvalvasTed = new Werknemercontract(advalvas, ted, "UpdateTest");

            //wcManager.VerwijderContract(contractAdvalvasTed);
            //Console.WriteLine("Geslaagd");

            //wcManager.UpdateContract(ted, advalvas, "teddy beer", null); // OK - contract bestaat niet
            //Console.WriteLine(wcManager.GeefContractenVanBedrijf(tesla2)[0]);
            //wcManager.UpdateContract(gomez, tesla2, "geen architectuur meer", null);
            //Console.WriteLine(wcManager.GeefContractenVanBedrijf(tesla2)[0]);
            //wcManager.UpdateContract(gomez, tesla2, null, "gomez@tesla2.com");
            //Console.WriteLine(wcManager.GeefContractenVanBedrijf(tesla2)[0]);
            //wcManager.UpdateContract(gomez, tesla2, "Architectuur", "gomez.steven@tesla.elon");
            //Console.WriteLine(wcManager.GeefContractenVanBedrijf(tesla2)[0]);

            // moet errors opleveren en geven ook errors
            //wcManager.UpdateContract(gomez, tesla2, null, null);
            //wcManager.UpdateContract(new Werknemer("random", "naam"), advalvas, "moet error gooien", null);
            //wcManager.UpdateContract(gomez, advalvas, "moet exception gooien", null);


            //Console.WriteLine(Controle.IsGoedeEmailSyntax(".mike@email.com"));
            //Console.WriteLine(Controle.IsGoedeEmailSyntax(".mike@email.come"));
            //Console.WriteLine(Controle.IsGoedeEmailSyntax(".mike@email.come"));



            // TEST MySql
            //AdresRepoMySql adresRepositoryMySql = new AdresRepoMySql(connectieStringMysql);
            //Console.WriteLine(adresRepositoryMySql.BestaatAdresMetId(1));
            //Console.WriteLine(adresRepositoryMySql.BestaatAdresMetId(2));
            //Console.WriteLine(adresRepositoryMySql.BestaatAdresMetId(3));
        }
    }
}





