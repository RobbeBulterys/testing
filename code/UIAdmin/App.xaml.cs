using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk.repoADO;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UIAdmin.view;
using DL_Projectwerk.repoADO;

namespace UIAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
       {
            string connectieStringOLD = "Server=tcp:bezoekerregistratiesysteem.database.windows.net,1433;Initial Catalog=bezoekerregistratiesysteemdb;Persist Security Info=False;User ID=Hackerman;Password=RootRoot!69;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string connectieString = "Server=ID367284_VRS.db.webhosting.be;User ID=ID367284_VRS;Password=RootRoot!69;Database=ID367284_VRS";

            

            // Repo's aanmaken om te gebruiken
            IAddressRepository adresRepository = new AddressRepoADO(connectieString);
            ICompanyRepository bedrijfRepository = new CompanyRepoADO(connectieString);
            IVisitorRepository bezoekerRepository = new VisitorRepoADO(connectieString);
            IVisitRepository bezoekRepository = new VisitRepoADO(connectieString);
            IEmployeeRepository werknemerRepository = new EmployeeRepoADO(connectieString);
            IEmployeecontractRepository werknemercontractRepository = new EmployeecontractRepoADO(connectieString);
            AddressManager adresManager = new AddressManager(adresRepository, bedrijfRepository);
            VisitorManager bezoekerManager = new VisitorManager(bezoekerRepository);
            VisitManager bezoekManager = new VisitManager(bezoekRepository);
            EmployeeManager werknemerManager = new EmployeeManager(werknemerRepository);
            EmployeecontractManager werknemercontractManager = new EmployeecontractManager(werknemercontractRepository);
            CompanyManager bedrijfManager = new CompanyManager(bedrijfRepository, werknemercontractManager);

            // Oproepen van het venster
            try
            {
                MainScreen mainScreen = new MainScreen(adresManager, bedrijfManager, bezoekerManager, werknemerManager, werknemercontractManager, bezoekManager);
                mainScreen.Show();
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
        }
    }
}
