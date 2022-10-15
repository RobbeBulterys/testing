using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project2022.Domein;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        DomeinController dc = new DomeinController();
        User user = new User();
        public MainScreen(DomeinController _dc, User _user)
        {
            InitializeComponent();
            ButtonComponentsPropertiesChange(200, 50);
            LblUsernameDisplay.Content = _user.Username;
            dc = _dc;
            user = _user;
        }
        private void ButtonComponentsPropertiesChange(int height, int fontSize)
        {
            BtnDartsSpel.Height = height;
            BtnDartsSpel.Width = height;
            BtnDartsSpel.FontSize = fontSize;
            BtnDartsStatistieken.Height = height;
            BtnDartsStatistieken.Width = height;
            BtnDartsStatistieken.FontSize = fontSize / 1.4;
        }
        #region taskbar buttons
        private void btnHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(dc);
            window.Title = "Project2022";
            window.WindowState = WindowState.Maximized;
            window.Show();
            this.Close();
        }
        private void btnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                ButtonComponentsPropertiesChange(100, 25);
            }
            else if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                ButtonComponentsPropertiesChange(200, 50);
            }
        }
        #endregion

        private void BtnDartsSpel_Click(object sender, RoutedEventArgs e)
        {
            DartsMainScreen window = new DartsMainScreen(dc, user);
            window.Title = user.Username;
            window.WindowState = WindowState.Maximized;
            window.Show();
            this.Close();
        }

        private void BtnDartsStatistieken_Click(object sender, RoutedEventArgs e)
        {
            StatistiekenMainScreen window = new StatistiekenMainScreen(dc, user);
            window.Title = user.Username;
            window.WindowState = WindowState.Maximized;
            window.Show();
            this.Close();
        }
    }
}
