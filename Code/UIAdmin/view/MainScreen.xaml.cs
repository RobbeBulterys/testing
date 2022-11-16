using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
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
using UIAdmin.Domein;

namespace UIAdmin.view
{
    public partial class MainScreen : Window
    {
        private bool _IsInitialized = false;
        private bool _isMaximized = false;
        private AdresManager _adresManager;
        private BedrijfManager _companyManager;
        private BezoekerManager _visitorManager;
        private WerknemerManager _employeeManager;
        private BezoekManager _visitManager;
        private WerknemercontractManager _employeecontractManager;
        private List<Werknemer> employees = new List<Werknemer>();
        private List<Bedrijf> companys = new List<Bedrijf>();
        private List<Bezoeker> visitors = new List<Bezoeker>();
        private List<Bezoek> visits = new List<Bezoek>();
        private List<Bezoek> allVisits = new List<Bezoek>();
        private ObservableCollection<Bedrijf> companysCollection = new ObservableCollection<Bedrijf>();
        private ObservableCollection<Bezoeker> visitorsCollection = new ObservableCollection<Bezoeker>();
        private ObservableCollection<Werknemer> employeesCollection = new ObservableCollection<Werknemer>();
        private ObservableCollection<BezoekAdmin> visitsAdminCollection = new ObservableCollection<BezoekAdmin>();
        public MainScreen(AdresManager adresManager, BedrijfManager companyManager, BezoekerManager visitorManager, WerknemerManager employeeManager, WerknemercontractManager employeecontract, BezoekManager visitManager)
        {
            InitializeComponent();
            _IsInitialized = true;
            HomeBtn.IsChecked = true;
            CompanyDataGrid.ItemsSource = companysCollection;
            VisitorDataGrid.ItemsSource = visitorsCollection;
            EmployeeDataGrid.ItemsSource = employeesCollection;
            VisitDataGrid.ItemsSource = visitsAdminCollection;
            _adresManager = adresManager;
            _companyManager = companyManager;
            _visitorManager = visitorManager;
            _employeeManager = employeeManager;
            _visitManager = visitManager;
            _employeecontractManager = employeecontract;
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
        private void MenuButtonsBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                CompanyBorder.Visibility = Visibility.Collapsed;
                VisitorBorder.Visibility = Visibility.Collapsed;
                EmployeeBorder.Visibility = Visibility.Collapsed;
                VisitBorder.Visibility = Visibility.Collapsed;
                RadioButton button = (RadioButton)sender;
                if (button.Name == "CompanyBtn")
                {
                    CompanyBorder.Visibility = Visibility.Visible;
                    companysCollection.Clear();
                    companys = _companyManager.GeefBedrijven().ToList();
                    CompanysShowList();
                }
                if (button.Name == "VisitorBtn")
                {
                    VisitorBorder.Visibility = Visibility.Visible;
                    visitorsCollection.Clear();
                    visitors.Clear();
                    visitors = _visitorManager.GeefBezoekers().ToList();
                    VisitorsShowList();
                }
                if (button.Name == "EmployeeBtn")
                {
                    EmployeeBorder.Visibility = Visibility.Visible;
                    employeesCollection.Clear();
                    employees.Clear();
                    employees = _employeeManager.GeefAlleWerknemers().ToList();
                    EmployeesShowList();
                }
                if (button.Name == "VisitBtn")
                {
                    VisitBorder.Visibility = Visibility.Visible;
                    visitsAdminCollection.Clear();
                    visits.Clear();
                    allVisits.Clear();
                    allVisits = _visitManager.GeefBezoeken().ToList();
                    visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList();
                    VisitsShowList();
                }
            }
        }
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region Bedrijf
        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            AddBedrijfScreen screen = new AddBedrijfScreen(_adresManager, _companyManager);
            screen.Show();
        }
        private void CompanyGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (CompanyDataGrid.SelectedItem.GetType() == typeof(Bedrijf))
                {
                    Bedrijf b = (Bedrijf)CompanyDataGrid.SelectedItem;
                    if (button.Name == "CompanyEditButton1") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, b, "Company"); screen.Show(); }
                    if (button.Name == "CompanyEditButton2") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, b, "Workers"); screen.Show(); }
                    if (button.Name == "CompanyEditButton3") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, b, "Visits"); screen.Show(); }
                }
            }
        }
        private void CompanyGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyDataGrid.SelectedItem.GetType() == typeof(Bedrijf))
            {
                Bedrijf b = (Bedrijf)CompanyDataGrid.SelectedItem;
                if (b.Adres != null) _companyManager.VerwijderBedrijf(b, b.Adres.Id);
                else _companyManager.VerwijderBedrijf(b, null);
                companysCollection.Clear();
            }
        }
        private void CompanySearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                CompanySearchBorder.Width = 350;
                if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    CompanySearchGrid2.Width = new GridLength(0);
                    CompanySearchBorder.Width = 55;
                    companysCollection.Clear();
                    companys = _companyManager.GeefBedrijven().ToList();
                    CompanysShowList();
                }
                else
                {
                    if (CompanySearchGrid2.Width == new GridLength(0)) { CompanySearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
            }
        }
        private void CompanySearchBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            companysCollection.Clear();
            companys.Clear();
            if (!string.IsNullOrWhiteSpace(CompanySearchBox.Text))
            {
                try
                {
                    if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All") { companys = _companyManager.ZoekBedrijven(null, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Bedrijfsnaam") { companys = _companyManager.ZoekBedrijven(null, CompanySearchBox.Text, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: BTW-nummer") { companys = _companyManager.ZoekBedrijven(CompanySearchBox.Text, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Telefoon") { companys = _companyManager.ZoekBedrijven(null, null, null, CompanySearchBox.Text).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Email") { companys = _companyManager.ZoekBedrijven(null, null, CompanySearchBox.Text, null).ToList(); }
                    CompanysShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { companys = _companyManager.ZoekBedrijven(null, null, null, null).ToList(); CompanysShowList(); }
        }
        private void CompanysShowList()
        {
            CompanyDataGridComboBox.Items.Clear();
            CompanyDataGridNextPage.Visibility = Visibility.Collapsed;
            CompanyDataGridPreviewPage.Visibility = Visibility.Collapsed;
            int numberPerPage = int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (companys.Count > numberPerPage)
            {
                int totalPages = (companys.Count + (numberPerPage - (companys.Count % numberPerPage))) / numberPerPage;
                for (int i = 1; i <= totalPages; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = i;
                    if (i == 1) { item.IsSelected = true; }
                    CompanyDataGridComboBox.Items.Add(item);
                }
                CompanyDataGridNextPage.Visibility = Visibility.Visible;
            }
            else
            {
                companys.ForEach(c => companysCollection.Add(c));
            }
        }
        private void CompanyItemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                companysCollection.Clear();
                CompanysShowList();
            }
        }
        private void CompanyDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && CompanyDataGridComboBox.SelectedIndex != -1)
            {
                companysCollection.Clear();
                int numberPerPage = int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = CompanyDataGridComboBox.SelectedIndex * numberPerPage;
                CompanyDataGridNextPage.Visibility = Visibility.Visible;
                CompanyDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { CompanyDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (companys.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < companys.Count; i++)
                    {
                        companysCollection.Add(companys[i]);
                    }
                    CompanyDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        companysCollection.Add(companys[i]);
                    }
                }
            }
        }
        private void CompanyDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (companysCollection.Count == int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                CompanyDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void CompanyDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (companysCollection.Count != 0)
            {
                CompanyDataGridComboBox.SelectedIndex -= 1;
            }
        }
        #endregion
        #region Bezoeker
        private void AddVisitor_Click(object sender, RoutedEventArgs e)
        {
            AddBezoekerScreen screen = new AddBezoekerScreen(_visitorManager);
            screen.Show();
        }
        private void VisitorGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorDataGrid.SelectedItem.GetType() == typeof(Bezoeker))
            {
                Bezoeker b = (Bezoeker)VisitorDataGrid.SelectedItem;
                EditBezoekerScreen screen = new EditBezoekerScreen(_visitorManager, _visitManager, b);
                screen.Show();
            }
        }
        private void VisitorGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorDataGrid.SelectedItem.GetType() == typeof(Bezoeker))
            {
                Bezoeker b = (Bezoeker)VisitorDataGrid.SelectedItem;
                _visitorManager.VerwijderBezoeker(b);
                visitorsCollection.Clear();
            }
        }
        private void VisitorSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                VisitorSearchBox1.Text = "";
                VisitorSearchBox2.Text = "";
                VisitorSearchGrid2.Width = new GridLength(0);
                VisitorSearchGrid3.Width = new GridLength(0);
                if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    VisitorSearchGrid2.Width = new GridLength(0);
                    VisitorSearchGrid3.Width = new GridLength(0);
                    VisitorSearchBorder.Width = 55;
                    visitorsCollection.Clear();
                    visitors.Clear();
                    visitors = _visitorManager.GeefBezoekers().ToList();
                    VisitorsShowList();
                }
                else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam en Voornaam")
                {
                    VisitorSearchBorder.Width = 500;
                    if (VisitorSearchGrid2.Width == new GridLength(0)) { VisitorSearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                    if (VisitorSearchGrid3.Width == new GridLength(0)) { VisitorSearchGrid3.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
                else
                {
                    VisitorSearchBorder.Width = 250;
                    if (VisitorSearchGrid2.Width == new GridLength(0)) { VisitorSearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
            }
        }
        private void VisitorSearchBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VisitorSearchBox1.Text) || !string.IsNullOrWhiteSpace(VisitorSearchBox2.Text))
            {
                visitorsCollection.Clear();
                visitors.Clear();
                try
                {
                    if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All") { visitors = _visitorManager.GeefBezoekers().ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam en Voornaam") { visitors = _visitorManager.ZoekBezoekers(VisitorSearchBox1.Text, VisitorSearchBox2.Text, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam") { visitors = _visitorManager.ZoekBezoekers(VisitorSearchBox1.Text, null, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Voornaam") { visitors = _visitorManager.ZoekBezoekers(null, VisitorSearchBox1.Text, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Bedrijfsnaam") { visitors = _visitorManager.ZoekBezoekers(null, null, null, VisitorSearchBox1.Text).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Email") { visitors = _visitorManager.ZoekBezoekers(null, null, VisitorSearchBox1.Text, null).ToList(); }
                    VisitorsShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { visitors = _visitorManager.GeefBezoekers().ToList(); VisitorsShowList(); }
        }
        private void VisitorsShowList()
        {
            visitorsCollection.Clear();
            VisitorDataGridComboBox.Items.Clear();
            VisitorDataGridNextPage.Visibility = Visibility.Collapsed;
            VisitorDataGridPreviewPage.Visibility = Visibility.Collapsed;
            int numberPerPage = int.Parse(VisitorItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (visitors.Count > numberPerPage)
            {
                int totalPages = (visitors.Count + (numberPerPage - (visitors.Count % numberPerPage))) / numberPerPage;
                for (int i = 1; i <= totalPages; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = i;
                    if (i == 1) { item.IsSelected = true; }
                    VisitorDataGridComboBox.Items.Add(item);
                }
                VisitorDataGridNextPage.Visibility = Visibility.Visible;
            }
            else
            {
                visitors.ForEach(v => visitorsCollection.Add(v));
            }
        }
        private void VisitorItemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                visitorsCollection.Clear();
                VisitorsShowList();
            }
        }
        private void VisitorDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitorDataGridComboBox.SelectedIndex != -1)
            {
                visitorsCollection.Clear();
                int numberPerPage = int.Parse(VisitorItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = VisitorDataGridComboBox.SelectedIndex * numberPerPage;
                VisitorDataGridNextPage.Visibility = Visibility.Visible;
                VisitorDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { VisitorDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (visitors.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < visitors.Count; i++)
                    {
                        visitorsCollection.Add(visitors[i]);
                    }
                    VisitorDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        visitorsCollection.Add(visitors[i]);
                    }
                }
            }
        }
        private void VisitorDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (visitorsCollection.Count == int.Parse(VisitorItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                VisitorDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void VisitorDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (visitorsCollection.Count != 0)
            {
                VisitorDataGridComboBox.SelectedIndex -= 1;
            }
        }
        #endregion
        #region Werknemer
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddWerknemerScreen screen = new AddWerknemerScreen(_employeeManager);
            screen.Show();
        }
        private void EmployeeGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                if (EmployeeDataGrid.SelectedItem.GetType() == typeof(Werknemer))
                {
                    Button button = (Button)sender;
                    Werknemer w = (Werknemer)EmployeeDataGrid.SelectedItem;
                    if (button.Name == "WorkerGridEditButton1") { EditWerknemerScreen screen = new EditWerknemerScreen(_employeeManager, _employeecontractManager, _visitManager, w, "Worker"); screen.Show(); }
                    else if (button.Name == "WorkerGridEditButton2") { EditWerknemerScreen screen = new EditWerknemerScreen(_employeeManager, _employeecontractManager, _visitManager, w, "Visits"); screen.Show(); }
                }
            }
        }
        private void EmployeeGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem.GetType() == typeof(Werknemer))
            {
                Werknemer w = (Werknemer)EmployeeDataGrid.SelectedItem;
                try
                {
                    _employeeManager.VerwijderWerknemer(w.PersoonId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"error!, {ex}");
                }
                employeesCollection.Clear();
            }
        }
        private void EmployeeSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                EployeeSearchBox1.Text = "";
                EployeeSearchBox2.Text = "";
                EmployeeSearchGrid2.Width = new GridLength(0);
                EmployeeSearchGrid3.Width = new GridLength(0);
                if (EmployeeSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    EmployeeSearchGrid2.Width = new GridLength(0);
                    EmployeeSearchGrid3.Width = new GridLength(0);
                    EmployeeSearchBorder.Width = 55;
                    employeesCollection.Clear();
                    employees.Clear();
                    employees = _employeeManager.GeefAlleWerknemers().ToList();
                    EmployeesShowList();
                }
                else if (EmployeeSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam en Voornaam")
                {
                    EmployeeSearchBorder.Width = 500;
                    if (EmployeeSearchGrid2.Width == new GridLength(0)) { EmployeeSearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                    if (EmployeeSearchGrid3.Width == new GridLength(0)) { EmployeeSearchGrid3.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
                else if (EmployeeSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam")
                {
                    EmployeeSearchBorder.Width = 250;
                    if (EmployeeSearchGrid2.Width == new GridLength(0)) { EmployeeSearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
                else if (EmployeeSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Voornaam")
                {
                    EmployeeSearchBorder.Width = 250;
                    if (EmployeeSearchGrid3.Width == new GridLength(0)) { EmployeeSearchGrid3.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
            }
        }
        private void EployeeSearchBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(EployeeSearchBox1.Text) || !string.IsNullOrWhiteSpace(EployeeSearchBox2.Text))
            {
                employeesCollection.Clear();
                employees.Clear();
                try { employees = _employeeManager.ZoekWerknemers(EployeeSearchBox1.Text, EployeeSearchBox2.Text).ToList(); EmployeesShowList(); }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { employees = _employeeManager.GeefAlleWerknemers().ToList(); EmployeesShowList(); }
        }
        private void EmployeesShowList()
        {
            employeesCollection.Clear();
            EmployeeDataGridComboBox.Items.Clear();
            EmployeeDataGridNextPage.Visibility = Visibility.Collapsed;
            EmployeeDataGridPreviewPage.Visibility = Visibility.Collapsed;
            int numberPerPage = int.Parse(EmployeeItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (employees.Count > numberPerPage)
            {
                int totalPages = (employees.Count + (numberPerPage - (employees.Count % numberPerPage))) / numberPerPage;
                for (int i = 1; i <= totalPages; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = i;
                    if (i == 1) { item.IsSelected = true; }
                    EmployeeDataGridComboBox.Items.Add(item);
                }
                EmployeeDataGridNextPage.Visibility = Visibility.Visible;
            }
            else
            {
                foreach (Werknemer werknemer in employees)
                {
                    employeesCollection.Add(werknemer);
                }
            }
        }
        private void EmployeeItemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                employeesCollection.Clear();
                EmployeesShowList();
            }
        }
        private void EmployeeDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && EmployeeDataGridComboBox.SelectedIndex != -1)
            {
                employeesCollection.Clear();
                int numberPerPage = int.Parse(EmployeeItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = EmployeeDataGridComboBox.SelectedIndex * numberPerPage;
                EmployeeDataGridNextPage.Visibility = Visibility.Visible;
                EmployeeDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { EmployeeDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (employees.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < employees.Count; i++)
                    {
                        employeesCollection.Add(employees[i]);
                    }
                    EmployeeDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        employeesCollection.Add(employees[i]);
                    }
                }
            }
        }
        private void EmployeeDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (employeesCollection.Count == int.Parse(EmployeeItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                EmployeeDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void EmployeeDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (employeesCollection.Count != 0)
            {
                EmployeeDataGridComboBox.SelectedIndex -= 1;
            }
        }
        #endregion
        #region Bezoek
        private void AddVisit_Click(object sender, RoutedEventArgs e)
        {
            AddBezoekScreen screen = new AddBezoekScreen(_visitorManager, _companyManager, _employeecontractManager, _visitManager);
            screen.Show();
        }
        private void VisitGridEditButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void VisitSearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                VisitSearchBox1.Text = "";
                VisitSearchBox2.Text = "";
                VisitSearchGrid2.Width = new GridLength(0);
                VisitSearchGrid3.Width = new GridLength(0);
                if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    VisitSearchBorder.Width = 55;
                    visitsAdminCollection.Clear();
                    visits.Clear();
                    visits = _visitManager.GeefBezoeken().ToList();
                    VisitsShowList();
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Speicfiek")
                {
                    VisitSearchBorder.Width = 500;
                    if (VisitSearchGrid2.Width == new GridLength(0)) { VisitSearchGrid2.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                    if (VisitSearchGrid3.Width == new GridLength(0)) { VisitSearchGrid3.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Vandaag")
                {
                    VisitSearchBorder.Width = 120;
                    visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList();
                    VisitsShowList();
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Deze Week")
                {
                    VisitSearchBorder.Width = 120;
                    if (DateTime.Today.DayOfWeek.ToString() == "Monday")
                    {
                        visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList();
                        VisitsShowList();
                    }
                    else
                    {
                        int number = 0;
                        if (DateTime.Today.DayOfWeek.ToString() == "Tuesday") { number = 2; }
                        else if (DateTime.Today.DayOfWeek.ToString() == "Wednesday") { number = 3; }
                        else if (DateTime.Today.DayOfWeek.ToString() == "Thursday") { number = 4; }
                        else if (DateTime.Today.DayOfWeek.ToString() == "Friday") { number = 5; }
                        else if (DateTime.Today.DayOfWeek.ToString() == "Saturday") { number = 6; }
                        else if (DateTime.Today.DayOfWeek.ToString() == "Sunday") { number = 7; }
                        if (DateTime.Today.Day >= number)
                        {
                            int firstDayOfTheWeek = DateTime.Today.Day - (number - 1);
                            visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{firstDayOfTheWeek}").ToList();
                            VisitsShowList();
                        }
                        else
                        {
                            int firstDayOfTheWeek = 0;
                            if (DateTime.Today.Month == 1) { firstDayOfTheWeek = 31; }
                            else { firstDayOfTheWeek = DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month - 1) - (number - (DateTime.Today.Day + 1)); }
                            visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month - 1}-{firstDayOfTheWeek}").ToList();
                            VisitsShowList();
                        }
                    }
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Deze Maand")
                {
                    VisitSearchBorder.Width = 120;
                    visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-0").ToList();
                    VisitsShowList();
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Dit Jaar")
                {
                    VisitSearchBorder.Width = 120;
                    visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-0-0").ToList();
                    VisitsShowList();
                }
            }
        }
        private void VisitSearchBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(VisitSearchBox1.Text) || !string.IsNullOrWhiteSpace(VisitSearchBox2.Text))
            {
                visitsAdminCollection.Clear();
                visits.Clear();
                try
                {
                    foreach (Bezoek bezoek in allVisits)
                    {
                        try
                        {
                            if (bezoek.StartTijd > DateTime.Parse(VisitSearchBox1.Text) && bezoek.StartTijd < DateTime.Parse(VisitSearchBox2.Text))
                            {
                                visits.Add(bezoek);
                            }
                        }
                        catch (Exception ex) { }
                    }
                    VisitsShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { visits = _visitManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList(); VisitsShowList(); }
        }
        private void VisitsShowList()
        {
            visitsAdminCollection.Clear();
            VisitDataGridComboBox.Items.Clear();
            VisitDataGridNextPage.Visibility = Visibility.Collapsed;
            VisitDataGridPreviewPage.Visibility = Visibility.Collapsed;
            int numberPerPage = int.Parse(VisitItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (visits.Count > numberPerPage)
            {
                int totalPages = (visits.Count + (numberPerPage - (visits.Count % numberPerPage))) / numberPerPage;
                for (int i = 1; i <= totalPages; i++)
                {
                    ComboBoxItem item = new ComboBoxItem();
                    item.Content = i;
                    if (i == 1) { item.IsSelected = true; }
                    VisitDataGridComboBox.Items.Add(item);
                }
                VisitDataGridNextPage.Visibility = Visibility.Visible;
            }
            else
            {
                visits.ForEach(v => visitsAdminCollection.Add(new BezoekAdmin(v)));
            }
        }
        private void VisitItemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized)
            {
                visitsAdminCollection.Clear();
                VisitsShowList();
            }
        }
        private void VisitDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitDataGridComboBox.SelectedIndex != -1)
            {
                visitsAdminCollection.Clear();
                int numberPerPage = int.Parse(VisitItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = VisitDataGridComboBox.SelectedIndex * numberPerPage;
                VisitDataGridNextPage.Visibility = Visibility.Visible;
                VisitDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { VisitDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (visits.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < visits.Count; i++)
                    {
                        visitsAdminCollection.Add(new BezoekAdmin(visits[i]));
                    }
                    VisitDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        visitsAdminCollection.Add(new BezoekAdmin(visits[i]));
                    }
                }
            }
        }
        private void VisitDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (visitsAdminCollection.Count == int.Parse(VisitItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                VisitDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void VisitDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (visitsAdminCollection.Count != 0)
            {
                VisitDataGridComboBox.SelectedIndex -= 1;
            }
        }
        #endregion
    }
}
