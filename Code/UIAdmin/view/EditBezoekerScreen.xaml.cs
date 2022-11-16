using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
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
using UIAdmin.Domein;

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for EditBezoekerScreen.xaml
    /// </summary>
    public partial class EditBezoekerScreen : Window
    {
        private bool _isMaximized = false;
        private BezoekerManager _bezoekerManager;
        private BezoekManager _bezoekManager;
        private Bezoeker _bezoeker;
        private ObservableCollection<BezoekAdmin> contracts = new ObservableCollection<BezoekAdmin>();
        public EditBezoekerScreen(BezoekerManager bezoekerManager, BezoekManager bezoekManager, Bezoeker bezoeker)
        {
            InitializeComponent();
            _bezoekerManager = bezoekerManager;
            _bezoekManager = bezoekManager;
            _bezoeker = bezoeker;
            BezoekenDataGrid.ItemsSource = contracts;
            InitializeBezoeker(bezoeker);
        }
        private void InitializeBezoeker(Bezoeker bezoeker)
        {
            TextBoxBezoekerNaam.Text = bezoeker.Naam;
            TextBoxVoorNaam.Text = bezoeker.Voornaam;
            TextBoxEmail.Text = bezoeker.Email;
            TextBoxBedrijfNaam.Text = bezoeker.Bedrijf;
            BezoekerIdAanpassen.Text = $"{bezoeker.Naam} {bezoeker.Voornaam}";
            TextBlockIdBezoeker.Text = $"Id: {bezoeker.PersoonId}";
            contracts.Clear();
            List<Bezoek> b = _bezoekManager.ZoekBezoeken(_bezoeker, null, null, null).ToList();
            foreach (Bezoek bezoek in b)
            {
                contracts.Add(new BezoekAdmin(bezoek));
            }
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
                    if ((!string.IsNullOrWhiteSpace(TextBoxBezoekerNaam.Text)) && (TextBoxBezoekerNaam.Text != _bezoeker.Naam)) { naam = TextBoxBezoekerNaam.Text; message += $"naam => {naam}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) && (TextBoxVoorNaam.Text != _bezoeker.Voornaam)) { voornaam = TextBoxVoorNaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) && (TextBoxEmail.Text != _bezoeker.Email)) { email = TextBoxEmail.Text; message += $"email => {email}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxBedrijfNaam.Text)) && (TextBoxBedrijfNaam.Text != _bezoeker.Bedrijf)) { bedrijfnaam = TextBoxBedrijfNaam.Text; message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else
                    {
                        _bezoekerManager.UpdateBezoeker(_bezoeker.PersoonId, naam, voornaam, email, bedrijfnaam);
                    }
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveBtn.IsEnabled = true;
            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#623ed0");
            BorderNaam.BorderBrush = colorBrush;
            TBNaam.Text = "";
            TBNaam.Foreground = Brushes.Black;
            BorderVoornaam.BorderBrush = colorBrush;
            TBVoornaam.Text = "";
            TBVoornaam.Foreground = Brushes.Black;
            BorderEmail.BorderBrush = colorBrush;
            TBEmail.Text = "";
            TBEmail.Foreground = Brushes.Black;
            BorderBedrijfNaam.BorderBrush = colorBrush;
            TBBedrijfNaam.Text = "";
            TBBedrijfNaam.Foreground = Brushes.Black;
            string? naam = null;
            string? voornaam = null;
            string? email = null;
            string? bedrijfNaam = null;
            if (!string.IsNullOrWhiteSpace(TextBoxBezoekerNaam.Text)) { naam = TextBoxBezoekerNaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) { voornaam = TextBoxVoorNaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { email = TextBoxEmail.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxBedrijfNaam.Text)) { bedrijfNaam = TextBoxBedrijfNaam.Text; }
            if (naam == null)
            {
                BorderNaam.BorderBrush = Brushes.Red;
                TBNaam.Text = "Naam: mag niet leeg zijn!";
                TBNaam.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (voornaam == null)
            {
                BorderVoornaam.BorderBrush = Brushes.Red;
                TBVoornaam.Text = "Voornaam: mag niet leeg zijn!";
                TBVoornaam.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (email == null)
            {
                BorderEmail.BorderBrush = Brushes.Red;
                TBEmail.Text = "Email: mag niet leeg zijn!";
                TBEmail.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (bedrijfNaam == null)
            {
                BorderBedrijfNaam.BorderBrush = Brushes.Red;
                TBBedrijfNaam.Text = "Bedrijfsnaam: mag niet leeg zijn!";
                TBBedrijfNaam.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (email != null)
            {
                try
                {
                    if (Controle.IsGoedeEmailSyntax(email)) { }
                }
                catch (Exception ex)
                {
                    SaveBtn.IsEnabled = false;
                    if (ex.Message == "Controle - IsGoedeEmailSyntax - ongeldige email")
                    {
                        BorderEmail.BorderBrush = Brushes.Red;
                        TBEmail.Text += "ongeldige syntax!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
        }
    }
}
