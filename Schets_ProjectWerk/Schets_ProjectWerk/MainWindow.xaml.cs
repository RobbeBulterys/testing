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

        private void BedrijvenBtn_Click(object sender, RoutedEventArgs e)
        {
            BedrijfPanel.Visibility = Visibility.Visible;
            BedrijvenBtn.Opacity = 1;
            WerknemersBtn.Opacity = 0.5;
            BezoekersBtn.Opacity =  0.5;
        }

        private void WerknemersBtn_Click(object sender, RoutedEventArgs e)
        {
            BedrijfPanel.Visibility = Visibility.Hidden;
            BedrijvenBtn.Opacity = 0.5;
            WerknemersBtn.Opacity = 1;
            BezoekersBtn.Opacity = 0.5;
        }

        private void BezoekersBtn_Click(object sender, RoutedEventArgs e)
        {
            BedrijfPanel.Visibility = Visibility.Hidden;
            BedrijvenBtn.Opacity = 0.5;
            WerknemersBtn.Opacity = 0.5;
            BezoekersBtn.Opacity = 1;
        }
    }
}
