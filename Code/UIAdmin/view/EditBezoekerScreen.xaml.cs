using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
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
    /// Interaction logic for EditBezoekerScreen.xaml
    /// </summary>
    public partial class EditBezoekerScreen : Window
    {
        private bool _isMaximized = false;
        private VisitorManager _visitorManager;
        private VisitManager _visitManager;
        private Visitor _visitor;
        private ObservableCollection<VisitAdmin> contracts = new ObservableCollection<VisitAdmin>();
        public EditBezoekerScreen(VisitorManager visitorManager, VisitManager visitManager, Visitor visitor)
        {
            InitializeComponent();
            _visitorManager = visitorManager;
            _visitManager = visitManager;
            _visitor = visitor;
            VisitsDataGrid.ItemsSource = contracts;
            InitializeBezoeker(visitor);
        }
        private void InitializeBezoeker(Visitor visitor)
        {
            TextBoxVisitorName.Text = visitor.LastName;
            TextBoxFirstName.Text = visitor.FirstName;
            TextBoxEmail.Text = visitor.Email;
            TextBoxCompanyName.Text = visitor.Company;
            VisitorId.Text = $"{visitor.LastName} {visitor.FirstName}";
            TextBlockIdVisitor.Text = $"Id: {visitor.PersonId}";
            contracts.Clear();
            List<Visit> b = _visitManager.SearchVisits(_visitor, null, null, null).ToList();
            foreach (Visit visit in b)
            {
                contracts.Add(new VisitAdmin(visit));
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
                    string? email = null;
                    string? companyName = null;
                    string message = "";
                    if ((!string.IsNullOrWhiteSpace(TextBoxVisitorName.Text)) && (TextBoxVisitorName.Text != _visitor.LastName)) { name = TextBoxVisitorName.Text; message += $"naam => {name}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxFirstName.Text)) && (TextBoxFirstName.Text != _visitor.FirstName)) { firstname = TextBoxFirstName.Text; message += $"voornaam => {firstname}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) && (TextBoxEmail.Text != _visitor.Email)) { email = TextBoxEmail.Text; message += $"email => {email}\n"; }
                    if ((!string.IsNullOrWhiteSpace(TextBoxCompanyName.Text)) && (TextBoxCompanyName.Text != _visitor.Company)) { companyName = TextBoxCompanyName.Text; message += $"bedrijfnaam => {companyName}\n"; }
                    if (message == "") MessageBox.Show("Er moet minimum 1 veld aangepast worden!");
                    else
                    {
                        _visitorManager.UpdateVisitor(_visitor.PersonId, name, firstname, email, companyName);
                    }
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
            BorderEmail.BorderBrush = colorBrush;
            TBEmail.Text = "";
            TBEmail.Foreground = Brushes.Black;
            BorderCompanyName.BorderBrush = colorBrush;
            TBCompanyName.Text = "";
            TBCompanyName.Foreground = Brushes.Black;
            string? name = null;
            string? firstname = null;
            string? email = null;
            string? companyName = null;
            if (!string.IsNullOrWhiteSpace(TextBoxVisitorName.Text)) { name = TextBoxVisitorName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxFirstName.Text)) { firstname = TextBoxFirstName.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxEmail.Text)) { email = TextBoxEmail.Text; }
            if (!string.IsNullOrWhiteSpace(TextBoxCompanyName.Text)) { companyName = TextBoxCompanyName.Text; }
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
            if (email == null)
            {
                BorderEmail.BorderBrush = Brushes.Red;
                TBEmail.Text = "Email: mag niet leeg zijn!";
                TBEmail.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (companyName == null)
            {
                BorderCompanyName.BorderBrush = Brushes.Red;
                TBCompanyName.Text = "Bedrijfsnaam: mag niet leeg zijn!";
                TBCompanyName.Foreground = Brushes.Red;
                SaveBtn.IsEnabled = false;
            }
            if (email != null)
            {
                try
                {
                    if (Verify.IsValidEmailSyntax(email)) { }
                }
                catch (Exception ex)
                {
                    SaveBtn.IsEnabled = false;
                    if (ex.Message == "Controle - IsGoedeEmailSyntax - ongeldige email")
                    {
                        BorderEmail.BorderBrush = Brushes.Red;
                        TBEmail.Text += "ongeldige syntax!";
                        TBEmail.Foreground = Brushes.Red;
                    }
                }
            }
        }
    }
}
