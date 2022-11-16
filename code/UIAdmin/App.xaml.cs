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

            // Oproepen van het venster
            MainScreen mainScreen = new MainScreen(adresManager, bedrijfManager, bezoekerManager, werknemerManager, werknemercontractManager, bezoekManager);
            mainScreen.Show();
        }
    }
}
