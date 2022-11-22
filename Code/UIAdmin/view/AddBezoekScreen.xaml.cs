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
        private VisitorManager _bezoekerManager;
        private CompanyManager _bedrijfManager;
        private VisitManager _bezoekManager;
        private EmployeecontractManager _werknemercontractManager;
        private List<Company> bedrijven = new List<Company>();
        private List<Employee> werknemersList = new List<Employee>();
        private ObservableCollection<string> BedrijfNamen = new ObservableCollection<string>();
        private ObservableCollection<string> WerknemersNamen = new ObservableCollection<string>();
        public AddBezoekScreen(VisitorManager bezoekerManager, CompanyManager bedrijfManager, EmployeecontractManager werknemercontract, VisitManager bezoekManager)
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
            bedrijven = _bedrijfManager.GetCompanies().ToList();
            bedrijven.ForEach(b => BedrijfNamen.Add(b.Name));
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
                        Visitor bezoeker = _bezoekerManager.SearchVisitors(naam, voornaam, email, null).ToList()[0];
                        Company bedrijfSelected = null;
                        Employee werknemerSelected = null;
                        foreach (Company bedrijf in bedrijven) { if (bedrijf.Name == bedrijfnaam) { bedrijfSelected = bedrijf; break; } }
                        foreach (Employee werknemer in werknemersList) { if ($"{werknemer.LastName}, {werknemer.FirstName}" == contactPersoon) { werknemerSelected = werknemer; break; } }
                        Visit b = new Visit(bezoeker, bedrijfSelected, werknemerSelected, DateTime.Now);
                        try
                        {
                            _bezoekManager.AddVisit(b);
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
            List<Employeecontract> werker = new List<Employeecontract>();
            foreach (Company bedrijf in bedrijven)
            {
                if (bedrijf.Name == ComboBoxBedrijf.SelectedValue.ToString())
                {
                    werker.AddRange(_werknemercontractManager.GetCompanyContracts(bedrijf));
                }
            }
            werker.ForEach(w => werknemersList.Add(w.Employee));
            werknemersList.ForEach(c => WerknemersNamen.Add($"{c.LastName}, {c.FirstName}"));
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
                    if (Verify.IsValidEmailSyntax(email)) { }
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
