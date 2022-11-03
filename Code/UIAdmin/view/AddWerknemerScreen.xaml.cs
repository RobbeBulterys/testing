using BL_Projectwerk.Domein;
using BL_Projectwerk.Managers;
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

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for AddWerknemerScreen.xaml
    /// </summary>
    public partial class AddWerknemerScreen : Window
    {
        private bool _isMaximized = false;
        private WerknemerManager _werknemerManager;
        public AddWerknemerScreen(WerknemerManager werknemerManager)
        {
            InitializeComponent();
            _werknemerManager = werknemerManager;
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
                    string? naam = null;
                    string? voornaam = null;
                    string message = "";
                    if (!string.IsNullOrWhiteSpace(TextBoxNaam.Text)) { naam = TextBoxNaam.Text; message += $"naam => {naam}\n"; }
                    if (!string.IsNullOrWhiteSpace(TextBoxVoorNaam.Text)) { voornaam = TextBoxVoorNaam.Text; message += $"voornaam => {voornaam}\n"; }
                    if (naam == null || voornaam == null) MessageBox.Show("Alle velden moeten worden ingevuld!");
                    else
                    {
                        try
                        {
                            _werknemerManager.VoegWerknemerToe(new Werknemer(naam, voornaam));
                            MessageBox.Show("Succes!");
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
    }
}
