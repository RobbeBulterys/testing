using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
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


        private static string connectionstring = "Server=tcp:bezoekerregistratiesysteem.database.windows.net,1433;Initial Catalog=bezoekerregistratiesysteemdb;Persist Security Info=False;User ID=Hackerman;Password=RootRoot!69;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

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
            //ComboBoxBedrijf.ItemsSource = bedrijven;
            IReadOnlyList<Bedrijf> bedrijven = _bedrijfManager.GeefBedrijven();

            IEnumerable<Bedrijf> bedrijfEnum = _bedrijfManager.GeefBedrijven();

            ComboBoxBedrijf.ItemsSource = bedrijfEnum;
            
        }

        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#85C4FF");
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            LoginBorder.Visibility = Visibility.Visible;
            LogoutBorder.Visibility = Visibility.Collapsed;
            Titel.Text = "Aanmelden";
        }

        private void Logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#85C4FF");
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");

            LoginBorder.Visibility = Visibility.Collapsed;
            LogoutBorder.Visibility = Visibility.Visible;
            Titel.Text = "Afmelden";

        }

        private void Home_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#002242");
            LoginBorder.Visibility = Visibility.Collapsed;
            LogoutBorder.Visibility= Visibility.Collapsed;
            ClearTextBoxLogin();
            ClearTextBoxLogout();
            Titel.Text = "Welkom op het bedrijvenpark";

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton==MouseButtonState.Pressed)
            {           
                DragMove();
            }
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
        private void ClearTextBoxLogout()
        {

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

        private void LoginButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void LoginButtonEffect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TextBoxNaam.Text == "" || TextBoxVoornaam.Text == "" || TextBoxEmailLogin.Text == "" || TextBoxBedrijfVanBezoeker.Text == "" || ComboBoxBedrijf.SelectedIndex == -1 || ComboBoxContactpersoon.SelectedIndex == -1)
                {
                    MessageBox.Show("Melding: U heeft niet alle velden ingevuld");
                }
                else
                {
                    _werknemer = (Werknemer)_werknemercontract.Werknemer;
                    _bezoeker = new Bezoeker(TextBoxNaam.Text, TextBoxVoornaam.Text, TextBoxEmailLogin.Text, TextBoxBedrijfVanBezoeker.Text);
                    if (!_bezoekerManager.BestaatBezoeker(_bezoeker))
                    {
                        _bezoekerManager.VoegBezoekerToe(_bezoeker);
                    }
                    else
                    {
                        List<Bezoeker> bezoekers = _bezoekerManager.ZoekBezoekers(_bezoeker.Naam, _bezoeker.Voornaam, _bezoeker.Email, _bezoeker.Bedrijf).ToList();
                        _bezoeker = bezoekers[0];
                    }
                        

                        
                    
                    if (MessageBox.Show($"Zijn deze gegevens correct: \nNaam: {_bezoeker.Naam} {_bezoeker.Voornaam} \nEmail: {_bezoeker.Email} \nBedrijf van afkomst:{_bezoeker.Bedrijf} \nBedrijf met afspraak:{_bedrijf.Naam} \nContactpersoon: {_werknemer.Naam} {_werknemer.Voornaam}","Controle",MessageBoxButton.YesNo) == MessageBoxResult.Yes) {
                        _bezoek = new Bezoek(_bezoeker, _bedrijf, _werknemer, DateTime.Now);
                        _bezoekManager.VoegBezoekToe(_bezoek);
                        ClearTextBoxLogin();
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception("Fout Toevoegen", ex);
            }
        }

        private void LogoutButtonEffect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string email = TextBoxEmailLogout.Text;

            

            if (MessageBox.Show($"Zeker dat u zich wilt afmelden?", "Controle", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _bezoekManager.LogoutBezoek(email);
                TextBoxEmailLogout.Clear();
            }
        }
    }
}
