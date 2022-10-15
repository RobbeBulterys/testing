using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Project2022.Domein;
using Application = System.Windows.Application;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DomeinController dc = new DomeinController();
            MainWindow window = new MainWindow(dc);
            window.Title = "Project2022";
            window.WindowState = WindowState.Maximized;
            window.Show();
        }
    }
}
