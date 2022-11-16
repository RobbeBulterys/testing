using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using DL_Projectwerk.Exceptions;
using DL_Projectwerk.repoADO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
using System.Xml;

namespace UIBezoeker
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {


        //private static string connectionstringAzure = "Server=tcp:bezoekerregistratiesysteem.database.windows.net,1433;Initial Catalog=bezoekerregistratiesysteemdb;Persist Security Info=False;User ID=Hackerman;Password=RootRoot!69;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        private static string connectionstring = "Server=ID367284_VRS.db.webhosting.be;User ID=ID367284_VRS;Password=RootRoot!69;Database=ID367284_VRS";

        private static IBezoekerRepository bezoekerRepo = new BezoekerRepoADO(connectionstring);
        private BezoekerManager _bezoekerManager = new BezoekerManager(bezoekerRepo);

        private static IAdresRepository adresRepo = new AdresRepoADO(connectionstring);
        private static AdresManager _adresManager = new AdresManager(adresRepo, bedrijfRepo);


        private static IWerknemercontractRepository contractRepo = new WerknemercontractRepoADO(connectionstring);
        private static WerknemercontractManager _contractManager = new WerknemercontractManager(contractRepo);

        private static IBezoekRepository bezoekRepo = new BezoekRepoADO(connectionstring);
        private BezoekManager _bezoekManager = new BezoekManager(bezoekRepo);

        private static IWerknemerRepository werknemerRepo = new WerknemerRepoADO(connectionstring);
        private WerknemerManager _werknemerRepo = new WerknemerManager(werknemerRepo);

        private static IBedrijfRepository bedrijfRepo = new BedrijfRepoADO(connectionstring);
        private BedrijfManager _bedrijfManager = new BedrijfManager(bedrijfRepo, _adresManager, _contractManager);

        static private Bedrijf _bedrijf;
        static private Werknemer _werknemer;
        
        static private Bezoek _bezoek;
        static private Bezoeker _bezoeker;
        static private Werknemercontract _werknemercontract;


        public MainView()
        {
            InitializeComponent();

            ComboBoxBedrijf.ItemsSource = _bedrijfManager.GeefBedrijven();
            
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#85C4FF");
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            LoginBorder.Visibility = Visibility.Visible;
            LogoutBorder.Visibility = Visibility.Collapsed;
            Titel.Text = "Aanmelden";
            ClearTextBoxLogin();
        }

        private void Logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#85C4FF");
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");

            LoginBorder.Visibility = Visibility.Collapsed;
            LogoutBorder.Visibility = Visibility.Visible;
            Titel.Text = "Afmelden";
            ClearTextBoxLogin();
        }

        private void Home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            LoginBorder.Visibility = Visibility.Collapsed;
            LogoutBorder.Visibility= Visibility.Collapsed;
            ClearTextBoxLogin();
            Titel.Text = "Welkom op het bedrijvenpark";
        }

        private void ComboBoxBedrijf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _bedrijf = ComboBoxBedrijf.SelectedItem as Bedrijf;

            if (ComboBoxBedrijf.SelectedIndex != -1)
            {
                ComboBoxContactpersoon.ItemsSource = _contractManager.GeefContractenVanBedrijf(_bedrijf);
            }
            
        }

        private void ComboBoxContactPersoon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            _werknemercontract = ComboBoxContactpersoon.SelectedItem as Werknemercontract;

        }

        private void LoginButtonEffect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //TODO: Exceptionhandling WPF
                LoginCheck();
                _bezoeker = new Bezoeker(TextBoxNaam.Text, TextBoxVoornaam.Text, TextBoxEmailLogin.Text, TextBoxBedrijfVanBezoeker.Text);
                if (_bezoekManager.IsLoggedIn(_bezoeker.Email)) throw new Exception("Bezoeker is reeds ingelogd");
                _bezoekerManager.VoegBezoekerToe(_bezoeker);
                _bezoeker = _bezoekerManager.ZoekBezoekers(_bezoeker.Naam, _bezoeker.Voornaam, _bezoeker.Email, _bezoeker.Bedrijf).ToList()[0];
                _bezoek = new Bezoek(_bezoeker, _bedrijf, _werknemercontract.Werknemer, DateTime.Now);
                if (MessageBox.Show($"Zijn deze gegevens correct? \nNaam: {TextBoxNaam.Text} {TextBoxVoornaam.Text} \nEmail: {TextBoxEmailLogin.Text} \nBedrijf van afkomst:{TextBoxBedrijfVanBezoeker.Text} \nBedrijf met afspraak:{_bedrijf.Naam} \nContactpersoon: {_werknemercontract.Werknemer.Naam} {_werknemercontract.Werknemer.Voornaam}", "Controle", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _bezoekManager.VoegBezoekToe(_bezoek);
                    ClearTextBoxLogin();
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Bezoeker is reeds ingelogd"))
                {
                    MessageBox.Show("U bent al reeds aangemeld, gelieve u eerst af te melden");
                    Logout_MouseLeftButtonDown(sender, e);
                }
                else
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        private void LogoutButtonEffect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _bezoekManager.LogoutBezoek(TextBoxEmailLogout.Text);
                MessageBox.Show("Succesvol Afgemeld");
                TextBoxEmailLogout.Clear();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("not logged in"))
                {
                    MessageBox.Show("U bent nog niet aangemeld, gelieve u eerst aan te melden");
                    Login_MouseLeftButtonDown(sender, e);
                } else
                {
                    MessageBox.Show(ex.Message);
                }            
            }
        }

        private void LoginCheck()
        {
            if (TextBoxVoornaam.Text == "") throw new Exception("Geen voornaam ingevuld");
            if (TextBoxNaam.Text == "") throw new Exception("Geen naam ingevuld");
            if (TextBoxEmailLogin.Text == "") throw new Exception("Geen Email ingevuld");
            if (ComboBoxBedrijf.SelectedIndex == -1) throw new Exception("Geen bedrijf geselecteerd");
            if (ComboBoxContactpersoon.SelectedIndex == -1) throw new Exception("Geen contactpersoon geselecteerd");
        }

        private void ClearTextBoxLogin()
        {
            TextBoxNaam.Clear();
            TextBoxVoornaam.Clear();
            TextBoxEmailLogin.Clear();
            TextBoxEmailLogout.Clear();
            TextBoxBedrijfVanBezoeker.Clear();
            ComboBoxContactpersoon.ItemsSource = null;
            ComboBoxBedrijf.SelectedIndex = -1;
            _werknemer = null;
            _bedrijf = null;
        }

    }
}
