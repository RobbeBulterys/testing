using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for AddEmployeeContractScreen.xaml
    /// </summary>
    public partial class AddEmployeeContractScreen : Window
    {
        private bool _IsInitialized = false;
        private bool _isMaximized = false;
        private Company _company;
        private CompanyManager _companyManager;
        private EmployeeManager _employeeManager;
        private EmployeecontractManager _employeecontractManager;
        private List<Company> companies = new List<Company>();
        private List<Employee> employees = new List<Employee>();
        private ObservableCollection<Company> companyList = new ObservableCollection<Company>();
        private ObservableCollection<Employee> employeeList = new ObservableCollection<Employee>();
        public AddEmployeeContractScreen(EmployeeManager employeeManager, CompanyManager companyManager, EmployeecontractManager employeecontractManager, Company company)
        {
            InitializeComponent();
            _IsInitialized = true;
            _company = company;
            _employeeManager = employeeManager;
            _companyManager = companyManager;
            _employeecontractManager = employeecontractManager;
            CompanyDataGrid.ItemsSource = companyList;
            EmployeeDataGrid.ItemsSource = employeeList;
            InitializeLists();
        }
        private void InitializeLists()
        {
            companies.Clear();
            employees.Clear();
            companies = _companyManager.GetCompanies().ToList();
            CompanysShowList();
            employees = _employeeManager.GetAllEmployees().ToList();
            EmployeesShowList();
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
                    companyList.Clear();
                    companies = _companyManager.GetCompanies().ToList();
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
            companyList.Clear();
            companies.Clear();
            if (!string.IsNullOrWhiteSpace(CompanySearchBox.Text))
            {
                try
                {
                    if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: All") { companies = _companyManager.SearchCompanies(null, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Bedrijfsnaam") { companies = _companyManager.SearchCompanies(null, CompanySearchBox.Text, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: BTW-nummer") { companies = _companyManager.SearchCompanies(CompanySearchBox.Text, null, null, null).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Telefoon") { companies = _companyManager.SearchCompanies(null, null, null, CompanySearchBox.Text).ToList(); }
                    else if (CompanySearchComboBox.SelectedValue.ToString() == "System.Windows.Controls.ComboBoxItem: Email") { companies = _companyManager.SearchCompanies(null, null, CompanySearchBox.Text, null).ToList(); }
                    CompanysShowList();
                }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { companies = _companyManager.SearchCompanies(null, null, null, null).ToList(); CompanysShowList(); }
        }
        private void CompanysShowList()
        {
            CompanyDataGridComboBox.Items.Clear();
            CompanyDataGridNextPage.Visibility = Visibility.Collapsed;
            CompanyDataGridPreviewPage.Visibility = Visibility.Collapsed;
            int numberPerPage = int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
            if (companies.Count > numberPerPage)
            {
                int totalPages = (companies.Count + (numberPerPage - (companies.Count % numberPerPage))) / numberPerPage;
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
                companyList.Clear();
                CompanysShowList();
            }
        }
        private void CompanyDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && CompanyDataGridComboBox.SelectedIndex != -1)
            {
                companyList.Clear();
                int numberPerPage = int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = CompanyDataGridComboBox.SelectedIndex * numberPerPage;
                CompanyDataGridNextPage.Visibility = Visibility.Visible;
                CompanyDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { CompanyDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (companies.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < companies.Count; i++)
                    {
                        companyList.Add(companies[i]);
                    }
                    CompanyDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        companyList.Add(companies[i]);
                    }
                }
            }
        }
        private void CompanyDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (companyList.Count == int.Parse(CompanyItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                CompanyDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void CompanyDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (companyList.Count != 0)
            {
                CompanyDataGridComboBox.SelectedIndex -= 1;
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
                    employeeList.Clear();
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
                employeeList.Clear();
                employees.Clear();
                try { employees = _employeeManager.SearchEmployees(EployeeSearchBox1.Text, EployeeSearchBox2.Text).ToList(); EmployeesShowList(); }
                catch (Exception ex) { MessageBox.Show($"{ex}"); }
            }
            else { employees = _employeeManager.GetAllEmployees().ToList(); EmployeesShowList(); }
        }
        private void EmployeesShowList()
        {
            employeeList.Clear();
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
                employeeList.Clear();
                EmployeesShowList();
            }
        }
        private void EmployeeDataGridComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender.GetType() == typeof(ComboBox) && _IsInitialized && EmployeeDataGridComboBox.SelectedIndex != -1)
            {
                employeeList.Clear();
                int numberPerPage = int.Parse(EmployeeItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]);
                int counter = EmployeeDataGridComboBox.SelectedIndex * numberPerPage;
                EmployeeDataGridNextPage.Visibility = Visibility.Visible;
                EmployeeDataGridPreviewPage.Visibility = Visibility.Visible;
                if (counter == 0) { EmployeeDataGridPreviewPage.Visibility = Visibility.Collapsed; }
                if (employees.Count < counter + numberPerPage)
                {
                    for (int i = counter; i < employees.Count; i++)
                    {
                        employeeList.Add(employees[i]);
                    }
                    EmployeeDataGridNextPage.Visibility = Visibility.Collapsed;
                }
                else
                {
                    for (int i = counter; i < counter + numberPerPage; i++)
                    {
                        employeeList.Add(employees[i]);
                    }
                }
            }
        }
        private void EmployeeDataGridNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (employeeList.Count == int.Parse(EmployeeItemsPerPageComboBox.SelectedItem.ToString().Split(" ").ToList()[1]))
            {
                EmployeeDataGridComboBox.SelectedIndex += 1;
            }
        }
        private void EmployeeDataGridPreviewPage_Click(object sender, RoutedEventArgs e)
        {
            if (employeeList.Count != 0)
            {
                EmployeeDataGridComboBox.SelectedIndex -= 1;
            }
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
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
