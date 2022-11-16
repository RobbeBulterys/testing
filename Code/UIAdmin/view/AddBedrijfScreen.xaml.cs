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
        private AdresManager _adresManager;
        private BedrijfManager _bedrijfManager;
        public AddBedrijfScreen(AdresManager adresManager, BedrijfManager bedrijfManager)
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
                    string? bedrijfNaam = null;
                    string? bedrijfBTW = null;
                    string? bedrijfTelefoon = null;
                    string? bedrijfEmail = null;
                    string? land = null;
                    string? straat = null;
                    string? nummer = null;
                    string? postcode = null;
                    string? plaats = null;
                    if (!string.IsNullOrWhiteSpace(TextBoxBedrijfNaam.Text)) { bedrijfNaam = TextBoxBedrijfNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxBTWnummer.Text)) { bedrijfBTW = TextBoxBTWnummer.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxTelefoon.Text)) { bedrijfTelefoon = TextBoxTelefoon.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { bedrijfEmail = TextBoxEmail.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxLand.Text)) { land = TextBoxLand.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxStraat.Text)) { straat = TextBoxStraat.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxNummer.Text)) { nummer = TextBoxNummer.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxPostcode.Text)) { postcode = TextBoxPostcode.Text; }
                    if (!string.IsNullOrWhiteSpace(TextBoxPlaats.Text)) { plaats = TextBoxPlaats.Text; }
                    if (CheckingIsIngevuld(bedrijfNaam, bedrijfBTW, bedrijfEmail, land, straat, nummer, postcode, plaats))
                    {
                        try
                        {
                            _bedrijfManager.VoegBedrijfToe(bedrijfBTW, bedrijfNaam, bedrijfEmail, bedrijfTelefoon, land, straat, nummer, postcode, plaats);
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
        private bool CheckingIsIngevuld(string? bedrijfNaam, string? bedrijfBTW, string? bedrijfEmail, string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            bool bedrijf = true;
            #region Basic colours
            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#623ed0");
            BorderBedrijfNaam.BorderBrush = colorBrush;
            TBBedrijfNaam.Text = "";
            TBBedrijfNaam.Foreground = Brushes.Black;
            BorderBTWnummer.BorderBrush = colorBrush;
            TBBTWnummer.Text = "";
            TBBTWnummer.Foreground = Brushes.Black;
            BorderEmail.BorderBrush = colorBrush;
            TBEmail.Text = "";
            TBEmail.Foreground = Brushes.Black;
            BorderLand.BorderBrush = colorBrush;
            TBLand.Text = "";
            TBLand.Foreground = Brushes.Black;
            BorderStraatNaam.BorderBrush = colorBrush;
            TBStraatNaam.Text = "";
            TBStraatNaam.Foreground = Brushes.Black;
            BorderNummer.BorderBrush = colorBrush;
            TBNummer.Text = "";
            TBNummer.Foreground = Brushes.Black;
            BorderPostcode.BorderBrush = colorBrush;
            TBPostcode.Text = "";
            TBPostcode.Foreground = Brushes.Black;
            BorderPlaats.BorderBrush = colorBrush;
            TBPlaats.Text = "";
            TBPlaats.Foreground = Brushes.Black;
            #endregion
            if (bedrijfNaam == null) 
            {
                BorderBedrijfNaam.BorderBrush = Brushes.Red;
                TBBedrijfNaam.Text += "Bedrijf naam: mag niet leeg zijn!";
                TBBedrijfNaam.Foreground = Brushes.Red;
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
                    if(Controle.IsBestaandBTWnummer(bedrijfBTW)) { }
                }
                catch (Exception ex)
                {
                    bedrijf = false;
                    if (ex.Message == "Controle - IsBestaandBTWnummer - Ongeldig BTW Nummer")
                    {
                        BorderBTWnummer.BorderBrush = Brushes.Red;
                        TBBTWnummer.Text += "ongeldige syntax!";
                        TBBTWnummer.Foreground = Brushes.Red;
                    }
                }
            }
            if (bedrijfEmail != null)
            {
                try
                {
                    if (Controle.IsGoedeEmailSyntax(bedrijfEmail)) { }
                }
                catch (Exception ex)
                {
                    bedrijf = false;
                    if (ex.Message == "Controle - IsGoedeEmailSyntax - Ongeldige email")
                    {
                        BorderEmail.BorderBrush = Brushes.Red;
                        TBEmail.Text += "ongeldige syntax!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(land) || !string.IsNullOrWhiteSpace(straat) || !string.IsNullOrWhiteSpace(nummer) || !string.IsNullOrWhiteSpace(postcode) || !string.IsNullOrWhiteSpace(plaats))
            {
                if (land == null)
                {
                    BorderLand.BorderBrush = Brushes.Red;
                    TBLand.Text += "Land: mag niet leeg zijn!";
                    TBLand.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (straat == null)
                {
                    BorderStraatNaam.BorderBrush = Brushes.Red;
                    TBStraatNaam.Text += "Straatnaam: mag niet leeg zijn!";
                    TBStraatNaam.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (nummer == null)
                {
                    BorderNummer.BorderBrush = Brushes.Red;
                    TBNummer.Text += "Nummer: mag niet leeg zijn!";
                    TBNummer.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (postcode == null)
                {
                    BorderPostcode.BorderBrush = Brushes.Red;
                    TBPostcode.Text += "Postcode: mag niet leeg zijn!";
                    TBPostcode.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (plaats == null)
                {
                    BorderPlaats.BorderBrush = Brushes.Red;
                    TBPlaats.Text += "Plaats: mag niet leeg zijn!";
                    TBPlaats.Foreground = Brushes.Red;
                    bedrijf = false;
                }
                if (nummer != null)
                {
                    try
                    {
                        if (Controle.IsGoedeAdresNummerSyntax(nummer)) { }
                    }
                    catch (Exception ex)
                    {
                        bedrijf = false;
                        if (ex.Message == "Controle - IsGoedeAdresNummerSyntax - Geen geldig nummer ingevuld")
                        {
                            BorderNummer.BorderBrush = Brushes.Red;
                            TBNummer.Text += "ongeldige syntacs!";
                            TBNummer.Foreground = Brushes.Red;
                        }
                    }
                }
            }
            return bedrijf;
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
            if (!string.IsNullOrWhiteSpace(TextBoxBedrijfNaam.Text)) { bedrijfNaam = TextBoxBedrijfNaam.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxBTWnummer.Text)) { bedrijfBTW = TextBoxBTWnummer.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxTelefoon.Text)) { bedrijfTelefoon = TextBoxTelefoon.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { bedrijfEmail = TextBoxEmail.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxLand.Text)) { land = TextBoxLand.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxStraat.Text)) { straat = TextBoxStraat.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxNummer.Text)) { nummer = TextBoxNummer.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPostcode.Text)) { postcode = TextBoxPostcode.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxPlaats.Text)) { plaats = TextBoxPlaats.Text; }
            if (!SaveBtn.IsEnabled)
            {
                if (CheckingIsIngevuld(bedrijfNaam, bedrijfBTW, bedrijfEmail, land, straat, nummer, postcode, plaats))
                {
                    SaveBtn.IsEnabled = true;
                }
            }
        }
    }
}
