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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project2022.Domein;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DomeinController dc = new DomeinController();
        List<User> users = new List<User>();
        public MainWindow(DomeinController _dc)
        {
            InitializeComponent();
            dc = _dc;
            users = dc.XMLFileUitlezen();
        }
        #region Loging in
        // Loging in button
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtUserName.Text == "") throw new IncorrectException("username cannot be empty");
                User loginUser = Checking_On_Username();
                Checking_On_Password(loginUser);
                lblErrorMessage.Content = "";
                MainScreen mainScreen = new MainScreen(dc, loginUser);
                mainScreen.Title = loginUser.Username;
                mainScreen.WindowState = WindowState.Maximized;
                mainScreen.Show();
                this.Close();
            } catch(IncorrectException f)
            {
                lblErrorMessage.Content = f.Message;
                txtPassWord.Password = "";
            }
        }
        private User Checking_On_Username()
        {
            foreach(User u in users)
            {
                if (u.Username == dc.CorrectNaming(txtUserName.Text))
                {
                    return u;
                }
            }
            throw new IncorrectException("Username Doesn't exist");
        }
        private Boolean Checking_On_Password(User user)
        {
            if (txtPassWord.Password == "") throw new IncorrectException("Password cannot be empty");
            if (user.Password == dc.Encription(txtPassWord.Password)) return true;
            throw new IncorrectException("Password is incorrect");
        }
        #endregion
        // opening the window to creating an new account
        private void btnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            CreateNewAccount createNewAccountWindow = new CreateNewAccount(dc, users);
            createNewAccountWindow.Title = "Create an new account";
            createNewAccountWindow.WindowState = WindowState.Maximized;
            createNewAccountWindow.Show();
            this.Close();
        }
    }
}
