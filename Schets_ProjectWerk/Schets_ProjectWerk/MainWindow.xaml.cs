using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
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
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowCheckHeight(13, 25);
            if (this.ActualHeight != 450)
            {
                if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WindowCheckHeight(20, 40);
                if (this.ActualHeight > 790 && this.ActualWidth > 1500) WindowCheckHeight(16, 32);
            }
        }
        private void WindowCheckHeight(double TextBlock, double TextBox)
        {
            #region Bedrijf Toevoegen
            BedrijfToevoegenTextBlockNaam.FontSize = TextBlock;
            BedrijfToevoegenTextBlockBTW.FontSize = TextBlock;
            BedrijfToevoegenTextBlockTelefoon.FontSize = TextBlock;
            BedrijfToevoegenTextBlockEmail.FontSize = TextBlock;
            BedrijfToevoegenTextBlockLand.FontSize = TextBlock;
            BedrijfToevoegenTextBlockstraat.FontSize = TextBlock;
            BedrijfToevoegenTextBlockPostcode.FontSize = TextBlock;
            BedrijfToevoegenTextBoxNaam.Height = TextBox;
            BedrijfToevoegenTextBoxBTW.Height = TextBox;
            BedrijfToevoegenTextBoxTelefoon.Height = TextBox;
            BedrijfToevoegenTextBoxEmail.Height = TextBox;
            BedrijfToevoegenTextBoxLand.Height = TextBox;
            BedrijfToevoegenTextBoxstraat.Height = TextBox;
            BedrijfToevoegenTextBoxNr.Height = TextBox;
            BedrijfToevoegenTextBoxPostcode.Height = TextBox;
            BedrijfToevoegenTextBoxPlaats.Height = TextBox;
            BedrijfToevoegenTextBoxNaam.FontSize = TextBlock;
            BedrijfToevoegenTextBoxBTW.FontSize = TextBlock;
            BedrijfToevoegenTextBoxTelefoon.FontSize = TextBlock;
            BedrijfToevoegenTextBoxEmail.FontSize = TextBlock;
            BedrijfToevoegenTextBoxLand.FontSize = TextBlock;
            BedrijfToevoegenTextBoxstraat.FontSize = TextBlock;
            BedrijfToevoegenTextBoxNr.FontSize = TextBlock;
            BedrijfToevoegenTextBoxPostcode.FontSize = TextBlock;
            BedrijfToevoegenTextBoxPlaats.FontSize = TextBlock;
            BedrijfToevoegenToevoegBtn.FontSize = TextBlock;
            BedrijfToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BedrijfToevoegenToevoegBtn.Height = 50;
            #endregion
            #region Bedrijf Opzoeken
            BedrijfOpzoekenTextBlockNaam.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockBTW.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockTelefoon.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockEmail.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockLand.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockStraat.FontSize = TextBlock;
            BedrijfOpzoekenTextBlockPostcode.FontSize = TextBlock;
            BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(250);
            BedrijfOpzoekenGridPanelColumn1.Width = new GridLength(200);
            BedrijfOpzoekenTextBoxNaam.Height = TextBox;
            BedrijfOpzoekenTextBoxBTW.Height = TextBox;
            BedrijfOpzoekenTextBoxTelefoon.Height = TextBox;
            BedrijfOpzoekenTextBoxEmail.Height = TextBox;
            BedrijfOpzoekenTextBoxLand.Height = TextBox;
            BedrijfOpzoekenTextBoxStraat.Height = TextBox;
            BedrijfOpzoekenTextBoxPostcode.Height = TextBox;
            BedrijfOpzoekenTextBoxNr.Height = TextBox;
            BedrijfOpzoekenTextBoxPlaats.Height = TextBox;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(500);
                BedrijfOpzoekenGridPanelColumn1.Width = new GridLength(300);
            }
            BedrijfOpzoekenTextBoxNaam.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxBTW.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxTelefoon.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxEmail.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxLand.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxStraat.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxPostcode.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxNr.FontSize = TextBlock;
            BedrijfOpzoekenTextBoxPlaats.FontSize = TextBlock;
            BedrijfOpvragenVerwijderenBtn.FontSize = TextBlock;
            BedrijfOpvragenVerwijderenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BedrijfOpvragenVerwijderenBtn.Height = 50;
            BedrijfOpvragenAanpassenBtn.FontSize = TextBlock;
            BedrijfOpvragenAanpassenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BedrijfOpvragenAanpassenBtn.Height = 50;
            BedrijfOpvragenFilterBtn.FontSize = TextBlock;
            BedrijfOpvragenFilterBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BedrijfOpvragenFilterBtn.Height = 50;
            #endregion
            #region Werknemer Toevoegen
            WerknemerToevoegenTextBlockNaam.FontSize = TextBlock;
            WerknemerToevoegenTextBlockEmail.FontSize = TextBlock;
            WerknemerToevoegenTextBlockId.FontSize = TextBlock;
            WerknemerToevoegenTextBlockFunctie.FontSize = TextBlock;
            WerknemerToevoegenGridPanelColumn0.Width = new GridLength(150);
            WerknemerToevoegenTextBoxNaam.FontSize = TextBlock;
            WerknemerToevoegenTextBoxVoornaam.FontSize = TextBlock;
            WerknemerToevoegenTextBoxEmail.FontSize = TextBlock;
            WerknemerToevoegenTextBoxId.FontSize = TextBlock;
            WerknemerToevoegenTextBoxFunctie.FontSize = TextBlock;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WerknemerToevoegenGridPanelColumn0.Width = new GridLength(200);
            WerknemerToevoegenTextBoxNaam.Height = TextBox;
            WerknemerToevoegenTextBoxVoornaam.Height = TextBox;
            WerknemerToevoegenTextBoxEmail.Height = TextBox;
            WerknemerToevoegenTextBoxId.Height = TextBox;
            WerknemerToevoegenTextBoxFunctie.Height = TextBox;
            WerknemerToevoegenToevoegBtn.FontSize = TextBlock;
            WerknemerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WerknemerToevoegenToevoegBtn.Height = 50;
            #endregion
            #region Werknemer Opzoeken
            WerknemerOpzoekenTextBlockNaam.FontSize = TextBlock;
            WerknemerOpzoekenTextBlockEmail.FontSize = TextBlock;
            WerknemerOpzoekenTextBlockId.FontSize = TextBlock;
            WerknemerOpzoekenTextBlockFunctie.FontSize = TextBlock;
            WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(200);
            WerknemerOpzoekenGridPanelColumn1.Width = new GridLength(150);
            WerknemerOpzoekenTextBoxNaam.FontSize = TextBlock;
            WerknemerOpzoekenTextBoxVoornaam.FontSize = TextBlock;
            WerknemerOpzoekenTextBoxEmail.FontSize = TextBlock;
            WerknemerOpzoekenTextBoxId.FontSize = TextBlock;
            WerknemerOpzoekenTextBoxFunctie.FontSize = TextBlock;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(500);
                WerknemerOpzoekenGridPanelColumn1.Width = new GridLength(300);
            }
            WerknemerOpzoekenTextBoxNaam.Height = TextBox;
            WerknemerOpzoekenTextBoxVoornaam.Height = TextBox;
            WerknemerOpzoekenTextBoxEmail.Height = TextBox;
            WerknemerOpzoekenTextBoxId.Height = TextBox;
            WerknemerOpzoekenTextBoxFunctie.Height = TextBox;
            WerknemerOpvragenVerwijderenBtn.FontSize = TextBlock;
            WerknemerOpvragenVerwijderenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WerknemerOpvragenVerwijderenBtn.Height = 50;
            WerknemerOpvragenAanpassenBtn.FontSize = TextBlock;
            WerknemerOpvragenAanpassenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WerknemerOpvragenAanpassenBtn.Height = 50;
            WerknemerOpvragenFilterBtn.FontSize = TextBlock;
            WerknemerOpvragenFilterBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) WerknemerOpvragenFilterBtn.Height = 50;
            #endregion
            #region Bezoeker Toevoegen
            BezoekerToevoegenTextBlockNaam.FontSize = TextBlock;
            BezoekerToevoegenTextBlockEmail.FontSize = TextBlock;
            BezoekerToevoegenTextBlockBedrijfNaam.FontSize = TextBlock;
            BezoekerToevoegenGridPanelColumn0.Width = new GridLength(150);
            BezoekerToevoegenTextBoxNaam.FontSize = TextBlock;
            BezoekerToevoegenTextBoxVoornaam.FontSize = TextBlock;
            BezoekerToevoegenTextBoxEmail.FontSize = TextBlock;
            BezoekerToevoegenTextBoxBedrijfNaam.FontSize = TextBlock;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekerToevoegenGridPanelColumn0.Width = new GridLength(200);
            BezoekerToevoegenTextBoxNaam.Height = TextBox;
            BezoekerToevoegenTextBoxVoornaam.Height = TextBox;
            BezoekerToevoegenTextBoxEmail.Height = TextBox;
            BezoekerToevoegenTextBoxBedrijfNaam.Height = TextBox;
            BezoekerToevoegenToevoegBtn.FontSize = TextBlock;
            BezoekerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekerToevoegenToevoegBtn.Height = 50;
            #endregion
            #region Bezoeker Opzoeken
            BezoekerOpzoekenTextBlockNaam.FontSize = TextBlock;
            BezoekerOpzoekenTextBlockEmail.FontSize = TextBlock;
            BezoekerOpzoekenTextBlockBedrijfNaam.FontSize = TextBlock;
            BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(200);
            BezoekerOpzoekenGridPanelColumn1.Width = new GridLength(150);
            BezoekerOpzoekenTextBoxNaam.FontSize = TextBlock;
            BezoekerOpzoekenTextBoxVoornaam.FontSize = TextBlock;
            BezoekerOpzoekenTextBoxEmail.FontSize = TextBlock;
            BezoekerOpzoekenTextBoxBedrijfNaam.FontSize = TextBlock;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(500);
                BezoekerOpzoekenGridPanelColumn1.Width = new GridLength(250);
            }
            BezoekerOpzoekenTextBoxNaam.Height = TextBox;
            BezoekerOpzoekenTextBoxVoornaam.Height = TextBox;
            BezoekerOpzoekenTextBoxEmail.Height = TextBox;
            BezoekerOpzoekenTextBoxBedrijfNaam.Height = TextBox;
            BezoekerOpvragenVerwijderenBtn.FontSize = TextBlock;
            BezoekerOpvragenVerwijderenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekerOpvragenVerwijderenBtn.Height = 50;
            BezoekerOpvragenAanpassenBtn.FontSize = TextBlock;
            BezoekerOpvragenAanpassenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekerOpvragenAanpassenBtn.Height = 50;
            BezoekerOpvragenFilterBtn.FontSize = TextBlock;
            BezoekerOpvragenFilterBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekerOpvragenFilterBtn.Height = 50;
            #endregion
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
