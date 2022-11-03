using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UIAdmin.view;

namespace UIAdmin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
       {
            string connectieString = "Server=tcp:bezoekerregistratiesysteem.database.windows.net,1433;Initial Catalog=bezoekerregistratiesysteemdb;Persist Security Info=False;User ID=Hackerman;Password=RootRoot!69;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            IAdresRepository adresRepository = new AdresRepoADO(connectieString);
            IBedrijfRepository bedrijfRepository = new BedrijfRepoADO(connectieString);
            IBezoekerRepository bezoekerRepository = new BezoekerRepoADO(connectieString);
            IBezoekRepository bezoekRepository = new BezoekRepoADO(connectieString);
            IWerknemerRepository werknemerRepository = new WerknemerRepoADO(connectieString);
            IWerknemercontractRepository werknemercontractRepository = new WerknemercontractRepoADO(connectieString);
            AdresManager adresManager = new AdresManager(adresRepository, bedrijfRepository);
            BezoekerManager bezoekerManager = new BezoekerManager(bezoekerRepository);
            BezoekManager bezoekManager = new BezoekManager(bezoekRepository);
            WerknemerManager werknemerManager = new WerknemerManager(werknemerRepository);
            WerknemercontractManager werknemercontractManager = new WerknemercontractManager(werknemercontractRepository);
            BedrijfManager bedrijfManager = new BedrijfManager(bedrijfRepository, adresManager, werknemercontractManager);
            MainScreen mainScreen = new MainScreen(adresManager, bedrijfManager, bezoekerManager, werknemerManager, werknemercontractManager, bezoekManager);
            mainScreen.Show();
        }
    }
}
