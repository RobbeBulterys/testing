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
        private ObservableCollection<ContractVoorLijst> contracts = new ObservableCollection<ContractVoorLijst>();
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
                contracts.Add(new ContractVoorLijst(bezoek));
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
    }
}
