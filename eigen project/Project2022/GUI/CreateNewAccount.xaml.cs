using System;
using System.Collections.Generic;
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
using Project2022.Domein;

namespace GUI
{
    /// <summary>
    /// Interaction logic for CreateNewAccount.xaml
    /// </summary>
    public partial class CreateNewAccount : Window
    {
        private DomeinController dc = new DomeinController();
        private string contentUI = "Enter text here...";
        private List<User> lijstUsers = new List<User>();
        public CreateNewAccount(DomeinController _dc, List<User> users)
        {
            InitializeComponent();
            ContentUI();
            dc = _dc;
            lijstUsers = users;
        }
        private void ContentUI()
        {
            txtFirstName.Text = contentUI;
            txtLastName.Text = contentUI;
            txtUserName.Text = contentUI;
        }

        #region UserInterface Mouse Enter/Leave
        // text when mouse enters
        private void txtFirstName_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseFocus("firstname");
        }
        // text when mouse leave
        private void txtFirstName_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseFocus("firstname");
        }
        // text when mouse enters
        private void txtLastName_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseFocus("lastname");
        }
        // text when mouse leave
        private void txtLastName_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseFocus("lastname");
        }
        // text when mouse enters
        private void txtUserName_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseFocus("username");
        }
        // text when mouse leave
        private void txtUserName_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseFocus("username");
        }
        private void MouseFocus(string tekst)
        {
            if (txtFirstName.Text == "" && !txtFirstName.IsKeyboardFocused)
            {
                txtFirstName.Text = contentUI;
            }
            if (txtLastName.Text == "" && !txtLastName.IsKeyboardFocused)
            {
                txtLastName.Text = contentUI;
            }
            if (txtUserName.Text == "" && !txtUserName.IsKeyboardFocused)
            {
                txtUserName.Text = contentUI;
            }
            switch (tekst)
            {
                case "firstname":
                    if (txtFirstName.Text != contentUI && txtFirstName.Text != "") { }
                    else if (txtFirstName.IsKeyboardFocused || txtFirstName.Text == contentUI) txtFirstName.Text = "";
                    break;
                case "lastname":
                    if (txtLastName.Text != contentUI && txtLastName.Text != "") { }
                    else if (txtLastName.IsKeyboardFocused || txtLastName.Text == contentUI) txtLastName.Text = "";
                    break;
                case "username":
                    if (txtUserName.Text != contentUI && txtUserName.Text != "") { }
                    else if (txtUserName.IsKeyboardFocused || txtUserName.Text == contentUI) txtUserName.Text = "";
                    break;
            }
        }
        #endregion
        #region Creating new account 
        // Butten for creating an new account and checking on exceptions
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User user = dc.CreateAccount(txtFirstName.Text, txtLastName.Text, txtUserName.Text, txtFirstPassword.Password, txtsecondPassword.Password);
                Checking_LijstUsers_For_UserName(user);
                xmlBestandAanmaken();
                MainWindow window = new MainWindow(dc);
                window.Title = "Project2022";
                window.WindowState = WindowState.Maximized;
                window.Show();
                this.Close();
            }
            catch (IncorrectException f)
            {
                lblPassword.Content = f.Message;
                txtFirstPassword.Password = "";
                txtsecondPassword.Password = "";
            }
        }
        // Checking the list of users for the username must be unique
        private void Checking_LijstUsers_For_UserName(User user)
        {
            foreach (User u in lijstUsers)
            {
                if (u.Username == user.Username) throw new IncorrectException("This username already exists");
            }
            lijstUsers.Add(user);
        }
        #endregion
        #region XML bestand
        // Creating an XML file
        private void xmlBestandAanmaken()
        {
            dc.CreateXMLFile(lijstUsers);
        }
        #endregion
        #region taskbar buttons
        private void btnHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow(dc);
            window.Title = "Project2022";
            window.WindowState = WindowState.Maximized;
            window.Show();
            this.Close();
        }
        private void btnCloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnMinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void btnMaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized) this.WindowState = WindowState.Normal;
            else if (this.WindowState != WindowState.Maximized) this.WindowState = WindowState.Maximized;
        }
        #endregion
    }
}
