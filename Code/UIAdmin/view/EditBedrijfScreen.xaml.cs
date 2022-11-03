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
        private Bedrijf _bedrijf;
        private AdresManager _adresManager;
        private BedrijfManager _bedrijfManager;
        private BezoekManager _bezoekManager;
        private WerknemercontractManager _werknemercontractManager;
        private ObservableCollection<Werknemercontract> werknemercontracts = new ObservableCollection<Werknemercontract>();
        private ObservableCollection<ContractVoorLijst> contracts = new ObservableCollection<ContractVoorLijst>();
        public EditBedrijfScreen(AdresManager adresManager, BedrijfManager bedrijfManager, WerknemercontractManager werknemercontract, BezoekManager bezoekManager, Bedrijf bedrijf)
        {
            InitializeComponent();
            _adresManager = adresManager;
            _bedrijfManager = bedrijfManager;
            _werknemercontractManager = werknemercontract;
            _bezoekManager = bezoekManager;
            _bedrijf = bedrijf;
            InitializeBedrijf(bedrijf);
            BedrijfContractDataGrid.ItemsSource = werknemercontracts;
            BezoekenDataGrid.ItemsSource = contracts;
        }
        private void InitializeBedrijf(Bedrijf bedrijf)
        {
            TextBoxBedrijfNaam.Text = bedrijf.Naam;
            TextBoxBTWnummer.Text = bedrijf.BTWNummer;
            TextBoxTelefoon.Text = bedrijf.Telefoon;
            TextBoxEmail.Text = bedrijf.Email;
            if (bedrijf.Adres != null)
            {
                TextBoxLand.Text = bedrijf.Adres.Land;
                TextBoxStraat.Text = bedrijf.Adres.Straat;
                TextBoxNummer.Text = bedrijf.Adres.Nummer;
                TextBoxPostcode.Text = bedrijf.Adres.Postcode;
                TextBoxPlaats.Text = bedrijf.Adres.Plaats;
            }
            BedrijfIdAanpassen.Text = $"{bedrijf.Naam}";
            TextBlockIdBedrijf.Text = $"Id: {bedrijf.Id}";
            BedrijfIdAanpassenWerknemer.Text = $"{bedrijf.Naam}";
            BedrijfIdAanpassenBezoeken.Text = $"{bedrijf.Naam}";
            BedrijfBtn.Content = $"{bedrijf.Naam}";
            BedrijfBtn.IsChecked = true;
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
                    if (TextBoxBedrijfNaam.Text != _bedrijf.Naam) { bedrijfNaam = TextBoxBedrijfNaam.Text; message += $"bedrijfNaam => {bedrijfNaam}\n"; }
                    if (TextBoxBTWnummer.Text != _bedrijf.BTWNummer) { bedrijfBTW = TextBoxBTWnummer.Text; message += $"bedrijfBTW => {bedrijfBTW}\n"; }
                    if (TextBoxTelefoon.Text != _bedrijf.Telefoon) { bedrijfTelefoon = TextBoxTelefoon.Text; message += $"bedrijfTelefoon => {bedrijfTelefoon}\n"; }
                    if (TextBoxEmail.Text != _bedrijf.Email) { bedrijfEmail = TextBoxEmail.Text; message += $"bedrijfEmail => {bedrijfEmail}\n"; }
                    if (TextBoxLand.Text != "" && _bedrijf.Adres == null) { land = TextBoxLand.Text; message += $"land => {land}\n"; }
                    if (TextBoxStraat.Text != "" && _bedrijf.Adres == null) { straat = TextBoxStraat.Text; message += $"straat => {straat}\n"; }
                    if (TextBoxNummer.Text != "" && _bedrijf.Adres == null) { nummer = TextBoxNummer.Text; message += $"nummer => {nummer}\n"; }
                    if (TextBoxPostcode.Text != "" && _bedrijf.Adres == null) { postcode = TextBoxPostcode.Text; message += $"postcode => {postcode}\n"; }
                    if (TextBoxPlaats.Text != "" && _bedrijf.Adres == null) { plaats = TextBoxPlaats.Text; message += $"plaats => {plaats}\n"; }
                    if (_bedrijf.Adres != null)
                    {
                        if (TextBoxLand.Text != _bedrijf.Adres.Land) { land = TextBoxLand.Text; message += $"land => {land}\n"; }
                        if (TextBoxStraat.Text != _bedrijf.Adres.Straat) { straat = TextBoxStraat.Text; message += $"straat => {straat}\n"; }
                        if (TextBoxNummer.Text != _bedrijf.Adres.Nummer) { nummer = TextBoxNummer.Text; message += $"nummer => {nummer}\n"; }
                        if (TextBoxPostcode.Text != _bedrijf.Adres.Postcode) { postcode = TextBoxPostcode.Text; message += $"postcode => {postcode}\n"; }
                        if (TextBoxPlaats.Text != _bedrijf.Adres.Plaats) { plaats = TextBoxPlaats.Text; message += $"plaats => {plaats}\n"; }
                    }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else if (!string.IsNullOrWhiteSpace(land) || !string.IsNullOrWhiteSpace(straat) || !string.IsNullOrWhiteSpace(nummer) || !string.IsNullOrWhiteSpace(postcode) || !string.IsNullOrWhiteSpace(plaats))
                    {
                        if ((land == null || straat == null || nummer == null || postcode == null || plaats == null) && _bedrijf.Adres == null) MessageBox.Show("Alle velden voor het adres moeten worden ingevuld!");
                        else
                        {
                            try
                            {
                                if (_bedrijf.Adres == null)
                                {
                                    int id = _adresManager.VoegAdresToe(new Adres(straat, nummer, postcode, plaats, land));
                                    _bedrijfManager.UpdateBedrijfAdres(_bedrijf.Id, id);
                                }
                                else
                                {
                                    _adresManager.UpdateAdres(_bedrijf.Adres.Id, straat, nummer, postcode, plaats, land);
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
                            _bedrijfManager.UpdateBedrijf(_bedrijf.Id, bedrijfBTW, bedrijfNaam, bedrijfEmail, bedrijfTelefoon);
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
        private void BedrijfBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BedrijfBorder.Visibility = Visibility.Visible;
                WerknemersBorder.Visibility = Visibility.Collapsed;
                BezoekenBorder.Visibility = Visibility.Collapsed;
            }
        }
        private void WerknemerslijstBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BedrijfBorder.Visibility = Visibility.Collapsed;
                BezoekenBorder.Visibility = Visibility.Collapsed;
                WerknemersBorder.Visibility = Visibility.Visible;
                werknemercontracts.Clear();
                List<Werknemercontract> w = _werknemercontractManager.GeefContractenVanBedrijf(_bedrijf).ToList();
                w.ForEach(c => werknemercontracts.Add(c));
            }
        }
        private void BezoekenLijstBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BedrijfBorder.Visibility = Visibility.Collapsed;
                WerknemersBorder.Visibility = Visibility.Collapsed;
                BezoekenBorder.Visibility = Visibility.Visible;
                //TODO bezoeken ophalen adhv bedrijf.
                contracts.Clear();
                List<Bezoek> b = _bezoekManager.ZoekBezoeken(null, _bedrijf, null, null).ToList();
                foreach (Bezoek bezoek in b)
                {
                    contracts.Add(new ContractVoorLijst(bezoek));
                }
            }
        }
    }
}
