using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditWerknemerScreen.xaml
    /// </summary>
    public partial class EditWerknemerScreen : Window
    {
        private bool _isMaximized = false;
        private Werknemer _werknemer;
        private WerknemerManager _werknemerManager;
        private BezoekManager _bezoekManager;
        private WerknemercontractManager _werknemercontractManager;
        private ObservableCollection<Werknemercontract> werknemercontracts = new ObservableCollection<Werknemercontract>();
        private ObservableCollection<BezoekAdmin> contracts = new ObservableCollection<BezoekAdmin>();
        public EditWerknemerScreen(WerknemerManager werknemerManager, WerknemercontractManager werknemercontractManager, BezoekManager bezoekManager, Werknemer werknemer, string screen)
        {
            InitializeComponent();
            _werknemerManager = werknemerManager;
            _bezoekManager = bezoekManager;
            _werknemercontractManager = werknemercontractManager;
            _werknemer = werknemer;
            WerknemerContractDataGrid.ItemsSource = werknemercontracts;
            BezoekenDataGrid.ItemsSource = contracts;
            InitializeWerknemer(werknemer, screen);
        }
        private void InitializeWerknemer(Werknemer werknemer, string screen)
        {
            if (screen == "Worker") { WerknemerBtn.IsChecked = true; }
            if (screen == "Visits") { BezoekenBtn.IsChecked = true; }
            TextBoxNaam.Text = werknemer.Naam;
            TextBoxVoorNaam.Text = werknemer.Voornaam;
            WerknemerIdAanpassen.Text = $"{werknemer.Naam} {werknemer.Voornaam}";
            WerknemerIdAanpassenBezoeken.Text = $"{werknemer.Naam} {werknemer.Voornaam}";
            TextBlockIdWerknemer.Text = $"Id: {werknemer.PersoonId}";
            WerknemerBtn.Content = $"{werknemer.Naam}";
            werknemercontracts.Clear();
            List<Werknemercontract> werknemercontracten = new List<Werknemercontract>();
            werknemercontracten.AddRange(_werknemercontractManager.GeefContractenVanWerknemer(werknemer).ToList());
            werknemercontracten.ForEach(a => werknemercontracts.Add(a));
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
                    string message = "";
                    if ((!string.IsNullOrWhiteSpace(TextBoxNaam.Text)) && (TextBoxNaam.Text != _werknemer.Naam)) { naam = TextBoxNaam.Text; message += $"naam => {naam}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) && (TextBoxVoorNaam.Text != _werknemer.Voornaam)) { voornaam = TextBoxVoorNaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else
                    {
                        try
                        {
                            _werknemerManager.UpdateWerknemer(_werknemer.PersoonId, naam, voornaam);
                            MessageBox.Show($"succes!");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"error!, {ex}");
                        }
                    }
                }
            }
        }
        private void WerknemerBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BezoekenBorder.Visibility = Visibility.Collapsed;
                BedrijfBorder.Visibility = Visibility.Visible;
            }
        }
        private void BezoekenBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BezoekenBorder.Visibility = Visibility.Visible;
                BedrijfBorder.Visibility = Visibility.Collapsed;
                contracts.Clear();
                List<Bezoek> b = _bezoekManager.ZoekBezoeken(null, null, _werknemer, null).ToList();
                foreach (Bezoek bezoek in b)
                {
                    contracts.Add(new BezoekAdmin(bezoek));
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
            string? naam = null;
            string? voornaam = null;
            if (!string.IsNullOrWhiteSpace(TextBoxNaam.Text)) { naam = TextBoxNaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) { voornaam = TextBoxVoorNaam.Text; }
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
        }
    }
}
