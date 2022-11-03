using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AdresManager _adresManager;
        private ObservableCollection<Werknemer> werknemersList = new ObservableCollection<Werknemer>();
        private ObservableCollection<Bedrijf> bedrijven = new ObservableCollection<Bedrijf>();
        private ObservableCollection<Bezoeker> bezoekers = new ObservableCollection<Bezoeker>();
        private ObservableCollection<Bezoek> bezoeken = new ObservableCollection<Bezoek>();
        private ObservableCollection<string> BedrijfNamen = new ObservableCollection<string>();
        private ObservableCollection<string> WerknemersNamen = new ObservableCollection<string>();
        public MainWindow(AdresManager adresManager)
        {
            InitializeComponent();

            OnStartupCollections();
            _adresManager = adresManager;

            //Testinghere(null, "Pollarestraat", null, null, null);
        }
        //Connecties tussen de ObservableCollections en de listviews en comboboxen
        private void OnStartupCollections()
        {
            ListviewBedrijfOpzoeken.ItemsSource = bedrijven;
            ListviewWerknemerOpzoeken.ItemsSource = werknemersList;
            ListviewBezoekerOpzoeken.ItemsSource = bezoekers;
            ListviewBezoekOpzoeken.ItemsSource = bezoeken;
            BezoekToevoegenComboBoxBedrijfNaam.ItemsSource = BedrijfNamen;
            BezoekToevoegenComboBoxContactPersoon.ItemsSource = WerknemersNamen;
            BezoekOpzoekenComboBoxBedrijfNaam.ItemsSource = BedrijfNamen;
            BezoekOpzoekenComboBoxContactPersoon.ItemsSource = WerknemersNamen;
        }

        //Het aanpassen van de grote van de buttons als de grote van de windowscreen wordt aangepast
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            #region Bedrijf Toevoegen
            BedrijfToevoegenToevoegBtn.FontSize = 13;
            BedrijfToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BedrijfToevoegenToevoegBtn.FontSize = 24;
                BedrijfToevoegenToevoegBtn.Height = 50;
            }
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BedrijfToevoegenToevoegBtn.FontSize = 20;
                BedrijfToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Bedrijf Opzoeken
            BedrijfOpvragenVerwijderenBtn.FontSize = 13;
            BedrijfOpvragenVerwijderenBtn.Height = 25;
            BedrijfOpvragenAanpassenBtn.FontSize = 13;
            BedrijfOpvragenAanpassenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BedrijfOpvragenVerwijderenBtn.Height = 50;
                BedrijfOpvragenVerwijderenBtn.FontSize = 24;
                BedrijfOpvragenAanpassenBtn.Height = 50;
                BedrijfOpvragenAanpassenBtn.FontSize = 24;
            }
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BedrijfOpvragenVerwijderenBtn.Height = 50;
                BedrijfOpvragenVerwijderenBtn.FontSize = 24;
                BedrijfOpvragenAanpassenBtn.Height = 50;
                BedrijfOpvragenAanpassenBtn.FontSize = 24;
            }
            BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
                BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(700);
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
                BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(250);
            #endregion
            #region Werknemer Toevoegen
            WerknemerToevoegenToevoegBtn.FontSize = 13;
            WerknemerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WerknemerToevoegenToevoegBtn.FontSize = 24;
                WerknemerToevoegenToevoegBtn.Height = 50;
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                WerknemerToevoegenToevoegBtn.FontSize = 24;
                WerknemerToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Werknemer Opzoeken
            WerknemerOpvragenVerwijderenBtn.FontSize = 13;
            WerknemerOpvragenVerwijderenBtn.Height = 25;
            WerknemerOpvragenAanpassenBtn.FontSize = 13;
            WerknemerOpvragenAanpassenBtn.Height = 25;
            WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WerknemerOpvragenVerwijderenBtn.FontSize = 24;
                WerknemerOpvragenVerwijderenBtn.Height = 50;
                WerknemerOpvragenAanpassenBtn.FontSize = 24;
                WerknemerOpvragenAanpassenBtn.Height = 50;
                WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                WerknemerOpvragenVerwijderenBtn.FontSize = 24;
                WerknemerOpvragenVerwijderenBtn.Height = 50;
                WerknemerOpvragenAanpassenBtn.FontSize = 24;
                WerknemerOpvragenAanpassenBtn.Height = 50;
                WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            #endregion
            #region Bezoeker Toevoegen
            BezoekerToevoegenGridPanelColumn0.Width = new GridLength(200);
            BezoekerToevoegenToevoegBtn.FontSize = 13;
            BezoekerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekerToevoegenToevoegBtn.FontSize = 24;
                BezoekerToevoegenToevoegBtn.Height = 50;
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekerToevoegenToevoegBtn.FontSize = 24;
                BezoekerToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Bezoeker Opzoeken
            BezoekerOpvragenVerwijderenBtn.FontSize = 13;
            BezoekerOpvragenVerwijderenBtn.Height = 25;
            BezoekerOpvragenAanpassenBtn.FontSize = 13;
            BezoekerOpvragenAanpassenBtn.Height = 25;
            BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekerOpvragenVerwijderenBtn.FontSize = 24;
                BezoekerOpvragenVerwijderenBtn.Height = 50;
                BezoekerOpvragenAanpassenBtn.FontSize = 24;
                BezoekerOpvragenAanpassenBtn.Height = 50;
                BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekerOpvragenVerwijderenBtn.FontSize = 24;
                BezoekerOpvragenVerwijderenBtn.Height = 50;
                BezoekerOpvragenAanpassenBtn.FontSize = 24;
                BezoekerOpvragenAanpassenBtn.Height = 50;
                BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            #endregion
            #region Bezoek Toevoegen
            BezoekToevoegenToevoegBtn.FontSize = 13;
            BezoekToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekToevoegenToevoegBtn.FontSize = 24;
                BezoekToevoegenToevoegBtn.Height = 50;
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekToevoegenToevoegBtn.FontSize = 24;
                BezoekToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Bezoeker Opzoeken
            BezoekOpzoekenVerwijderenBtn.FontSize = 13;
            BezoekOpzoekenVerwijderenBtn.Height = 25;
            BezoekOpzoekenAanpassenBtn.FontSize = 13;
            BezoekOpzoekenAanpassenBtn.Height = 25;
            BezoekOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekOpzoekenVerwijderenBtn.FontSize = 24;
                BezoekOpzoekenVerwijderenBtn.Height = 50;
                BezoekOpzoekenAanpassenBtn.FontSize = 24;
                BezoekOpzoekenAanpassenBtn.Height = 50;
                BezoekOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekOpzoekenVerwijderenBtn.FontSize = 24;
                BezoekOpzoekenVerwijderenBtn.Height = 50;
                BezoekOpzoekenAanpassenBtn.FontSize = 24;
                BezoekOpzoekenAanpassenBtn.Height = 50;
                BezoekOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            #endregion
        }

        //Het Weergeven van de bovenstaande buttons naargelang je drukt op bedrijf, werknemer, bezoeker of bezoek
        private void TopRowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                RadioButton radioButton = (RadioButton)sender;
                TopRowBtnExtras();
                //Het Weergeven van de bovenstaande buttons als je op bedrijf drukt
                if (radioButton.Name == "BedrijvenBtn")
                {
                    BedrijfPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BedrijfToevoegenBtn.Opacity = 1;
                    BedrijfOpzoekenBtn.Opacity = 1;
                    ContractToevoegenBtn.Opacity = 1;
                    ContractOpzoekenBtn.Opacity = 1;
                }
                //Het Weergeven van de bovenstaande buttons als je op werknemer drukt
                else if (radioButton.Name == "WerknemersBtn")
                {
                    WerknemerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    WerknemerToevoegenBtn.Opacity = 1;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                //Het Weergeven van de bovenstaande buttons als je op bezoeker drukt
                else if (radioButton.Name == "BezoekersBtn")
                {
                    BezoekerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BezoekerToevoegenBtn.Opacity = 1;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
                //Het Weergeven van de bovenstaande buttons als je op bezoek drukt
                else if (radioButton.Name == "BezoekenBtn")
                {
                    BezoekenPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BezoekenToevoegenBtn.Opacity = 1;
                    BezoekenOpzoekenBtn.Opacity = 1;
                }
            }
        }

        //Het verbergen van alle screens om er voor te zorgen dat enkel de juiste wordt weergegeven
        //Dit wordt opgeroepen door de methode: "TopRowBtn_Click"
        private void TopRowBtnExtras()
        {
            BedrijfPanel.Visibility = Visibility.Hidden;
            WerknemerPanel.Visibility = Visibility.Hidden;
            BezoekerPanel.Visibility = Visibility.Hidden;
            BezoekenPanel.Visibility = Visibility.Hidden;
            BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BezoekToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BezoekOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfToevoegenBtn.IsChecked = false;
            BedrijfOpzoekenBtn.IsChecked = false;
            ContractToevoegenBtn.IsChecked = false;
            ContractOpzoekenBtn.IsChecked = false;
            WerknemerToevoegenBtn.IsChecked = false;
            WerknemerOpzoekenBtn.IsChecked = false;
            BezoekerToevoegenBtn.IsChecked = false;
            BezoekerOpzoekenBtn.IsChecked = false;
            BezoekenToevoegenBtn.IsChecked = false;
            BezoekenOpzoekenBtn.IsChecked = false;
            TopSecondDP.Background = Brushes.LightGray;
        }

        //Het Weergeven van de screens naargelang je op de bovenstaande buttons drukt
        private void TopRowBedrijfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                RadioButton radioButton = (RadioButton)sender;
                TopRowBedrijfBtnExtras(radioButton.Name);
                if (radioButton.Name == "BedrijfToevoegenBtn")
                {
                    BedrijfToevoegenGridPanel.Visibility = Visibility.Visible;
                    BedrijfToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BedrijfOpzoekenBtn")
                {
                    BedrijfOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BedrijfOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "ContractToevoegenBtn")
                {
                    ContractToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "ContractOpzoekenBtn")
                {
                    ContractOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "WerknemerToevoegenBtn")
                {
                    WerknemerToevoegenGridPanel.Visibility = Visibility.Visible;
                    WerknemerToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "WerknemerOpzoekenBtn")
                {
                    WerknemerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekerToevoegenBtn")
                {
                    BezoekerToevoegenGridPanel.Visibility = Visibility.Visible;
                    BezoekerToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekerOpzoekenBtn")
                {
                    BezoekerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekenToevoegenBtn")
                {
                    //BezoekToevoegenGridPanel.Visibility = Visibility.Visible;
                    //BezoekenToevoegenBtn.Opacity = 1;
                    //List<Bedrijf> bedrijfs = new List<Bedrijf>();
                    //Bedrijf bedrijf1 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
                    //bedrijf1.ZetAdres(new Adres(1, "Bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
                    //bedrijf1.ZetTelefoon("0491732014");
                    //bedrijfs.Add(bedrijf1);
                    //Bedrijf bedrijf2 = new Bedrijf(2, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    //bedrijf2.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    //bedrijf2.ZetTelefoon("0491732123");
                    //bedrijfs.Add(bedrijf2);
                    //BedrijfNamen.Clear();
                    //bedrijfs.ForEach(c => BedrijfNamen.Add(c.Naam));
                    //List<Werknemer> werknemers = new List<Werknemer>();
                    //Werknemer werknemer1 = new Werknemer(1, "Jonssen", "Robrecht");
                    //werknemer1.ZetEmail("info@example.com");
                    //werknemers.Add(werknemer1);
                    //werknemers.Add(new Werknemer(2, "Janssen", "Jan"));
                    //werknemers.Add(new Werknemer(3, "Meenens", "Hozee"));
                    //werknemers.Add(new Werknemer(4, "David", "Achmed"));
                    //werknemers.Add(new Werknemer(5, "DeMaire", "Pieter"));
                    //werknemers.Add(new Werknemer(6, "Jonssen", "Robrecht"));
                    //werknemers.Add(new Werknemer(7, "Janssen", "Jan"));
                    //werknemers.Add(new Werknemer(8, "Meenens", "Hozee"));
                    //werknemers.Add(new Werknemer(9, "David", "Achmed"));
                    //werknemers.Add(new Werknemer(10, "DeMaire", "Pieter"));
                    //WerknemersNamen.Clear();
                    //werknemers.ForEach(c => WerknemersNamen.Add($"{c.Naam}, {c.Voornaam}"));
                }
                else if (radioButton.Name == "BezoekenOpzoekenBtn")
                {

                }
            }
        }

        //Het verbergen van alle screens om er voor te zorgen dat enkel de juiste wordt weergegeven
        //Dit wordt opgeroepen door de methode: "TopRowBedrijfBtn_Click"
        private void TopRowBedrijfBtnExtras(string name)
        {
            if (name != "BedrijfToevoegenBtn")
            {
                BedrijfToevoegenBtn.Opacity = 0.5;
                BedrijfToevoegenBtn.IsChecked = false;
                BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BedrijfOpzoekenBtn")
            {
                BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;
                BedrijfOpzoekenBtn.IsChecked = false;
                BedrijfOpzoekenBtn.Opacity = 0.5;
            }
            if (name != "ContractToevoegenBtn")
            {
                ContractToevoegenBtn.Opacity = 0.5;
                ContractToevoegenBtn.IsChecked = false;
            }
            if (name != "ContractOpzoekenBtn")
            {
                ContractOpzoekenBtn.Opacity = 0.5;
                ContractOpzoekenBtn.IsChecked = false;
            }
            if (name != "WerknemerToevoegenBtn")
            {
                WerknemerToevoegenBtn.Opacity = 0.5;
                WerknemerToevoegenBtn.IsChecked = false;
                WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "WerknemerOpzoekenBtn")
            {
                WerknemerOpzoekenBtn.Opacity = 0.5;
                WerknemerOpzoekenBtn.IsChecked = false;
                WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekerToevoegenBtn")
            {
                BezoekerToevoegenBtn.Opacity = 0.5;
                BezoekerToevoegenBtn.IsChecked = false;
                BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekerOpzoekenBtn")
            {
                BezoekerOpzoekenBtn.Opacity = 0.5;
                BezoekerOpzoekenBtn.IsChecked = false;
                BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekenToevoegenBtn")
            {
                BezoekenToevoegenBtn.Opacity = 0.5;
                BezoekenToevoegenBtn.IsChecked = false;
                BezoekToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekenOpzoekenBtn")
            {
                BezoekenOpzoekenBtn.Opacity = 0.5;
                BezoekenOpzoekenBtn.IsChecked = false;
                BezoekOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            }
        }

        //Het geeft aan welk item is ge selecteerd in de listview en geeft de data mee aan de juiste textboxen om het weer te geven
        private void ListviewWerknemerOpzoeken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListviewWerknemerOpzoeken.SelectedItem is Werknemer)
            {
                Werknemer werknemer = (Werknemer)ListviewWerknemerOpzoeken.SelectedItem;
                WerknemerOpzoekenTextBoxNaam.Text = werknemer.Naam;
                WerknemerOpzoekenTextBoxVoornaam.Text = werknemer.Voornaam;
                WerknemerOpzoekenTextBoxEmail.Text = werknemer.Email;
            }
            if (ListviewBedrijfOpzoeken.SelectedItem is Bedrijf)
            {
                Bedrijf bedrijf = (Bedrijf)ListviewBedrijfOpzoeken.SelectedItem;
                BedrijfOpzoekenTextBoxNaam.Text = bedrijf.Naam;
                BedrijfOpzoekenTextBoxBTW.Text = bedrijf.BTWNummer;
                BedrijfOpzoekenTextBoxTelefoon.Text = bedrijf.Telefoon;
                BedrijfOpzoekenTextBoxEmail.Text = bedrijf.Email;
                BedrijfOpzoekenTextBoxLand.Text = bedrijf.Adres.Land;
                BedrijfOpzoekenTextBoxStraat.Text = bedrijf.Adres.Straat;
                BedrijfOpzoekenTextBoxPostcode.Text = bedrijf.Adres.Postcode;
                BedrijfOpzoekenTextBoxNr.Text = bedrijf.Adres.Nummer;
                BedrijfOpzoekenTextBoxPlaats.Text = bedrijf.Adres.Plaats;
            }
            if (ListviewBezoekerOpzoeken.SelectedItem is Bezoeker)
            {
                Bezoeker bezoeker = (Bezoeker)ListviewBezoekerOpzoeken.SelectedItem;
                BezoekerOpzoekenTextBoxNaam.Text = bezoeker.Naam;
                BezoekerOpzoekenTextBoxVoornaam.Text = bezoeker.Voornaam;
                BezoekerOpzoekenTextBoxEmail.Text = bezoeker.Email;
                BezoekerOpzoekenTextBoxBedrijfNaam.Text = bezoeker.Bedrijf;
            }
            if (ListviewBezoekOpzoeken.SelectedItem is Bezoek)
            {
                Bezoek bezoek = (Bezoek)ListviewBezoekOpzoeken.SelectedItem;
                BezoekOpzoekenTextBoxEmail.Text = bezoek.Bezoeker.Email;
                BezoekOpzoekenTextBoxNaam.Text = bezoek.Bezoeker.Naam;
                BezoekOpzoekenTextBoxVoornaam.Text = bezoek.Bezoeker.Voornaam;
                BezoekOpzoekenTextBoxBedrijfNaamBezoeker.Text = bezoek.Bezoeker.Bedrijf;
                BezoekOpzoekenComboBoxBedrijfNaam.SelectedValue = bezoek.Bedrijf.Naam;
                foreach (string s in WerknemersNamen)
                {
                    if (s.Contains(bezoek.Contactpersoon.Naam)) BezoekOpzoekenComboBoxContactPersoon.SelectedValue = s;
                }
            }
        }

        //Alle Buttons voor bedrijf(toevoegen, aanpassen, verwijderen en filter)
        #region bedrijf buttons
        private void BedrijfOpvragenAanpassenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBedrijfOpzoeken.SelectedItems.Count == 1)
            {
                Bedrijf bedrijf = (Bedrijf)ListviewBedrijfOpzoeken.SelectedItem;
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
                if (BedrijfOpzoekenTextBoxNaam.Text != bedrijf.Naam) { bedrijfNaam = BedrijfOpzoekenTextBoxNaam.Text; message += $"bedrijfNaam => {bedrijfNaam}\n"; }
                if (BedrijfOpzoekenTextBoxBTW.Text != bedrijf.BTWNummer) { bedrijfBTW = BedrijfOpzoekenTextBoxBTW.Text; message += $"bedrijfBTW => {bedrijfBTW}\n"; }
                if (BedrijfOpzoekenTextBoxTelefoon.Text != bedrijf.Telefoon) { bedrijfTelefoon = BedrijfOpzoekenTextBoxTelefoon.Text; message += $"bedrijfTelefoon => {bedrijfTelefoon}\n"; }
                if (BedrijfOpzoekenTextBoxEmail.Text != bedrijf.Email) { bedrijfEmail = BedrijfOpzoekenTextBoxEmail.Text; message += $"bedrijfEmail => {bedrijfEmail}\n"; }
                if (BedrijfOpzoekenTextBoxLand.Text != bedrijf.Adres.Land) { land = BedrijfOpzoekenTextBoxLand.Text; message += $"land => {land}\n"; }
                if (BedrijfOpzoekenTextBoxStraat.Text != bedrijf.Adres.Straat) { straat = BedrijfOpzoekenTextBoxStraat.Text; message += $"straat => {straat}\n"; }
                if (BedrijfOpzoekenTextBoxNr.Text != bedrijf.Adres.Nummer) { nummer = BedrijfOpzoekenTextBoxNr.Text; message += $"nummer => {nummer}\n"; }
                if (BedrijfOpzoekenTextBoxPostcode.Text != bedrijf.Adres.Postcode) { postcode = BedrijfOpzoekenTextBoxPostcode.Text; message += $"postcode => {postcode}\n"; }
                if (BedrijfOpzoekenTextBoxPlaats.Text != bedrijf.Adres.Plaats) { plaats = BedrijfOpzoekenTextBoxPlaats.Text; message += $"plaats => {plaats}\n"; }
                if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                else MessageBox.Show(message);
            }
            else MessageBox.Show("Je mag maximum en minimum 1 bedrijf in \nde lijst tegelijker tijd aanpassen");
        }
        private void BedrijfOpvragenVerwijderenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBedrijfOpzoeken.SelectedItems.Count == 1)
            {
                Bedrijf bedrijf = (Bedrijf)ListviewBedrijfOpzoeken.SelectedItem;
                MessageBox.Show($"BedrijfId => {bedrijf.Id}");
                bedrijven.Clear();
                BedrijfOpzoekenTextBoxNaam.Text = "";
                BedrijfOpzoekenTextBoxBTW.Text = "";
                BedrijfOpzoekenTextBoxTelefoon.Text = "";
                BedrijfOpzoekenTextBoxEmail.Text = "";
                BedrijfOpzoekenTextBoxLand.Text = "";
                BedrijfOpzoekenTextBoxStraat.Text = "";
                BedrijfOpzoekenTextBoxPostcode.Text = "";
                BedrijfOpzoekenTextBoxNr.Text = "";
                BedrijfOpzoekenTextBoxPlaats.Text = "";
                BedrijfOpzoekenFilterTextBoxNaam.Text = "";
                BedrijfOpzoekenFilterTextBoxBTW.Text = "";
                BedrijfOpzoekenFilterTextBoxId.Text = "";
            }
            else MessageBox.Show("Je mag maximum en minimum 1 bedrijf verwijderen");
        }
        private void BedrijfOpvragenFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BedrijfOpvragenFilterBtn")
                {
                    string? bedrijfNaam = null;
                    string? bedrijfBTW = null;
                    string? bedrijfId = null;
                    if (!string.IsNullOrWhiteSpace(BedrijfOpzoekenFilterTextBoxNaam.Text)) { bedrijfNaam = BedrijfOpzoekenFilterTextBoxNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BedrijfOpzoekenFilterTextBoxBTW.Text)) { bedrijfBTW = BedrijfOpzoekenFilterTextBoxBTW.Text; }
                    if (!string.IsNullOrWhiteSpace(BedrijfOpzoekenFilterTextBoxId.Text)) { bedrijfId = BedrijfOpzoekenFilterTextBoxId.Text; }
                    MessageBox.Show($"bedrijfId => {bedrijfId}\nbedrijfNaam => {bedrijfNaam}\nbedrijfBTW => {bedrijfBTW}");
                    bedrijven.Clear();
                    Bedrijf bedrijf1 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
                    bedrijf1.ZetAdres(new Adres(1, "Bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
                    bedrijf1.ZetTelefoon("0491732014");
                    bedrijven.Add(bedrijf1);
                    Bedrijf bedrijf2 = new Bedrijf(2, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf2.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf2.ZetTelefoon("0491732123");
                    bedrijven.Add(bedrijf2);
                    Bedrijf bedrijf3 = new Bedrijf(3, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf3.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf3.ZetTelefoon("0491732123");
                    bedrijven.Add(bedrijf3);
                    Bedrijf bedrijf4 = new Bedrijf(4, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf4.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf4.ZetTelefoon("0491732123");
                    bedrijven.Add(bedrijf4);
                    Bedrijf bedrijf5 = new Bedrijf(5, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf5.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf5.ZetTelefoon("0491732123");
                    bedrijven.Add(bedrijf5);
                }
            }
        }
        private void BedrijfToevoegenToevoegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BedrijfToevoegenToevoegBtn")
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
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxNaam.Text)) { bedrijfNaam = BedrijfToevoegenTextBoxNaam.Text; message += $"bedrijfNaam => {bedrijfNaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxBTW.Text)) { bedrijfBTW = BedrijfToevoegenTextBoxBTW.Text; message += $"bedrijfBTW => {bedrijfBTW}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxTelefoon.Text)) { bedrijfTelefoon = BedrijfToevoegenTextBoxTelefoon.Text; message += $"bedrijfTelefoon => {bedrijfTelefoon}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxEmail.Text)) { bedrijfEmail = BedrijfToevoegenTextBoxEmail.Text; message += $"bedrijfEmail => {bedrijfEmail}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxLand.Text)) { land = BedrijfToevoegenTextBoxLand.Text; message += $"land => {land}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxstraat.Text)) { straat = BedrijfToevoegenTextBoxstraat.Text; message += $"straat => {straat}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxNr.Text)) { nummer = BedrijfToevoegenTextBoxNr.Text; message += $"nummer => {nummer}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxPostcode.Text)) { postcode = BedrijfToevoegenTextBoxPostcode.Text; message += $"postcode => {postcode}\n"; }
                    if (!string.IsNullOrWhiteSpace(BedrijfToevoegenTextBoxPlaats.Text)) { plaats = BedrijfToevoegenTextBoxPlaats.Text; message += $"plaats => {plaats}\n"; }
                    if (bedrijfNaam == null || bedrijfBTW == null || bedrijfTelefoon == null || bedrijfEmail == null || land == null || straat == null || nummer == null || postcode == null || plaats == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else MessageBox.Show(message);
                }
            }
        }
        #endregion
        //Alle Buttons voor werknemer(toevoegen, aanpassen, verwijderen en filter)
        #region werknemer buttons
        private void WerknemerToevoegenToevoegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "WerknemerToevoegenToevoegBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? email = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(WerknemerToevoegenTextBoxNaam.Text)) { naam = WerknemerToevoegenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(WerknemerToevoegenTextBoxVoornaam.Text)) { voornaam = WerknemerToevoegenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(WerknemerToevoegenTextBoxEmail.Text)) { email = WerknemerToevoegenTextBoxEmail.Text; message += $"email => {email}\n"; }
                    if (naam == null || voornaam == null || email == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else MessageBox.Show(message);
                }
            }
        }
        private void WerknemerOpvragenFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "WerknemerOpvragenFilterBtn")
                {

                }
            }
        }
        private void WerknemerOpvragenVerwijderenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewWerknemerOpzoeken.SelectedItems.Count == 1)
            {
                Werknemer werknemer = (Werknemer)ListviewWerknemerOpzoeken.SelectedItem;
                MessageBox.Show($"werknemerId => {werknemer.PersoonId}");
                werknemersList.Clear();
                WerknemerOpzoekenTextBoxNaam.Text = "";
                WerknemerOpzoekenTextBoxVoornaam.Text = "";
                WerknemerOpzoekenTextBoxEmail.Text = "";
                WerknemerOpzoekenFilterTextBoxNaam.Text = "";
                WerknemerOpzoekenFilterTextBoxVoorNaam.Text = "";
                WerknemerOpzoekenFilterTextBoxBedrijfId.Text = "";
                WerknemerOpzoekenFilterTextBoxEmail.Text = "";
                WerknemerOpzoekenFilterTextBoxFunctie.Text = "";
            }
            else MessageBox.Show("Je mag maximum en minimum 1 wernemer verwijderen");
        }
        private void WerknemerOpvragenAanpassenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewWerknemerOpzoeken.SelectedItems.Count == 1)
            {
                Werknemer werknemer = (Werknemer)ListviewWerknemerOpzoeken.SelectedItem;
                string? naam = null;
                string? voornaam = null;
                string? email = null;
                string message = "";
                if ((!string.IsNullOrWhiteSpace(WerknemerOpzoekenTextBoxNaam.Text)) && (WerknemerOpzoekenTextBoxNaam.Text != werknemer.Naam)) { naam = WerknemerOpzoekenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                if ((!string.IsNullOrWhiteSpace(WerknemerOpzoekenTextBoxVoornaam.Text)) && (WerknemerOpzoekenTextBoxVoornaam.Text != werknemer.Voornaam)) { voornaam = WerknemerOpzoekenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                if ((!string.IsNullOrWhiteSpace(WerknemerOpzoekenTextBoxEmail.Text)) && (WerknemerOpzoekenTextBoxEmail.Text != werknemer.Email)) { email = WerknemerOpzoekenTextBoxEmail.Text; message += $"email => {email}\n"; }
                if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                else MessageBox.Show(message);
            }
            else MessageBox.Show("Je mag maximum en minimum 1 werknemer in \nde lijst tegelijker tijd aanpassen");
        }
        #endregion
        //Alle Buttons voor bezoeker(toevoegen, aanpassen, verwijderen en filter)
        #region bezoeker buttons
        private void BezoekerToevoegenToevoegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BezoekerToevoegenToevoegBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? email = null;
                    string? bedrijfnaam = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(BezoekerToevoegenTextBoxNaam.Text)) { naam = BezoekerToevoegenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekerToevoegenTextBoxVoornaam.Text)) { voornaam = BezoekerToevoegenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekerToevoegenTextBoxEmail.Text)) { email = BezoekerToevoegenTextBoxEmail.Text; message += $"email => {email}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekerToevoegenTextBoxBedrijfNaam.Text)) { bedrijfnaam = BezoekerToevoegenTextBoxBedrijfNaam.Text; message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                    if (naam == null || voornaam == null || email == null || bedrijfnaam == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else MessageBox.Show(message);
                }
            }
        }
        private void BezoekerOpvragenFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BezoekerOpvragenFilterBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? persoonId = null;
                    string? email = null;
                    string? bedrijfnaam = null;
                    if (!string.IsNullOrWhiteSpace(BezoekerOpzoekenFilterTextBoxNaam.Text)) { naam = BezoekerOpzoekenFilterTextBoxNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekerOpzoekenFilterTextBoxVoorNaam.Text)) { voornaam = BezoekerOpzoekenFilterTextBoxVoorNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekerOpzoekenFilterTextBoxPersoonId.Text)) { persoonId = BezoekerOpzoekenFilterTextBoxPersoonId.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekerOpzoekenFilterTextBoxEmail.Text)) { email = BezoekerOpzoekenFilterTextBoxEmail.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekerOpzoekenFilterTextBoxBedrijfNaam.Text)) { bedrijfnaam = BezoekerOpzoekenFilterTextBoxBedrijfNaam.Text; }
                    MessageBox.Show($"persoonId => {persoonId}\nnaam => {naam}\nvoornaam => {voornaam}\nemail => {email}\nbedrijfnaam => {bedrijfnaam}");
                    bezoekers.Clear();
                    bezoekers.Add(new Bezoeker(1, "Geeroms", "Jantje", "Jantje.Geeroms@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(2, "Stanton", "Rumaysa", "Rumaysa.Stanton@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(3, "Sims", "Eben", "Eben.Sims@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(4, "Pena", "Hannah", "Hannah.Pena@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(5, "Wall", "Eboni", "Eboni.Wall@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(6, "Rasmussen", "Zayan", "Zayan.Rasmussen@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(7, "Hope", "Adaline", "Adaline.Hope@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(8, "Knights", "Zeeshan", "Zeeshan.Knights@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(9, "Cooley", "Kayleigh", "Kayleigh.Cooley@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(10, "Baldwin", "David", "David.Baldwin@hotmail.com", "allphi"));
                }
            }
        }
        private void BezoekerOpvragenVerwijderenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBezoekerOpzoeken.SelectedItems.Count == 1)
            {
                Bezoeker bezoeker = (Bezoeker)ListviewBezoekerOpzoeken.SelectedItem;
                MessageBox.Show($"bezoekerId => {bezoeker.PersoonId}");
                bezoekers.Clear();
                BezoekerOpzoekenTextBoxNaam.Text = "";
                BezoekerOpzoekenTextBoxVoornaam.Text = "";
                BezoekerOpzoekenTextBoxEmail.Text = "";
                BezoekerOpzoekenTextBoxBedrijfNaam.Text = "";
                BezoekerOpzoekenFilterTextBoxNaam.Text = "";
                BezoekerOpzoekenFilterTextBoxVoorNaam.Text = "";
                BezoekerOpzoekenFilterTextBoxPersoonId.Text = "";
                BezoekerOpzoekenFilterTextBoxEmail.Text = "";
                BezoekerOpzoekenFilterTextBoxBedrijfNaam.Text = "";
            }
            else MessageBox.Show("Je mag maximum en minimum 1 wernemer verwijderen");
        }
        private void BezoekerOpvragenAanpassenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBezoekerOpzoeken.SelectedItems.Count == 1)
            {
                Bezoeker bezoeker = (Bezoeker)ListviewBezoekerOpzoeken.SelectedItem;
                string? naam = null;
                string? voornaam = null;
                string? email = null;
                string? bedrijfnaam = null;
                string message = "";
                if ((!string.IsNullOrWhiteSpace(BezoekerOpzoekenTextBoxNaam.Text)) && (BezoekerOpzoekenTextBoxNaam.Text != bezoeker.Naam)) { naam = BezoekerOpzoekenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekerOpzoekenTextBoxVoornaam.Text)) && (BezoekerOpzoekenTextBoxVoornaam.Text != bezoeker.Voornaam)) { voornaam = BezoekerOpzoekenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekerOpzoekenTextBoxEmail.Text)) && (BezoekerOpzoekenTextBoxEmail.Text != bezoeker.Email)) { email = BezoekerOpzoekenTextBoxEmail.Text; message += $"email => {email}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekerOpzoekenTextBoxBedrijfNaam.Text)) && (BezoekerOpzoekenTextBoxBedrijfNaam.Text != bezoeker.Bedrijf)) { bedrijfnaam = BezoekerOpzoekenTextBoxBedrijfNaam.Text; message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                else MessageBox.Show(message);
            }
            else MessageBox.Show("Je mag maximum en minimum 1 bezoeker in \nde lijst tegelijker tijd aanpassen");
        }
        #endregion
        //Alle Buttons voor bezoek(toevoegen, aanpassen, verwijderen en filter)
        #region bezoek buttons
        private void BezoekToevoegenToevoegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BezoekToevoegenToevoegBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? email = null;
                    string? bedrijfnaamBezoeker = null;
                    string? bedrijf = null;
                    string? werknemer = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(BezoekToevoegenTextBoxNaam.Text)) { naam = BezoekToevoegenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekToevoegenTextBoxVoornaam.Text)) { voornaam = BezoekToevoegenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekToevoegenTextBoxEmail.Text)) { email = BezoekToevoegenTextBoxEmail.Text; message += $"email => {email}\n"; }
                    if (!string.IsNullOrWhiteSpace(BezoekToevoegenTextBoxBedrijfNaamBezoeker.Text)) { bedrijfnaamBezoeker = BezoekToevoegenTextBoxBedrijfNaamBezoeker.Text; message += $"bedrijfnaamBezoeker => {bedrijfnaamBezoeker}\n"; }
                    if (BezoekToevoegenComboBoxBedrijfNaam.SelectedItem != null) { bedrijf = BezoekToevoegenComboBoxBedrijfNaam.SelectedItem.ToString(); message += $"bedrijf => {bedrijf}\n"; }
                    if (BezoekToevoegenComboBoxContactPersoon.SelectedItem != null) { werknemer = BezoekToevoegenComboBoxContactPersoon.SelectedItem.ToString(); message += $"werknemer => {werknemer}\n"; }
                    if (naam == null || voornaam == null || email == null || bedrijfnaamBezoeker == null || bedrijf == null || werknemer == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else MessageBox.Show(message);
                }
            }
        }
        private void BezoekOpvragenFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BezoekOpvragenFilterBtn")
                {
                    string? naam = null;
                    string? voornaam = null;
                    string? bedrijfnaam = null;
                    string? email = null;
                    string? PersoonId = null;
                    if (!string.IsNullOrWhiteSpace(BezoekOpzoekenFilterTextBoxNaam.Text)) { naam = BezoekOpzoekenFilterTextBoxNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekOpzoekenFilterTextBoxVoorNaam.Text)) { voornaam = BezoekOpzoekenFilterTextBoxVoorNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekOpzoekenFilterTextBoxBedrijfNaam.Text)) { bedrijfnaam = BezoekOpzoekenFilterTextBoxBedrijfNaam.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekOpzoekenFilterTextBoxEmail.Text)) { email = BezoekOpzoekenFilterTextBoxEmail.Text; }
                    if (!string.IsNullOrWhiteSpace(BezoekOpzoekenFilterTextBoxPersoonId.Text)) { PersoonId = BezoekOpzoekenFilterTextBoxPersoonId.Text; }
                    MessageBox.Show($"naam => {naam}\nvoornaam => {voornaam}\nbedrijfId => {bedrijfnaam}\nemail => {email}\nPersoonId => {PersoonId}");
                    bezoeken.Clear();
                    Bedrijf bedrijf1 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
                    bedrijf1.ZetAdres(new Adres(1, "Bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
                    bedrijf1.ZetTelefoon("0491732014");
                    Bezoek bezoek1 = new Bezoek(1, new Bezoeker(1, "Geeroms", "Jantje", "Jantje.Geeroms@hotmail.com", "allphi"), bedrijf1, new Werknemer(2, "Janssen", "Jan"), new DateTime(2022, 10, 24), new DateTime(2022, 10, 24));
                    bezoeken.Add(bezoek1);
                    Bedrijf bedrijf2 = new Bedrijf(2, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf2.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf2.ZetTelefoon("0491732123");
                    Bezoek bezoek2 = new Bezoek(2, new Bezoeker(2, "Stanton", "Rumaysa", "Rumaysa.Stanton@hotmail.com", "allphi"), bedrijf2, new Werknemer(6, "Jonssen", "Robrecht"), new DateTime(2022, 10, 20), new DateTime(2022, 10, 20));
                    bezoeken.Add(bezoek2);
                }
            }
        }
        private void BezoekOpzoekenVerwijderenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBezoekOpzoeken.SelectedItems.Count == 1)
            {
                Bezoek bezoek = (Bezoek)ListviewBezoekOpzoeken.SelectedItem;
                MessageBox.Show($"bezoekId => {bezoek.BezoekId}");
                bezoeken.Clear();
                BezoekOpzoekenTextBoxEmail.Text = "";
                BezoekOpzoekenTextBoxNaam.Text = "";
                BezoekOpzoekenTextBoxVoornaam.Text = "";
                BezoekOpzoekenTextBoxBedrijfNaamBezoeker.Text = "";
                BezoekOpzoekenFilterTextBoxNaam.Text = "";
                BezoekOpzoekenFilterTextBoxVoorNaam.Text = "";
                BezoekOpzoekenFilterTextBoxPersoonId.Text = "";
                BezoekOpzoekenFilterTextBoxBedrijfNaam.Text = "";
                BezoekOpzoekenFilterTextBoxEmail.Text = "";
                BezoekOpzoekenComboBoxBedrijfNaam.SelectedValue = "";
                BezoekOpzoekenComboBoxContactPersoon.SelectedValue = "";
            }
            else MessageBox.Show("Je mag maximum en minimum 1 wernemer verwijderen");
        }
        private void BezoekOpzoekenAanpassenBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ListviewBezoekOpzoeken.SelectedItems.Count == 1)
            {
                Bezoek bezoek = (Bezoek)ListviewBezoekOpzoeken.SelectedItem;
                string? naam = null;
                string? voornaam = null;
                string? email = null;
                string? bedrijfnaam = null;
                string? bedrijf = null;
                string? werknemer = null;
                string message = "";
                if ((!string.IsNullOrWhiteSpace(BezoekOpzoekenTextBoxNaam.Text)) && (BezoekOpzoekenTextBoxNaam.Text != bezoek.Bezoeker.Naam)) { naam = BezoekOpzoekenTextBoxNaam.Text; message += $"naam => {naam}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekOpzoekenTextBoxVoornaam.Text)) && (BezoekOpzoekenTextBoxVoornaam.Text != bezoek.Bezoeker.Voornaam)) { voornaam = BezoekOpzoekenTextBoxVoornaam.Text; message += $"voornaam => {voornaam}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekOpzoekenTextBoxEmail.Text)) && (BezoekOpzoekenTextBoxEmail.Text != bezoek.Bezoeker.Email)) { email = BezoekOpzoekenTextBoxEmail.Text; message += $"email => {email}\n"; }
                if ((!string.IsNullOrWhiteSpace(BezoekOpzoekenTextBoxBedrijfNaamBezoeker.Text)) && (BezoekOpzoekenTextBoxBedrijfNaamBezoeker.Text != bezoek.Bezoeker.Bedrijf)) { bedrijfnaam = BezoekOpzoekenTextBoxBedrijfNaamBezoeker.Text; message += $"bedrijfnaam => {bedrijfnaam}\n"; }
                if ((BezoekOpzoekenComboBoxBedrijfNaam.SelectedItem != null) && (BezoekOpzoekenComboBoxBedrijfNaam.SelectedItem.ToString() != bezoek.Bedrijf.Naam)) { bedrijf = BezoekOpzoekenComboBoxBedrijfNaam.SelectedItem.ToString(); message += $"bedrijf => {bedrijf}\n"; }
                if ((BezoekOpzoekenComboBoxContactPersoon.SelectedItem != null) && (BezoekOpzoekenComboBoxContactPersoon.SelectedItem.ToString() != $"{bezoek.Contactpersoon.Naam}, {bezoek.Contactpersoon.Voornaam}")) { werknemer = BezoekOpzoekenComboBoxContactPersoon.SelectedItem.ToString(); message += $"werknemer => {werknemer}\n"; }
                if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                else MessageBox.Show(message);
            }
            else MessageBox.Show("Je mag maximum en minimum 1 bezoeker in \nde lijst tegelijker tijd aanpassen");
        }
        #endregion

        private void Testinghere(string? land, string? straat, string? nummer, string? postcode, string? plaats)
        {
            string quary = "update Adres set ";
            if (!string.IsNullOrWhiteSpace(land)) quary += "Land = @Land";
            if (!string.IsNullOrWhiteSpace(straat) && quary != "update Adres set ") quary += " AND ";
            if (!string.IsNullOrWhiteSpace(straat)) quary += "Straat = @Straat";
            if (!string.IsNullOrWhiteSpace(nummer) && quary != "update Adres set ") quary += " AND ";
            if (!string.IsNullOrWhiteSpace(nummer)) quary += "Nummer = @Nummer";
            if (!string.IsNullOrWhiteSpace(postcode) && quary != "update Adres set ") quary += " AND ";
            if (!string.IsNullOrWhiteSpace(postcode)) quary += "Postcode = @Postcode";
            if (!string.IsNullOrWhiteSpace(plaats) && quary != "update Adres set ") quary += " AND ";
            if (!string.IsNullOrWhiteSpace(plaats)) quary += "Plaats = @Plaats";
            MessageBox.Show(quary);
        }
    }
}
