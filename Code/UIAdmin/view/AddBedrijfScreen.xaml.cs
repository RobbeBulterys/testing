using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
    /// Interaction logic for AddBedrijfScreen.xaml
    /// </summary>
    public partial class AddBedrijfScreen : Window
    {
        private bool _isMaximized = false;
        private AddressManager _adresManager;
        private CompanyManager _bedrijfManager;
        public AddBedrijfScreen(AddressManager adresManager, CompanyManager bedrijfManager)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _bedrijfManager = bedrijfManager;
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
                    string? companyName = null;
                    string? companyVatNumber = null;
                    string? companyPhoneNumber = null;
                    string? companyEmail = null;
                    string? country = null;
                    string? street = null;
                    string? number = null;
                    string? postalcode = null;
                    string? place = null;
                    if (!string.IsNullOrWhiteSpace(TextBoxCompanyName.Text)) { companyName = TextBoxCompanyName.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxVatNumber.Text)) { companyVatNumber = TextBoxVatNumber.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text)) { companyPhoneNumber = TextBoxPhoneNumber.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { companyEmail = TextBoxEmail.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxCountry.Text)) { country = TextBoxCountry.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxStreetName.Text)) { street = TextBoxStreetName.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxNumber.Text)) { number = TextBoxNumber.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxPostalCode.Text)) { postalcode = TextBoxPostalCode.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxPlace.Text)) { place = TextBoxPlace.Text; }
                    if (CheckingIsIngevuld(companyName, companyVatNumber, companyEmail, country, street, number, postalcode, place))
                    {
                        try
                        {
                            _bedrijfManager.AddCompany(companyVatNumber, companyName, companyEmail, companyPhoneNumber, country, street, number, postalcode, place);
                            MessageBox.Show($"succes!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"error! {ex.Message}");
                        }
                    }
                    else SaveBtn.IsEnabled = false;
                }
            }
        }
        private bool CheckingIsIngevuld(string? companyName, string? companyVatNumber, string? companyEmail, string? country, string? street, string? number, string? postalcode, string? place)
        {
            bool companyIsCorrect = true;
            #region Basic colours
            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#623ed0");
            BorderCompanyName.BorderBrush = colorBrush;
            TBCompanyName.Text = "";
            TBCompanyName.Foreground = Brushes.Black;
            BorderVatNumber.BorderBrush = colorBrush;
            TBVatNumber.Text = "";
            TBVatNumber.Foreground = Brushes.Black;
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
            if (companyName == null) 
            {
                BorderCompanyName.BorderBrush = Brushes.Red;
                TBCompanyName.Text += "Bedrijf naam: mag niet leeg zijn!";
                TBCompanyName.Foreground = Brushes.Red;
                companyIsCorrect = false;
            } 
            if (companyVatNumber == null)
            {
                BorderVatNumber.BorderBrush = Brushes.Red;
                TBVatNumber.Text += "BTW-nummer: mag niet leeg zijn!";
                TBVatNumber.Foreground = Brushes.Red;
                companyIsCorrect = false;
            }
            if (companyEmail == null)
            {
                BorderEmail.BorderBrush = Brushes.Red;
                TBEmail.Text += "Email: mag niet leeg zijn!";
                TBEmail.Foreground = Brushes.Red;
                companyIsCorrect = false;
            }
            if (companyVatNumber != null)
            {
                try
                {
                    if(Verify.IsExistingVATnumber(companyVatNumber)) { }
                }
                catch (Exception ex)
                {
                    companyIsCorrect = false;
                    if (ex.Message == "Controle - IsBestaandBTWnummer - Ongeldig BTW Nummer")
                    {
                        BorderVatNumber.BorderBrush = Brushes.Red;
                        TBVatNumber.Text += "ongeldige syntax!";
                        TBVatNumber.Foreground = Brushes.Red;
                    }
                }
            }
            if (companyEmail != null)
            {
                try
                {
                    if (Verify.IsValidEmailSyntax(companyEmail)) { }
                }
                catch (Exception ex)
                {
                    companyIsCorrect = false;
                    if (ex.Message == "Controle - IsGoedeEmailSyntax - Ongeldige email")
                    {
                        BorderEmail.BorderBrush = Brushes.Red;
                        TBEmail.Text += "ongeldige syntax!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(country) || !string.IsNullOrWhiteSpace(street) || !string.IsNullOrWhiteSpace(number) || !string.IsNullOrWhiteSpace(postalcode) || !string.IsNullOrWhiteSpace(place))
            {
                if (country == null)
                {
                    BorderCountry.BorderBrush = Brushes.Red;
                    TBCountry.Text += "Land: mag niet leeg zijn!";
                    TBCountry.Foreground = Brushes.Red;
                    companyIsCorrect = false;
                }
                if (street == null)
                {
                    BorderStreetName.BorderBrush = Brushes.Red;
                    TBStreetName.Text += "Straatnaam: mag niet leeg zijn!";
                    TBStreetName.Foreground = Brushes.Red;
                    companyIsCorrect = false;
                }
                if (number == null)
                {
                    BorderNumber.BorderBrush = Brushes.Red;
                    TBNumber.Text += "Nummer: mag niet leeg zijn!";
                    TBNumber.Foreground = Brushes.Red;
                    companyIsCorrect = false;
                }
                if (postalcode == null)
                {
                    BorderPostalCode.BorderBrush = Brushes.Red;
                    TBPostalCode.Text += "Postcode: mag niet leeg zijn!";
                    TBPostalCode.Foreground = Brushes.Red;
                    companyIsCorrect = false;
                }
                if (place == null)
                {
                    BorderPlace.BorderBrush = Brushes.Red;
                    TBPlace.Text += "Plaats: mag niet leeg zijn!";
                    TBPlace.Foreground = Brushes.Red;
                    companyIsCorrect = false;
                }
                if (number != null)
                {
                    try
                    {
                        if (Verify.IsValidAdressNumberSyntax(number)) { }
                    }
                    catch (Exception ex)
                    {
                        companyIsCorrect = false;
                        if (ex.Message == "Controle - IsGoedeAdresNummerSyntax - Geen geldig nummer ingevuld")
                        {
                            BorderNumber.BorderBrush = Brushes.Red;
                            TBNumber.Text += "ongeldige syntacs!";
                            TBNumber.Foreground = Brushes.Red;
                        }
                    }
                }
            }
            return companyIsCorrect;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string? companyName = null;
            string? companyVatNumber = null;
            string? companyPhoneNumber = null;
            string? companyEmail = null;
            string? country = null;
            string? street = null;
            string? number = null;
            string? postalcode = null;
            string? place = null;
            if (!string.IsNullOrWhiteSpace(TextBoxCompanyName.Text)) { companyName = TextBoxCompanyName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxVatNumber.Text)) { companyVatNumber = TextBoxVatNumber.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPhoneNumber.Text)) { companyPhoneNumber = TextBoxPhoneNumber.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { companyEmail = TextBoxEmail.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxCountry.Text)) { country = TextBoxCountry.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxStreetName.Text)) { street = TextBoxStreetName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxNumber.Text)) { number = TextBoxNumber.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPostalCode.Text)) { postalcode = TextBoxPostalCode.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPlace.Text)) { place = TextBoxPlace.Text; }
            if (!SaveBtn.IsEnabled)
            {
                if (CheckingIsIngevuld(companyName, companyVatNumber, companyEmail, country, street, number, postalcode, place))
                {
                    SaveBtn.IsEnabled = true;
                }
            }
        }
    }
}
