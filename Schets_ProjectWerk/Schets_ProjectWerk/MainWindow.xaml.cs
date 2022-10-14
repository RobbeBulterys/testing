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
                BedrijvenBtn.Opacity = 0.5;
                WerknemersBtn.Opacity = 0.5;
                BezoekersBtn.Opacity = 0.5;
                BedrijfPanel.Visibility = Visibility.Hidden;
                BedrijfToevoegenGridPanel.Visibility = Visibility.Hidden;
                TopSecondDP.Background = Brushes.LightGray;
                if (button.Name == "BedrijvenBtn")
                {
                    BedrijfPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BedrijvenBtn.Opacity = 1;
                    BedrijfToevoegenBtn.Opacity = 1;
                    BedrijfVerwijderenBtn.Opacity = 1;
                    BedrijfAanpassenBtn.Opacity = 1;
                    ContractToevoegenBtn.Opacity = 1;
                    ContractVerwijderenBtn.Opacity = 1;
                    ContractAanpassenBtn.Opacity = 1;
                } 
                else if (button.Name == "WerknemersBtn")
                    WerknemersBtn.Opacity = 1;
                else if (button.Name == "BezoekersBtn")
                    BezoekersBtn.Opacity = 1;
            }
        }
        private void TopRowBedrijfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                BedrijfToevoegenBtn.Opacity = 0.5;
                BedrijfVerwijderenBtn.Opacity = 0.5;
                BedrijfAanpassenBtn.Opacity = 0.5;
                ContractToevoegenBtn.Opacity = 0.5;
                ContractVerwijderenBtn.Opacity = 0.5;
                ContractAanpassenBtn.Opacity = 0.5;
                BedrijfToevoegenGridPanel.Visibility = Visibility.Hidden;
                if (button.Name == "BedrijfToevoegenBtn")
                {
                    BedrijfToevoegenGridPanel.Visibility = Visibility.Visible;
                    BedrijfToevoegenBtn.Opacity = 1;
                }
                else if (button.Name == "BedrijfVerwijderenBtn")
                    BedrijfVerwijderenBtn.Opacity = 1;
                else if (button.Name == "BedrijfAanpassenBtn")
                    BedrijfAanpassenBtn.Opacity = 1;
                else if (button.Name == "ContractToevoegenBtn")
                    ContractToevoegenBtn.Opacity = 1;
                else if (button.Name == "ContractVerwijderenBtn")
                    ContractVerwijderenBtn.Opacity = 1;
                else if (button.Name == "ContractAanpassenBtn")
                    ContractAanpassenBtn.Opacity = 1;
            }
        }
    }
}
