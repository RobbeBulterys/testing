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
    /// Interaction logic for DartsMainScreen.xaml
    /// </summary>
    public partial class DartsMainScreen : Window
    {
        DomeinController dc = new DomeinController();
        User user = new User();
        public DartsMainScreen(DomeinController _dc, User _user)
        {
            InitializeComponent();
            dc = _dc;
            user = _user;
            CloseSpelInstellingen();
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
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            else if (this.WindowState != WindowState.Maximized) this.WindowState = WindowState.Maximized;
        }
        #endregion
        #region Spelinstellingen
        private void ShowSpelInstellingen(string choice)
        {
            switch (choice)
            {
                case "wedstrijd":
                    CloseSpelInstellingen();
                    cmbWedstrijdBestFirstOf.Visibility = Visibility.Visible;
                    cmbWedstrijdIn.Visibility = Visibility.Visible;
                    cmbWedstrijdOneToTen.Visibility = Visibility.Visible;
                    cmbWedstrijdOut.Visibility = Visibility.Visible;
                    cmbWedstrijdSetsOrLegs.Visibility = Visibility.Visible;
                    cmbWedstrijdTotalPoints.Visibility = Visibility.Visible;
                    btnStartSpelWedstrijd.Visibility = Visibility.Visible;
                    lblWedstrijdExtraInfo.Content = "";
                    break;
                case "cricket":
                    CloseSpelInstellingen();
                    lblCricket.Visibility = Visibility.Visible;
                    btnStartSpelCricket.Visibility = Visibility.Visible;
                    break;
                case "tactics":
                    CloseSpelInstellingen();
                    lblTactics.Visibility = Visibility.Visible;
                    btnStartSpelTactics.Visibility = Visibility.Visible;
                    break;
                case "singletraining":
                    CloseSpelInstellingen();
                    btnStartSpelSingleTraining.Visibility = Visibility.Visible;
                    cmbSingleTrainingKeuze.Visibility = Visibility.Visible;
                    lblSingleTraining.Visibility = Visibility.Visible;
                    break;
                case "doubletraining":
                    CloseSpelInstellingen();
                    btnStartSpelDoublesTraining.Visibility = Visibility.Visible;
                    cmbDoubleTrainingKeuze.Visibility = Visibility.Visible;
                    lblDoubleTraining.Visibility = Visibility.Visible;
                    break;
                case "bob's27":
                    CloseSpelInstellingen();
                    btnStartSpelBobs27.Visibility = Visibility.Visible;
                    cmbBobs27.Visibility = Visibility.Visible;
                    lblBobs27.Visibility = Visibility.Visible;
                    break;
                case "scoretraining":
                    CloseSpelInstellingen();
                    btnStartSpelScoreTraining.Visibility = Visibility.Visible;
                    lblScoreTraining.Visibility = Visibility.Visible;
                    cmbScoreTrainingNumbers.Visibility = Visibility.Visible;
                    lblScoreTraining2.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void CloseSpelInstellingen()
        {
            cmbWedstrijdBestFirstOf.Visibility = Visibility.Hidden;
            cmbWedstrijdIn.Visibility = Visibility.Hidden;
            cmbWedstrijdOneToTen.Visibility = Visibility.Hidden;
            cmbWedstrijdOut.Visibility = Visibility.Hidden;
            cmbWedstrijdSetsOrLegs.Visibility = Visibility.Hidden;
            cmbWedstrijdTotalPoints.Visibility = Visibility.Hidden;
            btnStartSpelWedstrijd.Visibility = Visibility.Hidden;
            lblWedstrijdExtraInfo.Content = "";

            lblCricket.Visibility = Visibility.Hidden;
            btnStartSpelCricket.Visibility = Visibility.Hidden;

            lblTactics.Visibility = Visibility.Hidden;
            btnStartSpelTactics.Visibility = Visibility.Hidden;

            btnStartSpelSingleTraining.Visibility = Visibility.Hidden;
            cmbSingleTrainingKeuze.Visibility = Visibility.Hidden;
            lblSingleTraining.Visibility = Visibility.Hidden;

            btnStartSpelDoublesTraining.Visibility = Visibility.Hidden;
            cmbDoubleTrainingKeuze.Visibility = Visibility.Hidden;
            lblDoubleTraining.Visibility = Visibility.Hidden;

            btnStartSpelBobs27.Visibility = Visibility.Hidden;
            cmbBobs27.Visibility = Visibility.Hidden;
            lblBobs27.Visibility = Visibility.Hidden;

            btnStartSpelScoreTraining.Visibility = Visibility.Hidden;
            lblScoreTraining.Visibility = Visibility.Hidden;
            cmbScoreTrainingNumbers.Visibility = Visibility.Hidden;
            lblScoreTraining2.Visibility = Visibility.Hidden;
        }
        #endregion
        #region buttons start spel
        private void btnWedstrijd_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("wedstrijd");
        }
        private void btnCricket_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("cricket");
        }
        private void btnTactics_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("tactics");
        }
        private void btnSingleTraining_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("singletraining");
        }
        private void btnDoublesTraining_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("doubletraining");
        }
        private void btnBobs27_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("bob's27");
        }
        private void btnScoreTraining_Click(object sender, RoutedEventArgs e)
        {
            ShowSpelInstellingen("scoretraining");
        }
        private void cmbWedstrijdTotalPoints_MouseEnter(object sender, MouseEventArgs e)
        {
            lblWedstrijdExtraInfo.Content = "Totale punten per set";
        }
        private void cmbWedstrijdOneToTen_MouseEnter(object sender, MouseEventArgs e)
        {
            lblWedstrijdExtraInfo.Content = "Totale aantal sets / legs";
        }
        private void cmbWedstrijdIn_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbWedstrijdIn.SelectedIndex == 0) lblWedstrijdExtraInfo.Content = "Begin van een set naar keuze";
            if (cmbWedstrijdIn.SelectedIndex == 1) lblWedstrijdExtraInfo.Content = "Begin van een set met een double";
            if (cmbWedstrijdIn.SelectedIndex == 2) lblWedstrijdExtraInfo.Content = "Begin van een set met een bull";
        }
        private void cmbWedstrijdOut_MouseEnter(object sender, MouseEventArgs e)
        {
            if (cmbWedstrijdOut.SelectedIndex == 2) lblWedstrijdExtraInfo.Content = "Einde van een set naar keuze";
            if (cmbWedstrijdOut.SelectedIndex == 0) lblWedstrijdExtraInfo.Content = "Einde van een set met een double";
            if (cmbWedstrijdOut.SelectedIndex == 1) lblWedstrijdExtraInfo.Content = "Einde van een set met een bull";
        }
        #endregion

        private void btnStartSpelScoreTraining_Click(object sender, RoutedEventArgs e)
        {
            int selectedNumber = int.Parse(cmbScoreTrainingNumbers.Text);
            DartsScoreTraining window = new DartsScoreTraining(dc, user, selectedNumber);
            window.Title = user.Username;
            window.WindowState = WindowState.Maximized;
            window.Show();
            this.Close();
        }
    }
}
