using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using DL_Projectwerk;
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

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for AddBezoekScreen.xaml
    /// </summary>
    public partial class AddBezoekScreen : Window
    {
        private bool _isMaximized = false;
        private BezoekerManager _bezoekerManager;
        private BedrijfManager _bedrijfManager;
        private BezoekManager _bezoekManager;
        private WerknemercontractManager _werknemercontractManager;
        private List<Bedrijf> bedrijven = new List<Bedrijf>();
        private List<Werknemer> werknemersList = new List<Werknemer>();
        private ObservableCollection<string> BedrijfNamen = new ObservableCollection<string>();
        private ObservableCollection<string> WerknemersNamen = new ObservableCollection<string>();
        public AddBezoekScreen(BezoekerManager bezoekerManager, BedrijfManager bedrijfManager, WerknemercontractManager werknemercontract, BezoekManager bezoekManager)
        {
            InitializeComponent();
            _bezoekerManager = bezoekerManager;
            _bedrijfManager = bedrijfManager;
            _bezoekManager = bezoekManager;
            _werknemercontractManager = werknemercontract;
            InitializeLists();
        }
        private void InitializeLists()
        {
            ComboBoxBedrijf.ItemsSource = BedrijfNamen;
            ComboBoxWerknemer.ItemsSource = WerknemersNamen;
            bedrijven.Clear();
            BedrijfNamen.Clear();
            bedrijven = _bedrijfManager.GeefBedrijven().ToList();
            bedrijven.ForEach(b => BedrijfNamen.Add(b.Naam));
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
                    string? contactPersoon = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(TextBoxBezoekerNaam.Text)) { naam = TextBoxBezoekerNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxBezoekerVoornaam.Text)) { voornaam = TextBoxBezoekerVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxBezoekerEmail.Text)) { email = TextBoxBezoekerEmail.Text; message += $"email => {email}\n"; }
                    if (ComboBoxBedrijf.SelectedItem != null) { bedrijfnaam = ComboBoxBedrijf.SelectedItem.ToString(); message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                    if (ComboBoxWerknemer.SelectedItem != null) { contactPersoon = ComboBoxWerknemer.SelectedItem.ToString(); message += $"contactPersoon => {contactPersoon}\n"; }
                    if (naam == null || voornaam == null || email == null || bedrijfnaam == null || contactPersoon == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else
                    {
                        Bezoeker bezoeker = _bezoekerManager.ZoekBezoekers(naam, voornaam, email, null).ToList()[0];
                        Bedrijf bedrijfSelected = null;
                        Werknemer werknemerSelected = null;
                        foreach (Bedrijf bedrijf in bedrijven) { if (bedrijf.Naam == bedrijfnaam) { bedrijfSelected = bedrijf; break; } }
                        foreach (Werknemer werknemer in werknemersList) { if ($"{werknemer.Naam}, {werknemer.Voornaam}" == contactPersoon) { werknemerSelected = werknemer; break; } }
                        Bezoek b = new Bezoek(bezoeker, bedrijfSelected, werknemerSelected, DateTime.Now);
                        try
                        {
                            _bezoekManager.VoegBezoekToe(b);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"error! {ex}");
                        }
                    }
                }
            }
        }
        private void ComboBoxBedrijf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            werknemersList.Clear();
            WerknemersNamen.Clear();
            List<Werknemercontract> werker = new List<Werknemercontract>();
            foreach (Bedrijf bedrijf in bedrijven)
            {
                if (bedrijf.Naam == ComboBoxBedrijf.SelectedValue.ToString())
                {
                    werker.AddRange(_werknemercontractManager.GeefContractenVanBedrijf(bedrijf));
                }
            }
            werker.ForEach(w => werknemersList.Add(w.Werknemer));
            werknemersList.ForEach(c => WerknemersNamen.Add($"{c.Naam}, {c.Voornaam}"));
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
            string? naam = null;
            string? voornaam = null;
            string? email = null;
            if (!string.IsNullOrWhiteSpace(TextBoxBezoekerNaam.Text)) { naam = TextBoxBezoekerNaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxBezoekerVoornaam.Text)) { voornaam = TextBoxBezoekerVoornaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxBezoekerEmail.Text)) { email = TextBoxBezoekerEmail.Text; }
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
                        TBEmail.Text += "ongeldige syntacs!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
        }
    }
}
