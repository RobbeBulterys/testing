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

        private static IVisitorRepository _visitorRepo = new VisitorRepoADO(connectionstring);
        private VisitorManager _visitorManager = new VisitorManager(_visitorRepo);


        private static IEmployeecontractRepository contractRepo = new EmployeecontractRepoADO(connectionstring);
        private static EmployeecontractManager _contractManager = new EmployeecontractManager(contractRepo);

        private static IVisitRepository _visitRepo = new VisitRepoADO(connectionstring);
        private VisitManager _visitManager = new VisitManager(_visitRepo);

        private static IEmployeeRepository _employeeRepo = new EmployeeRepoADO(connectionstring);
        private EmployeeManager _employeeManager = new EmployeeManager(_employeeRepo);

        private static ICompanyRepository _companyRepo = new CompanyRepoADO(connectionstring);
        private CompanyManager _companyManager = new CompanyManager(_companyRepo, _adresManager, _contractManager);

        private static IAddressRepository adresRepo = new AddressRepoADO(connectionstring);
        private static AddressManager _adresManager = new AddressManager(adresRepo, _companyRepo);

        static private Company _company;
        static private Employee _employee;
        
        static private Visit _visit;
        static private Visitor _visitor;
        static private Employeecontract _employeecontract;


        public MainView()
        {
            InitializeComponent();

            ComboBoxBedrijf.ItemsSource = _companyManager.GetCompanies();       
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
            _company = ComboBoxBedrijf.SelectedItem as Company;
            if (ComboBoxBedrijf.SelectedIndex != -1)
            {
                ComboBoxContactpersoon.ItemsSource = _contractManager.GetCompanyContracts(_company);
            }       
        }

        private void ComboBoxContactPersoon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _employeecontract = ComboBoxContactpersoon.SelectedItem as Employeecontract;
        }

        private void LoginButtonEffect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //TODO: Exceptionhandling WPF
                LoginCheck();
                _visitor = new Visitor(TextBoxNaam.Text, TextBoxVoornaam.Text, TextBoxEmailLogin.Text, TextBoxBedrijfVanBezoeker.Text);
                _visitorManager.AddVisitor(_visitor);
                _visitor = _visitorManager.SearchVisitors(_visitor.LastName, _visitor.FirstName, _visitor.Email, _visitor.Company).ToList()[0];
                _visitManager.AlreadyLoggedIn(_visitor.Email);
                _visit = new Visit(_visitor, _company, _employeecontract.Employee, DateTime.Now);
                if (MessageBox.Show($"Zijn deze gegevens correct? \nNaam: {TextBoxNaam.Text} {TextBoxVoornaam.Text} \nEmail: {TextBoxEmailLogin.Text} \nBedrijf van afkomst:{TextBoxBedrijfVanBezoeker.Text} \nBedrijf met afspraak:{_company.Name} \nContactpersoon: {_employeecontract.Employee.LastName} {_employeecontract.Employee.FirstName}", "Controle", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _visitManager.AddVisit(_visit);
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
                _visitManager.LogoutVisit(TextBoxEmailLogout.Text);
                MessageBox.Show("Succesvol Afgemeld");
                TextBoxEmailLogout.Clear();
            }
            catch (Exception ex)
            {
                if (ex.Message.Equals("Is not logged in"))
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
            _employee = null;
            _company = null;
        }

    }
}
