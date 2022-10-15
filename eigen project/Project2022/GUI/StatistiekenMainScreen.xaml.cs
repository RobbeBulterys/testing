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
    /// Interaction logic for StatistiekenMainScreen.xaml
    /// </summary>
    public partial class StatistiekenMainScreen : Window
    {
        private DomeinController dc = new DomeinController();
        private User user = new User();
        private List<DartsScoreTrainingClass> dartsScores;
        public StatistiekenMainScreen(DomeinController _dc, User _user)
        {
            InitializeComponent();
            dc = _dc;
            user = _user;
            dartsScores = user.DartScoreTrainingScores;
            lstvDartsScoreTraining.ItemsSource = dartsScores;
            lblUsername.Content = user.Username;
            Statistieken();
        }
        private void Statistieken()
        {
            if (lstvDartsScoreTraining is not null)
            {
                StatistiekenChoiceChange();
                if (cmbStatistieken.SelectedIndex == 6) lstvDartsScoreTraining.Visibility = Visibility.Visible;
            }
        }
        private void StatistiekenChoiceChange()
        {
            lstvDartsScoreTraining.Visibility = Visibility.Hidden;
        }
        #region taskbar buttons
        private void btnHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainScreen window = new MainScreen(dc, user);
            window.Title = user.Username;
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
            }
            else if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        #endregion

        private void cmbStatistieken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Statistieken();
        }
    }
}
