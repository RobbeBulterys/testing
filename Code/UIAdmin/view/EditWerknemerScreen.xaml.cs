using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
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
using System.Windows.Shapes;
using UIAdmin.Domein;

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for EditWerknemerScreen.xaml
    /// </summary>
    public partial class EditWerknemerScreen : Window
    {
        private bool _isMaximized = false;
        private Employee _employee;
        private EmployeeManager _employeeManager;
        private VisitManager _visitManager;
        private EmployeecontractManager _employeecontractManager;
        private ObservableCollection<Employeecontract> employeecontracts = new ObservableCollection<Employeecontract>();
        private ObservableCollection<VisitAdmin> contracts = new ObservableCollection<VisitAdmin>();
        public EditWerknemerScreen(EmployeeManager employeeManager, EmployeecontractManager employeecontractManager, VisitManager visitManager, Employee employee, string screen)
        {
            InitializeComponent();
            _employeeManager = employeeManager;
            _visitManager = visitManager;
            _employeecontractManager = employeecontractManager;
            _employee = employee;
            EmployeeContractDataGrid.ItemsSource = employeecontracts;
            VisitsDataGrid.ItemsSource = contracts;
            InitializeEmployee(employee, screen);
        }
        private void InitializeEmployee(Employee employee, string screen)
        {
            if (screen == "Employee") { EmployeeBtn.IsChecked = true; }
            if (screen == "Visits") { VisitsBtn.IsChecked = true; }
            TextBoxName.Text = employee.LastName;
            TextBoxFirstName.Text = employee.FirstName;
            EmployeeId.Text = $"{employee.LastName} {employee.FirstName}";
            EmployeeIdVisit.Text = $"{employee.LastName} {employee.FirstName}";
            TextBlockIdEmployee.Text = $"Id: {employee.PersonId}";
            EmployeeBtn.Content = $"{employee.LastName}";
            employeecontracts.Clear();
            List<Employeecontract> employeeContracts = new List<Employeecontract>();
            employeeContracts.AddRange(_employeecontractManager.GetEmployeeContracts(employee).ToList());
            employeeContracts.ForEach(a => employeecontracts.Add(a));
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
                    string? name = null;
                    string? firstname = null;
                    string message = "";
                    if ((!string.IsNullOrWhiteSpace(TextBoxName.Text)) && (TextBoxName.Text != _employee.LastName)) { name = TextBoxName.Text; message += $"naam => {name}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxFirstName.Text)) && (TextBoxFirstName.Text != _employee.FirstName)) { firstname = TextBoxFirstName.Text; message += $"voornaam => {firstname}\n"; }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else
                    {
                        try
                        {
                            _employeeManager.UpdateEmployee(_employee.PersonId, name, firstname);
                            MessageBox.Show($"succes!");
                            this.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"error!, {ex}");
                        }
                    }
                }
            }
        }
        private void EmployeeBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                VisitBorder.Visibility = Visibility.Collapsed;
                EmployeeBorder.Visibility = Visibility.Visible;
            }
        }
        private void VisitsBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                VisitBorder.Visibility = Visibility.Visible;
                EmployeeBorder.Visibility = Visibility.Collapsed;
                contracts.Clear();
                List<Visit> b = _visitManager.SearchVisits(null, null, _employee, null).ToList();
                foreach (Visit visit in b)
                {
                    contracts.Add(new VisitAdmin(visit));
                }
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SaveBtn.IsEnabled = true;
            SolidColorBrush colorBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#623ed0");
            BorderName.BorderBrush = colorBrush;
            TBName.Text = "";
            TBName.Foreground = Brushes.Black;
            BorderFirstName.BorderBrush = colorBrush;
            TBFirstName.Text = "";
            TBFirstName.Foreground = Brushes.Black;
            string? name = null;
            string? firstname = null;
            if (!string.IsNullOrWhiteSpace(TextBoxName.Text)) { name = TextBoxName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxFirstName.Text)) { firstname = TextBoxFirstName.Text; }
            if (name == null)
            {
                BorderName.BorderBrush = Brushes.Red;
                TBName.Text = "Naam: mag niet leeg zijn!";
                TBName.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (firstname == null)
            {
                BorderFirstName.BorderBrush = Brushes.Red;
                TBFirstName.Text = "Voornaam: mag niet leeg zijn!";
                TBFirstName.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
        }
    }
}
