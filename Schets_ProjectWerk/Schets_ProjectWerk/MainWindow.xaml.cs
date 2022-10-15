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

namespace Schets_ProjectWerk
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

        private void TopRowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                TopRowBtnExtras();
                if (button.Name == "BedrijvenBtn")
                {
                    BedrijfPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BedrijvenBtn.Opacity = 1;
                    BedrijfToevoegenBtn.Opacity = 1;
                    BedrijfOpzoekenBtn.Opacity = 1;
                    ContractToevoegenBtn.Opacity = 1;
                    ContractOpzoekenBtn.Opacity = 1;
                } 
                else if (button.Name == "WerknemersBtn")
                {
                    WerknemerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    WerknemersBtn.Opacity = 1;
                    WerknemerToevoegenBtn.Opacity = 1;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                else if (button.Name == "BezoekersBtn")
                {
                    BezoekerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BezoekersBtn.Opacity = 1;
                    BezoekerToevoegenBtn.Opacity = 1;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
            }
        }
        private void TopRowBtnExtras()
        {
            BedrijvenBtn.Opacity = 0.5;
            WerknemersBtn.Opacity = 0.5;
            BezoekersBtn.Opacity = 0.5;
            BedrijfPanel.Visibility = Visibility.Hidden;
            WerknemerPanel.Visibility = Visibility.Hidden;
            BezoekerPanel.Visibility = Visibility.Hidden;
            BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            TopSecondDP.Background = Brushes.LightGray;
        }
        private void TopRowBedrijfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                TopRowBedrijfBtnExtras();
                if (button.Name == "BedrijfToevoegenBtn")
                {
                    BedrijfToevoegenGridPanel.Visibility = Visibility.Visible;
                    BedrijfToevoegenBtn.Opacity = 1;
                }
                else if (button.Name == "BedrijfOpzoekenBtn")
                {
                    BedrijfOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BedrijfOpzoekenBtn.Opacity = 1;
                }
                else if (button.Name == "ContractToevoegenBtn")
                    ContractToevoegenBtn.Opacity = 1;
                else if (button.Name == "ContractOpzoekenBtn")
                    ContractOpzoekenBtn.Opacity = 1;
                else if (button.Name == "WerknemerToevoegenBtn")
                {
                    WerknemerToevoegenGridPanel.Visibility = Visibility.Visible;
                    WerknemerToevoegenBtn.Opacity = 1;
                }
                else if (button.Name == "WerknemerOpzoekenBtn")
                {
                    WerknemerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                else if (button.Name == "BezoekerToevoegenBtn")
                {
                    BezoekerToevoegenGridPanel.Visibility = Visibility.Visible;
                    BezoekerToevoegenBtn.Opacity = 1;
                }
                else if (button.Name == "BezoekerOpzoekenBtn")
                {
                    BezoekerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
            }
        }
        private void TopRowBedrijfBtnExtras()
        {
            BedrijfToevoegenBtn.Opacity = 0.5;
            BedrijfOpzoekenBtn.Opacity = 0.5;
            BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;

            ContractToevoegenBtn.Opacity = 0.5;
            ContractOpzoekenBtn.Opacity = 0.5;

            WerknemerToevoegenBtn.Opacity = 0.5;
            WerknemerOpzoekenBtn.Opacity = 0.5;
            WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;

            BezoekerToevoegenBtn.Opacity = 0.5;
            BezoekerOpzoekenBtn.Opacity = 0.5;
            BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
        }
    }
}
