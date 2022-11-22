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
        private AddressManager _adresManager;
        private CompanyManager _companyManager;
        private VisitorManager _visitorManager;
        private EmployeeManager _employeeManager;
        private VisitManager _visitManager;
        private EmployeecontractManager _employeecontractManager;
        private List<Employee> employees = new List<Employee>();
        private List<Company> companys = new List<Company>();
        private List<Visitor> visitors = new List<Visitor>();
        private List<Visit> visits = new List<Visit>();
        private List<Visit> allVisits = new List<Visit>();
        private ObservableCollection<Company> companysCollection = new ObservableCollection<Company>();
        private ObservableCollection<Visitor> visitorsCollection = new ObservableCollection<Visitor>();
        private ObservableCollection<Employee> employeesCollection = new ObservableCollection<Employee>();
        private ObservableCollection<VisitAdmin> visitsAdminCollection = new ObservableCollection<VisitAdmin>();
        public MainScreen(AddressManager adresManager, CompanyManager companyManager, VisitorManager visitorManager, EmployeeManager employeeManager, EmployeecontractManager employeecontract, VisitManager visitManager)
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
                    if (companys.Count == 0)
                    {
                        try
                        {
                            companys.Clear();
                            companysCollection.Clear();
                            companys = _companyManager.GetCompanies().ToList();
                            CompanysShowList();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.InnerException.InnerException.Message); }
                    }
                }
                if (button.Name == "VisitorBtn")
                {
                    VisitorBorder.Visibility = Visibility.Visible;
                    if (visitors.Count == 0)
                    {
                        try
                        {
                            visitorsCollection.Clear();
                            visitors.Clear();
                            visitors = _visitorManager.GetVisitors().ToList();
                            VisitorsShowList();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.InnerException.InnerException.Message); }
                    }
                }
                if (button.Name == "EmployeeBtn")
                {
                    EmployeeBorder.Visibility = Visibility.Visible;
                    if (employees.Count == 0)
                    {
                        try
                        {
                            employeesCollection.Clear();
                            employees.Clear();
                            employees = _employeeManager.GetAllEmployees().ToList();
                            EmployeesShowList();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.InnerException.InnerException.Message); }
                    }
                }
                if (button.Name == "VisitBtn")
                {
                    VisitBorder.Visibility = Visibility.Visible;
                    if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Vandaag")
                    {
                        try
                        {
                            visitsAdminCollection.Clear();
                            visits.Clear();
                            DateTime tomorrow = DateTime.Today.AddDays(1);
                            visits = _visitManager.SearchSpecificVisits($"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}", $"{tomorrow.Year}-{tomorrow.Month}-{tomorrow.Day}").OrderBy(v => v.StartingTime).ToList();
                            VisitsShowList();
                        }
                        catch (Exception ex) { MessageBox.Show(ex.InnerException.InnerException.Message); }
                    }
                }
            }
        }
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region Company
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
                if (CompanyDataGrid.SelectedItem.GetType() == typeof(Company))
                {
                    Company b = (Company)CompanyDataGrid.SelectedItem;
                    if (button.Name == "CompanyEditButton1") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, _employeeManager, b, "Company"); screen.Show(); }
                    if (button.Name == "CompanyEditButton2") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, _employeeManager, b, "Workers"); screen.Show(); }
                    if (button.Name == "CompanyEditButton3") { EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _companyManager, _employeecontractManager, _visitManager, _employeeManager, b, "Visits"); screen.Show(); }
                }
            }
        }
        private void CompanyGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CompanyDataGrid.SelectedItem.GetType() == typeof(Company))
            {
                Company b = (Company)CompanyDataGrid.SelectedItem;
                if (b.Address != null) _companyManager.DeleteCompany(b, b.Address.Id);
                else _companyManager.DeleteCompany(b, null);
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
                    companys = _companyManager.GetCompanies().ToList();
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
                    if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All") { companys = _companyManager.SearchCompanies(null, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Bedrijfsnaam") { companys = _companyManager.SearchCompanies(null, CompanySearchBox.Text, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: BTW-nummer") { companys = _companyManager.SearchCompanies(CompanySearchBox.Text, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Telefoon") { companys = _companyManager.SearchCompanies(null, null, null, CompanySearchBox.Text).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Email") { companys = _companyManager.SearchCompanies(null, null, CompanySearchBox.Text, null).ToList(); }
                    CompanysShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { companys = _companyManager.SearchCompanies(null, null, null, null).ToList(); CompanysShowList(); }
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
                CompanyDataGridMaxPages.Text = $"of {totalPages}";
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
                CompanyDataGridMaxPages.Text = $"of 1";
                ComboBoxItem item = new ComboBoxItem();
                item.Content = 1;
                item.IsSelected = true;
                CompanyDataGridComboBox.Items.Add(item);
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
        #region Visitor
        private void AddVisitor_Click(object sender, RoutedEventArgs e)
        {
            AddBezoekerScreen screen = new AddBezoekerScreen(_visitorManager);
            screen.Show();
        }
        private void VisitorGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorDataGrid.SelectedItem.GetType() == typeof(Visitor))
            {
                Visitor b = (Visitor)VisitorDataGrid.SelectedItem;
                EditBezoekerScreen screen = new EditBezoekerScreen(_visitorManager, _visitManager, b);
                screen.Show();
            }
        }
        private void VisitorGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (VisitorDataGrid.SelectedItem.GetType() == typeof(Visitor))
            {
                Visitor b = (Visitor)VisitorDataGrid.SelectedItem;
                _visitorManager.DeleteVisitor(b);
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
                    visitors = _visitorManager.GetVisitors().ToList();
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
                    if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All") { visitors = _visitorManager.GetVisitors().ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam en Voornaam") { visitors = _visitorManager.SearchVisitors(VisitorSearchBox1.Text, VisitorSearchBox2.Text, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Naam") { visitors = _visitorManager.SearchVisitors(VisitorSearchBox1.Text, null, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Voornaam") { visitors = _visitorManager.SearchVisitors(null, VisitorSearchBox1.Text, null, null).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Bedrijfsnaam") { visitors = _visitorManager.SearchVisitors(null, null, null, VisitorSearchBox1.Text).ToList(); }
                    else if (VisitorSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Email") { visitors = _visitorManager.SearchVisitors(null, null, VisitorSearchBox1.Text, null).ToList(); }
                    VisitorsShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { visitors = _visitorManager.GetVisitors().ToList(); VisitorsShowList(); }
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
                VisitorDataGridMaxPages.Text = $"of {totalPages}";
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
                VisitorDataGridMaxPages.Text = $"of 1";
                ComboBoxItem item = new ComboBoxItem();
                item.Content = 1;
                item.IsSelected = true;
                VisitorDataGridComboBox.Items.Add(item);
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
        #region Employee
        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddWerknemerScreen screen = new AddWerknemerScreen(_employeeManager);
            screen.Show();
        }
        private void EmployeeGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                if (EmployeeDataGrid.SelectedItem.GetType() == typeof(Employee))
                {
                    Button button = (Button)sender;
                    Employee w = (Employee)EmployeeDataGrid.SelectedItem;
                    if (button.Name == "EmployeeGridEditButton1") { EditWerknemerScreen screen = new EditWerknemerScreen(_employeeManager, _employeecontractManager, _visitManager, w, "Employee"); screen.Show(); }
                    else if (button.Name == "EmployeeGridEditButton2") { EditWerknemerScreen screen = new EditWerknemerScreen(_employeeManager, _employeecontractManager, _visitManager, w, "Visits"); screen.Show(); }
                }
            }
        }
        private void EmployeeGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem.GetType() == typeof(Employee))
            {
                Employee w = (Employee)EmployeeDataGrid.SelectedItem;
                try
                {
                    _employeeManager.DeleteEmployee(w.PersonId);
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
                    employees = _employeeManager.GetAllEmployees().ToList();
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
                try { employees = _employeeManager.SearchEmployees(EployeeSearchBox1.Text, EployeeSearchBox2.Text).ToList(); EmployeesShowList(); }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { employees = _employeeManager.GetAllEmployees().ToList(); EmployeesShowList(); }
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
                EmployeeDataGridMaxPages.Text = $"of {totalPages}";
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
                EmployeeDataGridMaxPages.Text = $"of 1";
                ComboBoxItem item = new ComboBoxItem();
                item.Content = 1;
                item.IsSelected = true;
                EmployeeDataGridComboBox.Items.Add(item);
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
        #region Visit
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
                VisitSearchGrid4.Width = new GridLength(0);
                VisitSearchGrid5.Width = new GridLength(0);
                VisitSearchGrid6.Width = new GridLength(0);
                VisitListOfMonthsBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfMonthsBtnDown.Visibility = Visibility.Collapsed;
                VisitListOfWeeksBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfWeeksBtnDown.Visibility = Visibility.Collapsed;
                if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All")
                {
                    VisitSearchBorder.Width = 55;
                    visitsAdminCollection.Clear();
                    visits.Clear();
                    visits = _visitManager.GetVisits().ToList();
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
                    VisitSearchBorder.Width = 80;
                    visits.Clear();
                    DateTime tomorrow = DateTime.Today.AddDays(1);
                    visits = _visitManager.SearchSpecificVisits($"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}", $"{tomorrow.Year}-{tomorrow.Month}-{tomorrow.Day}").OrderBy(v => v.StartingTime).ToList();
                    VisitsShowList();
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Week")
                {
                    VisitSearchBorder.Width = 500;
                    if (VisitSearchGrid4.Width == new GridLength(0)) { VisitSearchGrid4.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                    if (VisitSearchGrid5.Width == new GridLength(0)) { VisitSearchGrid5.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("2*"); }
                    VisitListOfYearsComboBox.SelectedIndex = 0;
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Maand")
                {
                    VisitSearchBorder.Width = 500;
                    if (VisitSearchGrid4.Width == new GridLength(0)) { VisitSearchGrid4.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("*"); }
                    if (VisitSearchGrid6.Width == new GridLength(0)) { VisitSearchGrid6.Width = (GridLength)new GridLengthConverter().ConvertFromInvariantString("2*"); }
                    VisitListOfYearsComboBox.SelectedIndex = 0;
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Dit Jaar")
                {
                    VisitSearchBorder.Width = 120;
                    visits.Clear();
                    DateTime tomorrow = DateTime.Today.AddYears(1);
                    visits = _visitManager.SearchSpecificVisits($"{DateTime.Today.Year}-{0}-{0}", $"{tomorrow.Year}-{0}-{0}").OrderBy(v => v.StartingTime).ToList();
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
                    foreach (Visit bezoek in allVisits)
                    {
                        try
                        {
                            if (bezoek.StartingTime > DateTime.Parse(VisitSearchBox1.Text) && bezoek.StartingTime < DateTime.Parse(VisitSearchBox2.Text))
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
            else 
            {
                visitsAdminCollection.Clear();
                visits.Clear();
                DateTime tomorrow = DateTime.Today.AddDays(1);
                visits = _visitManager.SearchSpecificVisits($"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}", $"{tomorrow.Year}-{tomorrow.Month}-{tomorrow.Day}").OrderBy(v => v.StartingTime).ToList();
                VisitsShowList();
            }
        }
        private void VisitsShowList()
        {
            visitsAdminCollection.Clear();
            VisitDataGridComboBox.Items.Clear();
            int numberPerPage = int.Parse(VisitItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (visits.Count > numberPerPage)
            {
                int totalPages = (visits.Count + (numberPerPage - (visits.Count % numberPerPage))) / numberPerPage;
                VisitDataGridMaxPages.Text = $"of {totalPages}";
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
                VisitDataGridMaxPages.Text = $"of 1";
                ComboBoxItem item = new ComboBoxItem();
                item.Content = 1;
                item.IsSelected = true;
                VisitDataGridComboBox.Items.Add(item);
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
                        visitsAdminCollection.Add(new VisitAdmin(visits[i]));
                    }
                    VisitDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        visitsAdminCollection.Add(new VisitAdmin(visits[i]));
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
        private void VisitListOfYearsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitListOfYearsComboBox.SelectedIndex > 0)
            {
                if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Week")
                {
                    VisitListOfWeeksBtnUp.Visibility = Visibility.Collapsed;
                    VisitListOfWeeksBtnDown.Visibility = Visibility.Collapsed;
                    int selectedYear = int.Parse(VisitListOfYearsComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                    DateTime date = new DateTime(selectedYear, 1, 1);
                    int number = 0;
                    if (date.DayOfWeek.ToString() == "Monday") { number = 1; }
                    else if (date.DayOfWeek.ToString() == "Tuesday") { number = 2; }
                    else if (date.DayOfWeek.ToString() == "Wednesday") { number = 3; }
                    else if (date.DayOfWeek.ToString() == "Thursday") { number = 4; }
                    else if (date.DayOfWeek.ToString() == "Friday") { number = 5; }
                    else if (date.DayOfWeek.ToString() == "Saturday") { number = 6; }
                    else if (date.DayOfWeek.ToString() == "Sunday") { number = 7; }
                    int firstDayOfTheWeek = 0;
                    DateTime FirstWeek = DateTime.Now;
                    if (number == 1) { FirstWeek = date; }
                    else { firstDayOfTheWeek = 31 - (number - 2); FirstWeek = new DateTime(date.Year - 1, 12, firstDayOfTheWeek); }
                    VisitListOfWeeksComboBox.Items.Clear();
                    for (int i = 0; i < 55; i++)
                    {
                        if (FirstWeek.AddDays(7 * i).Year > date.Year) { break; }
                        if (FirstWeek.AddDays(7 * i) > DateTime.Today) { break; }
                        DateTime date1 = FirstWeek.AddDays(7 * i);
                        ComboBoxItem item = new ComboBoxItem();
                        item.Content = $"week {i + 1}: {date1.ToShortDateString()} - {date1.AddDays(6).ToShortDateString()}";
                        if (FirstWeek.AddDays((7 * i) + 7) > DateTime.Today) { item.IsSelected = true; }
                        if (i == 0 && selectedYear != DateTime.Today.Year) { item.IsSelected = true; }
                        VisitListOfWeeksComboBox.Items.Add(item);
                    }
                }
                else if (VisitSearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Maand")
                {
                    VisitListOfMonthsBtnUp.Visibility = Visibility.Collapsed;
                    VisitListOfMonthsBtnDown.Visibility = Visibility.Collapsed;
                    int selectedYear = int.Parse(VisitListOfYearsComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                    DateTime date = new DateTime(selectedYear, 1, 1);
                    VisitListOfMonthsComboBox.Items.Clear();
                    for (int i = 0; i < 12; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        if (date.Year < DateTime.Today.Year && i == 0) { item.IsSelected = true; }
                        if (date.AddMonths(i).Month == 1) { item.Content = "Januari"; }
                        else if (date.AddMonths(i).Month == 2) { item.Content = "Februari"; }
                        else if (date.AddMonths(i).Month == 3) { item.Content = "Maart"; }
                        else if (date.AddMonths(i).Month == 4) { item.Content = "April"; }
                        else if (date.AddMonths(i).Month == 5) { item.Content = "Mei"; }
                        else if (date.AddMonths(i).Month == 6) { item.Content = "Juni"; }
                        else if (date.AddMonths(i).Month == 7) { item.Content = "Juli"; }
                        else if (date.AddMonths(i).Month == 8) { item.Content = "Augustus"; }
                        else if (date.AddMonths(i).Month == 9) { item.Content = "September"; }
                        else if (date.AddMonths(i).Month == 10) { item.Content = "Oktober"; }
                        else if (date.AddMonths(i).Month == 11) { item.Content = "November"; }
                        else if (date.AddMonths(i).Month == 12) { item.Content = "December"; }
                        if (date.Year == DateTime.Today.Year && date.AddMonths(i).Month == DateTime.Today.Month)
                        { item.IsSelected = true; VisitListOfMonthsComboBox.Items.Add(item); break; }
                        VisitListOfMonthsComboBox.Items.Add(item);
                    }
                }
            }
            else if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitListOfYearsComboBox.SelectedIndex == 0)
            {
                VisitListOfWeeksBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfWeeksBtnDown.Visibility = Visibility.Collapsed;
                VisitListOfMonthsBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfMonthsBtnDown.Visibility = Visibility.Collapsed;
                VisitListOfWeeksComboBox.Items.Clear();
                VisitListOfMonthsComboBox.Items.Clear();
            }
        }
        private void VisitListOfWeeksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitListOfWeeksComboBox.SelectedIndex != -1)
            {
                VisitListOfWeeksBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfWeeksBtnDown.Visibility = Visibility.Collapsed;
                DateTime date = DateTime.Parse(VisitListOfWeeksComboBox.SelectedItem.ToString().Split(" ").ToList()[3]);
                DateTime date1 = date.AddDays(7);
                visits.Clear();
                visits = _visitManager.SearchSpecificVisits($"{date.Year}-{date.Month}-{date.Day}", $"{date1.Year}-{date1.Month}-{date1.Day}").OrderBy(v => v.StartingTime).ToList();
                VisitsShowList();
                if (VisitListOfWeeksComboBox.SelectedIndex < VisitListOfWeeksComboBox.Items.Count - 1 || VisitListOfWeeksComboBox.SelectedIndex == 0) { VisitListOfWeeksBtnUp.Visibility = Visibility.Visible; }
                if (VisitListOfWeeksComboBox.SelectedIndex > 0) { VisitListOfWeeksBtnDown.Visibility = Visibility.Visible; }
            }
        }
        private void VisitListOfMonthsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && VisitListOfMonthsComboBox.SelectedIndex != -1)
            {
                VisitListOfMonthsBtnUp.Visibility = Visibility.Collapsed;
                VisitListOfMonthsBtnDown.Visibility = Visibility.Collapsed;
                string selectedMonth = VisitListOfMonthsComboBox.SelectedItem.ToString().Split(" ").ToList()[1];
                DateTime date = DateTime.Today;
                if (selectedMonth == "Januari") { date = new DateTime(date.Year, 1, 1); }
                else if (selectedMonth == "Februari") { date = new DateTime(date.Year, 2, 1); }
                else if (selectedMonth == "Maart") { date = new DateTime(date.Year, 3, 1); }
                else if (selectedMonth == "April") { date = new DateTime(date.Year, 4, 1); }
                else if (selectedMonth == "Mei") { date = new DateTime(date.Year, 5, 1); }
                else if (selectedMonth == "Juni") { date = new DateTime(date.Year, 6, 1); }
                else if (selectedMonth == "Juli") { date = new DateTime(date.Year, 7, 1); }
                else if (selectedMonth == "Augustus") { date = new DateTime(date.Year, 8, 1); }
                else if (selectedMonth == "September") { date = new DateTime(date.Year, 9, 1); }
                else if (selectedMonth == "Oktober") { date = new DateTime(date.Year, 10, 1); }
                else if (selectedMonth == "November") { date = new DateTime(date.Year, 11, 1); }
                else if (selectedMonth == "December") { date = new DateTime(date.Year, 12, 1); }
                DateTime date1 = date.AddMonths(1);
                visits.Clear();
                visits = _visitManager.SearchSpecificVisits($"{date.Year}-{date.Month}-{date.Day}", $"{date1.Year}-{date1.Month}-{date1.Day}").OrderBy(v => v.StartingTime).ToList();
                VisitsShowList();
                if (VisitListOfMonthsComboBox.SelectedIndex < VisitListOfMonthsComboBox.Items.Count - 1 || VisitListOfMonthsComboBox.SelectedIndex == 0) { VisitListOfMonthsBtnUp.Visibility = Visibility.Visible; }
                if (VisitListOfMonthsComboBox.SelectedIndex > 0) { VisitListOfMonthsBtnDown.Visibility = Visibility.Visible; }
            }
        }
        private void VisitListOfBtnUp_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button) && _IsInitialized)
            {
                Button button = sender as Button;
                if (button.Name == "VisitListOfMonthsBtnUp") { VisitListOfMonthsComboBox.SelectedIndex += 1; }
                if (button.Name == "VisitListOfWeeksBtnUp") { VisitListOfWeeksComboBox.SelectedIndex += 1; }
            }
        }
        private void VisitListOfBtnDown_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button) && _IsInitialized)
            {
                Button button = sender as Button;
                if (button.Name == "VisitListOfMonthsBtnDown") { VisitListOfMonthsComboBox.SelectedIndex -= 1; }
                if (button.Name == "VisitListOfWeeksBtnDown") { VisitListOfWeeksComboBox.SelectedIndex -= 1; }

            }
        }
        #endregion
    }
}
