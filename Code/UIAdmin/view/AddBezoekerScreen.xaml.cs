using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
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

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for AddBezoekerScreen.xaml
    /// </summary>
    public partial class AddBezoekerScreen : Window
    {
        private bool _isMaximized = false;
        private BezoekerManager _bezoekerManager;
        public AddBezoekerScreen(BezoekerManager bezoekerManager)
        {
            InitializeComponent();
            _bezoekerManager = bezoekerManager;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (_isMaximized) { this.WindowState = WindowState.Normal; this.Height = 720; this.Width = 1080; _isMaximized = false; }
                else { this.WindowState = WindowState.Maximized; _isMaximized = true; }
            }
        }
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "SaveBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? email = null;
                    string? bedrijfnaam = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(TextBoxBezoekerNaam.Text)) { naam = TextBoxBezoekerNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) { voornaam = TextBoxVoorNaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { email = TextBoxEmail.Text; message += $"email => {email}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxBedrijfNaam.Text)) { bedrijfnaam = TextBoxBedrijfNaam.Text; message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                    if (naam == null || voornaam == null || email == null || bedrijfnaam == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else
                    {
                        _bezoekerManager.VoegBezoekerToe(new Bezoeker(naam, voornaam, email, bedrijfnaam));
                        this.Close();
                    }
                }
            }
        }
    }
}
