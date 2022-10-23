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

namespace UIBezoeker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Login.BorderThickness = new Thickness(5);
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");
            LoginText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#010C1A");

        }
        private void Login_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Aanmelden(Login, Logout, LoginView, LogoutView, LoginText, LogoutText);
            welcome.Text = "Aanmelden";
        }

        private void Logout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logout.BorderThickness = new Thickness(5);
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");
            LogoutText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#010C1A");
        }

        private void Logout_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Aanmelden(Logout, Login, LogoutView, LoginView, LogoutText, LoginText);
            welcome.Text = "Afmelden";
        }

        

        private void Aanmelden(Border ButtonPressed, Border ButtonOther, Grid ShowGrid, Grid HideGrid, TextBlock Pressed, TextBlock NotPressed)
        {
            ButtonPressed.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#5bc3ff");
            Pressed.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFFFFF");
            ButtonOther.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            NotPressed.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#02142A");
            ButtonPressed.BorderThickness = new Thickness(0);
            ShowGrid.Visibility = Visibility.Visible;
            HideGrid.Visibility = Visibility.Hidden;
        }

        private void MeldAan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        
                MeldAan.BorderThickness = new Thickness(2);
                MeldAan.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");

        }

        private void MeldAan_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            MeldAan.BorderThickness = new Thickness(1);
            MeldAan.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            
        }

        private void AnnuleerMeldAan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnnuleerMeldAan.BorderThickness = new Thickness(2);
            AnnuleerMeldAan.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");
        }

        private void AnnuleerMeldAan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AnnuleerMeldAan.BorderThickness = new Thickness(1);
            AnnuleerMeldAan.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            LoginView.Visibility = Visibility.Hidden;
            Login.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            LoginText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#02142A");
            welcome.Text = "Welkom op het bedrijvenpark";
        }

        private void MeldAf_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MeldAf.BorderThickness = new Thickness(2);
            MeldAf.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");
        }

        private void MeldAf_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MeldAf.BorderThickness = new Thickness(1);
            MeldAf.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
        }

        private void AnnuleerMeldAf_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AnnuleerMeldAf.BorderThickness = new Thickness(2);
            AnnuleerMeldAf.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FDFD96");
        }

        private void AnnuleerMeldAf_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AnnuleerMeldAf.BorderThickness = new Thickness(1);
            AnnuleerMeldAf.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            LogoutView.Visibility = Visibility.Hidden;
            Logout.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFF017");
            LogoutText.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#02142A");
            welcome.Text = "Welkom op het bedrijvenpark";
        }


    }
    
}
