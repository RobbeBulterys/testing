using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
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
    /// Interaction logic for EditBedrijfScreen.xaml
    /// </summary>
    public partial class EditBedrijfScreen : Window
    {
        private bool _isMaximized = false;
        private Company _company;
        private AddressManager _adresManager;
        private CompanyManager _companyManager;
        private VisitManager _visitManager;
        private EmployeeManager _employeeManager;
        private EmployeecontractManager _employeecontractManager;
        private ObservableCollection<Employeecontract> employeecontractsCollection = new ObservableCollection<Employeecontract>();
        private ObservableCollection<VisitAdmin> visitsAdminCollection = new ObservableCollection<VisitAdmin>();
        public EditBedrijfScreen(AddressManager adresManager, CompanyManager companyManager, EmployeecontractManager employeecontract, VisitManager visitManager, EmployeeManager employeeManager, Company company, string screen)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _companyManager = companyManager;
            _employeecontractManager = employeecontract;
            _visitManager = visitManager;
            _employeeManager = employeeManager;
            _company = company;
            BedrijfContractDataGrid.ItemsSource = employeecontractsCollection;
            BezoekenDataGrid.ItemsSource = visitsAdminCollection;
            InitializeCompany(company, screen);
        }
        private void InitializeCompany(Company company, string screen)
        {
            if (screen == "Company") { CompanyBtn.IsChecked = true; }
            if (screen == "Workers") { EmployeelijstBtn.IsChecked = true; }
            if (screen == "Visits") { VisitLijstBtn.IsChecked = true; }
            TextBoxCompanyName.Text = company.Name;
            TextBoxBTWnummer.Text = company.VATNumber;
            TextBoxPhoneNumber.Text = company.PhoneNumber;
            TextBoxEmail.Text = company.Email;
            if (company.Address != null)
            {
                TextBoxCountry.Text = company.Address.Country;
                TextBoxStreetName.Text = company.Address.Street;
                TextBoxNumber.Text = company.Address.Number;
                TextBoxPostalCode.Text = company.Address.Postalcode;
                TextBoxPlace.Text = company.Address.Place;
            }
            ShowCompanyId.Text = $"{company.Name}";
            TextBlockIdCompany.Text = $"Id: {company.Id}";
            CompanyEmployees.Text = $"{company.Name}";
            CompanyVisits.Text = $"{company.Name}";
            CompanyBtn.Content = $"{company.Name}";
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
                    string? bedrijfNaam = null;
                    string? bedrijfBTW = null;
                    string? bedrijfTelefoon = null;
                    string? bedrijfEmail = null;
                    string? land = null;
                    string? straat = null;
                    string? nummer = null;
                    string? postcode = null;
                    string? plaats = null;
                    string message = "";
                    if (TextBoxCompanyName.Text != _company.Name) { bedrijfNaam = TextBoxCompanyName.Text; message += $"bedrijfNaam => {bedrijfNaam}\n"; }
                    if (TextBoxBTWnummer.Text != _company.VATNumber) { bedrijfBTW = TextBoxBTWnummer.Text; message += $"bedrijfBTW => {bedrijfBTW}\n"; }
                    if (TextBoxPhoneNumber.Text != _company.PhoneNumber) { bedrijfTelefoon = TextBoxPhoneNumber.Text; message += $"bedrijfTelefoon => {bedrijfTelefoon}\n"; }
                    if (TextBoxEmail.Text != _company.Email) { bedrijfEmail = TextBoxEmail.Text; message += $"bedrijfEmail => {bedrijfEmail}\n"; }
                    if (_company.Address == null)
                    {
                        if (TextBoxCountry.Text != "") { land = TextBoxCountry.Text; message += $"land => {land}\n"; }
                        if (TextBoxStreetName.Text != "") { straat = TextBoxStreetName.Text; message += $"straat => {straat}\n"; }
                        if (TextBoxNumber.Text != "") { nummer = TextBoxNumber.Text; message += $"nummer => {nummer}\n"; }
                        if (TextBoxPostalCode.Text != "") { postcode = TextBoxPostalCode.Text; message += $"postcode => {postcode}\n"; }
                        if (TextBoxPlace.Text != "") { plaats = TextBoxPlace.Text; message += $"plaats => {plaats}\n"; }
                    }
                    else
                    {
                        if (TextBoxCountry.Text != _company.Address.Country) { land = TextBoxCountry.Text; message += $"land => {land}\n"; }
                        if (TextBoxStreetName.Text != _company.Address.Street) { straat = TextBoxStreetName.Text; message += $"straat => {straat}\n"; }
                        if (TextBoxNumber.Text != _company.Address.Number) { nummer = TextBoxNumber.Text; message += $"nummer => {nummer}\n"; }
                        if (TextBoxPostalCode.Text != _company.Address.Postalcode) { postcode = TextBoxPostalCode.Text; message += $"postcode => {postcode}\n"; }
                        if (TextBoxPlace.Text != _company.Address.Place) { plaats = TextBoxPlace.Text; message += $"plaats => {plaats}\n"; }
                    }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else if (!string.IsNullOrWhiteSpace(land) || !string.IsNullOrWhiteSpace(straat) || !string.IsNullOrWhiteSpace(nummer) || !string.IsNullOrWhiteSpace(postcode) || !string.IsNullOrWhiteSpace(plaats))
                    {
                        if ((land == null || straat == null || nummer == null || postcode == null || plaats == null) && _company.Address == null) MessageBox.Show("Alle velden voor het adres moeten worden ingevuld!");
                        else
                        {
                            try
                            {
                                if (_company.Address == null)
                                {
                                    int id = _adresManager.AddAddress(new Address(straat, nummer, postcode, plaats, land));
                                    _companyManager.UpdateCompanyAddress(_company.Id, id);
                                }
                                else
                                {
                                    _adresManager.UpdateAddress(_company.Address.Id, straat, nummer, postcode, plaats, land);
                                }
                                this.Close();
                                MessageBox.Show($"succes! adres");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"error! {ex.Message}");
                            }
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(bedrijfNaam) || !string.IsNullOrWhiteSpace(bedrijfBTW) || !string.IsNullOrWhiteSpace(bedrijfTelefoon) || !string.IsNullOrWhiteSpace(bedrijfEmail))
                    {
                        try
                        {
                            _companyManager.UpdateCompany(_company.Id, bedrijfBTW, bedrijfNaam, bedrijfEmail, bedrijfTelefoon);
                            MessageBox.Show($"succes! bedrijf");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"error! {ex.Message}");
                        }
                    }
                }
            }
        }
        private void MenuButtonsBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                CompanyBorder.Visibility = Visibility.Collapsed;
                EmployeeBorder.Visibility = Visibility.Collapsed;
                VisitBorder.Visibility = Visibility.Collapsed;
                RadioButton button = (RadioButton)sender;
                if (button.Name == "CompanyBtn")
                {
                    CompanyBorder.Visibility = Visibility.Visible;
                }
                if (button.Name == "EmployeelijstBtn")
                {
                    EmployeeBorder.Visibility = Visibility.Visible;
                    employeecontractsCollection.Clear();
                    List<Employeecontract> w = _employeecontractManager.GetCompanyContracts(_company).ToList();
                    w.ForEach(c => employeecontractsCollection.Add(c));
                }
                if (button.Name == "VisitLijstBtn")
                {
                    VisitBorder.Visibility = Visibility.Visible;
                    visitsAdminCollection.Clear();
                    List<Visit> b = _visitManager.SearchVisits(null, _company, null, null).ToList();
                    foreach (Visit bezoek in b)
                    {
                        visitsAdminCollection.Add(new VisitAdmin(bezoek));
                    }
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? bedrijfNaam = null;
            string? bedrijfBTW = null;
            string? bedrijfTelefoon = null;
            string? bedrijfEmail = null;
            string? land = null;
            string? straat = null;
            string? nummer = null;
            string? postcode = null;
            string? plaats = null;
            if (!string.IsNullOrWhiteSpace(TextBoxCompanyName.Text)) { bedrijfNaam = TextBoxCompanyName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxBTWnummer.Text)) { bedrijfBTW = TextBoxBTWnummer.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text)) { bedrijfTelefoon = TextBoxPhoneNumber.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { bedrijfEmail = TextBoxEmail.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxCountry.Text)) { land = TextBoxCountry.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxStreetName.Text)) { straat = TextBoxStreetName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxNumber.Text)) { nummer = TextBoxNumber.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPostalCode.Text)) { postcode = TextBoxPostalCode.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPlace.Text)) { plaats = TextBoxPlace.Text; }
            if (CheckingIsEverythingCorrect(bedrijfNaam, bedrijfBTW, bedrijfEmail, land, straat, nummer, postcode, plaats))
            {
                SaveBtn.IsEnabled = true;
            }
            else SaveBtn.IsEnabled = false;
        }
        private bool CheckingIsEverythingCorrect(string? bedrijfNaam, string? bedrijfBTW, string? bedrijfEmail, string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            bool bedrijf = true;
            #region Basic colours
            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#623ed0");
            BorderCompanyName.BorderBrush = colorBrush;
            TBCompanyName.Text = "";
            TBCompanyName.Foreground = Brushes.Black;
            BorderBTWnummer.BorderBrush = colorBrush;
            TBBTWnummer.Text = "";
            TBBTWnummer.Foreground = Brushes.Black;
            BorderEmail.BorderBrush = colorBrush;
            TBEmail.Text = "";
            TBEmail.Foreground = Brushes.Black;
            BorderCountry.BorderBrush = colorBrush;
            TBCountry.Text = "";
            TBCountry.Foreground = Brushes.Black;
            BorderStreetName.BorderBrush = colorBrush;
            TBStreetName.Text = "";
            TBStreetName.Foreground = Brushes.Black;
            BorderNumber.BorderBrush = colorBrush;
            TBNumber.Text = "";
            TBNumber.Foreground = Brushes.Black;
            BorderPostalCode.BorderBrush = colorBrush;
            TBPostalCode.Text = "";
            TBPostalCode.Foreground = Brushes.Black;
            BorderPlace.BorderBrush = colorBrush;
            TBPlace.Text = "";
            TBPlace.Foreground = Brushes.Black;
            #endregion
            if (bedrijfNaam == null)
            {
                BorderCompanyName.BorderBrush = Brushes.Red;
                TBCompanyName.Text += "Bedrijf naam: mag niet leeg zijn!";
                TBCompanyName.Foreground = Brushes.Red;
                bedrijf = false;
            }
            if (bedrijfBTW == null)
            {
                BorderBTWnummer.BorderBrush = Brushes.Red;
                TBBTWnummer.Text += "BTW-nummer: mag niet leeg zijn!";
                TBBTWnummer.Foreground = Brushes.Red;
                bedrijf = false;
            }
            if (bedrijfEmail == null)
            {
                BorderEmail.BorderBrush = Brushes.Red;
                TBEmail.Text += "Email: mag niet leeg zijn!";
                TBEmail.Foreground = Brushes.Red;
                bedrijf = false;
            }
            if (bedrijfBTW != null)
            {
                try
                {
                    if (Verify.IsExistingVATnumber(bedrijfBTW)) { }
                }
                catch (Exception ex)
                {
                    bedrijf = false;
                    if (ex.Message == "Controle - IsBestaandBTWnummer - ongeldig BTW Nummer")
                    {
                        BorderBTWnummer.BorderBrush = Brushes.Red;
                        TBBTWnummer.Text += "ongeldige syntacs!";
                        TBBTWnummer.Foreground = Brushes.Red;
                    }
                }
            }
            if (bedrijfEmail != null)
            {
                try
                {
                    if (Verify.IsValidEmailSyntax(bedrijfEmail)) { }
                }
                catch (Exception ex)
                {
                    bedrijf = false;
                    if (ex.Message == "Controle - IsGoedeEmailSyntax - ongeldige email")
                    {
                        BorderEmail.BorderBrush = Brushes.Red;
                        TBEmail.Text += "ongeldige syntacs!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(land) || !string.IsNullOrWhiteSpace(straat) || !string.IsNullOrWhiteSpace(nummer) || !string.IsNullOrWhiteSpace(postcode) || !string.IsNullOrWhiteSpace(plaats))
            {
                if (land == null)
                {
                    BorderCountry.BorderBrush = Brushes.Red;
                    TBCountry.Text += "Land: mag niet leeg zijn!";
                    TBCountry.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (straat == null)
                {
                    BorderStreetName.BorderBrush = Brushes.Red;
                    TBStreetName.Text += "Straatnaam: mag niet leeg zijn!";
                    TBStreetName.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (nummer == null)
                {
                    BorderNumber.BorderBrush = Brushes.Red;
                    TBNumber.Text += "Nummer: mag niet leeg zijn!";
                    TBNumber.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (postcode == null)
                {
                    BorderPostalCode.BorderBrush = Brushes.Red;
                    TBPostalCode.Text += "Postcode: mag niet leeg zijn!";
                    TBPostalCode.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (plaats == null)
                {
                    BorderPlace.BorderBrush = Brushes.Red;
                    TBPlace.Text += "Plaats: mag niet leeg zijn!";
                    TBPlace.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (nummer != null)
                {
                    try
                    {
                        if (Verify.IsValidAdressNumberSyntax(nummer)) { }
                    }
                    catch (Exception ex)
                    {
                        bedrijf = false;
                        if (ex.Message == "Controle - IsGoedeAdresNummerSyntax - geen geldige syntax ingevuld")
                        {
                            BorderNumber.BorderBrush = Brushes.Red;
                            TBNumber.Text += "ongeldige syntax!";
                            TBNumber.Foreground = Brushes.Red;
                        }
                    }
                }
            }
            return bedrijf;
        }

        private void AddEmployeeContract_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                AddEmployeeContractScreen screen = new AddEmployeeContractScreen(_employeeManager, _companyManager, _employeecontractManager, _company);
                screen.Show();
            }
        }
    }
}
