using BL_Projectwerk.Domein;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Werknemer> werknemersList = new ObservableCollection<Werknemer>();
        private ObservableCollection<Bedrijf> bedrijven = new ObservableCollection<Bedrijf>();
        private ObservableCollection<Bezoeker> bezoekers = new ObservableCollection<Bezoeker>();
        public MainWindow()
        {
            InitializeComponent();

            ListviewBedrijfOpzoeken.ItemsSource = bedrijven;
            ListviewWerknemerOpzoeken.ItemsSource = werknemersList;
            ListviewBezoekerOpzoeken.ItemsSource = bezoekers;
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            WindowCheckHeightBedrijf();
            WindowCheckHeightWerknemer();
            WindowCheckHeightBezoeker();
            WindowCheckHeightBezoek(13, 25);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WindowCheckHeightBezoeker();
                WindowCheckHeightBezoek(20, 40);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                WindowCheckHeightBezoeker();
                WindowCheckHeightBezoek(16, 32);
            }
        }
        private void WindowCheckHeightBedrijf()
        {
            #region Bedrijf Toevoegen
            BedrijfToevoegenToevoegBtn.FontSize = 13;
            BedrijfToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BedrijfToevoegenToevoegBtn.FontSize = 24;
                BedrijfToevoegenToevoegBtn.Height = 50;
            }
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BedrijfToevoegenToevoegBtn.FontSize = 20;
                BedrijfToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Bedrijf Opzoeken
            BedrijfOpvragenVerwijderenBtn.FontSize = 13;
            BedrijfOpvragenVerwijderenBtn.Height = 25;
            BedrijfOpvragenAanpassenBtn.FontSize = 13;
            BedrijfOpvragenAanpassenBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BedrijfOpvragenVerwijderenBtn.Height = 50;
                BedrijfOpvragenVerwijderenBtn.FontSize = 24;
                BedrijfOpvragenAanpassenBtn.Height = 50;
                BedrijfOpvragenAanpassenBtn.FontSize = 24;
            }
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BedrijfOpvragenVerwijderenBtn.Height = 50;
                BedrijfOpvragenVerwijderenBtn.FontSize = 24;
                BedrijfOpvragenAanpassenBtn.Height = 50;
                BedrijfOpvragenAanpassenBtn.FontSize = 24;
            }
            BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
                BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(700);
            else if (this.ActualHeight > 790 && this.ActualWidth > 1500)
                BedrijfOpzoekenGridPanelColumn0.Width = new GridLength(250);
            #endregion
        }
        private void WindowCheckHeightWerknemer()
        {
            #region Werknemer Toevoegen
            WerknemerToevoegenToevoegBtn.FontSize = 13;
            WerknemerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WerknemerToevoegenToevoegBtn.FontSize = 24;
                WerknemerToevoegenToevoegBtn.Height = 50;
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                WerknemerToevoegenToevoegBtn.FontSize = 24;
                WerknemerToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Werknemer Opzoeken
            WerknemerOpvragenVerwijderenBtn.FontSize = 13;
            WerknemerOpvragenVerwijderenBtn.Height = 25;
            WerknemerOpvragenAanpassenBtn.FontSize = 13;
            WerknemerOpvragenAanpassenBtn.Height = 25;
            WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                WerknemerOpvragenVerwijderenBtn.FontSize = 24;
                WerknemerOpvragenVerwijderenBtn.Height = 50;
                WerknemerOpvragenAanpassenBtn.FontSize = 24;
                WerknemerOpvragenAanpassenBtn.Height = 50;
                WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                WerknemerOpvragenVerwijderenBtn.FontSize = 24;
                WerknemerOpvragenVerwijderenBtn.Height = 50;
                WerknemerOpvragenAanpassenBtn.FontSize = 24;
                WerknemerOpvragenAanpassenBtn.Height = 50;
                WerknemerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            #endregion
        }
        private void WindowCheckHeightBezoeker()
        {
            #region Bezoeker Toevoegen
            BezoekerToevoegenGridPanelColumn0.Width = new GridLength(200);
            BezoekerToevoegenToevoegBtn.FontSize = 13;
            BezoekerToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekerToevoegenToevoegBtn.FontSize = 24;
                BezoekerToevoegenToevoegBtn.Height = 50;
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekerToevoegenToevoegBtn.FontSize = 24;
                BezoekerToevoegenToevoegBtn.Height = 50;
            }
            #endregion
            #region Bezoeker Opzoeken
            BezoekerOpvragenVerwijderenBtn.FontSize = 13;
            BezoekerOpvragenVerwijderenBtn.Height = 25;
            BezoekerOpvragenAanpassenBtn.FontSize = 13;
            BezoekerOpvragenAanpassenBtn.Height = 25;
            BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(300);
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekerOpvragenVerwijderenBtn.FontSize = 24;
                BezoekerOpvragenVerwijderenBtn.Height = 50;
                BezoekerOpvragenAanpassenBtn.FontSize = 24;
                BezoekerOpvragenAanpassenBtn.Height = 50;
                BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekerOpvragenVerwijderenBtn.FontSize = 24;
                BezoekerOpvragenVerwijderenBtn.Height = 50;
                BezoekerOpvragenAanpassenBtn.FontSize = 24;
                BezoekerOpvragenAanpassenBtn.Height = 50;
                BezoekerOpzoekenGridPanelColumn0.Width = new GridLength(500);
            }
            #endregion
        }
        private void WindowCheckHeightBezoek(double TextBlock, double TextBox)
        {
            #region Bezoek Toevoegen
            BezoekToevoegenTextBlockEmail.FontSize = TextBlock;
            BezoekToevoegenTextBlockNaam.FontSize = TextBlock;
            BezoekToevoegenTextBlockBedrijfNaam.FontSize = TextBlock;
            BezoekToevoegenTextBlockContactPersson.FontSize = TextBlock;
            BezoekToevoegenTextBlockBedrijfNaamBezoeker.FontSize = TextBlock;
            BezoekToevoegenGridPanelColumn0.Width = new GridLength(160);
            BezoekToevoegenGridPanelColumn1.Width = new GridLength(150);
            BezoekToevoegenTextBoxEmail.FontSize = TextBlock;
            BezoekToevoegenTextBoxNaam.FontSize = TextBlock;
            BezoekToevoegenTextBoxVoornaam.FontSize = TextBlock;
            BezoekToevoegenComboBoxBedrijfNaam.FontSize = TextBlock;
            BezoekToevoegenComboBoxContactPersoon.FontSize = TextBlock;
            BezoekToevoegenTextBoxBedrijfNaamBezoeker.FontSize = TextBlock;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900)
            {
                BezoekToevoegenGridPanelColumn0.Width = new GridLength(250);
                BezoekToevoegenGridPanelColumn1.Width = new GridLength(250);
            }
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
            {
                BezoekToevoegenGridPanelColumn0.Width = new GridLength(200);
                BezoekToevoegenGridPanelColumn1.Width = new GridLength(150);
            }
            BezoekToevoegenTextBoxEmail.Height = TextBox;
            BezoekToevoegenTextBoxNaam.Height = TextBox;
            BezoekToevoegenTextBoxVoornaam.Height = TextBox;
            BezoekToevoegenComboBoxBedrijfNaam.Height = TextBox;
            BezoekToevoegenComboBoxContactPersoon.Height = TextBox;
            BezoekToevoegenTextBoxBedrijfNaamBezoeker.Height = TextBox;
            BezoekToevoegenToevoegBtn.FontSize = TextBlock;
            BezoekToevoegenToevoegBtn.Height = 25;
            if (this.ActualHeight > 1000 && this.ActualWidth > 1900) BezoekToevoegenToevoegBtn.Height = 50;
            if (this.ActualHeight > 790 && this.ActualWidth > 1500)
                BezoekToevoegenToevoegBtn.Height = 50;
            #endregion
        }
        private void TopRowBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                RadioButton radioButton = (RadioButton)sender;
                TopRowBtnExtras();
                if (radioButton.Name == "BedrijvenBtn")
                {
                    BedrijfPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BedrijfToevoegenBtn.Opacity = 1;
                    BedrijfOpzoekenBtn.Opacity = 1;
                    ContractToevoegenBtn.Opacity = 1;
                    ContractOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "WerknemersBtn")
                {
                    WerknemerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    WerknemerToevoegenBtn.Opacity = 1;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekersBtn")
                {
                    BezoekerPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BezoekerToevoegenBtn.Opacity = 1;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekenBtn")
                {
                    BezoekenPanel.Visibility = Visibility.Visible;
                    TopSecondDP.Background = Brushes.Gray;
                    BezoekenToevoegenBtn.Opacity = 1;
                    BezoekenOpzoekenBtn.Opacity = 1;
                }
            }
        }
        private void TopRowBtnExtras()
        {
            BedrijfPanel.Visibility = Visibility.Hidden;
            WerknemerPanel.Visibility = Visibility.Hidden;
            BezoekerPanel.Visibility = Visibility.Hidden;
            BezoekenPanel.Visibility = Visibility.Hidden;
            BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            BezoekToevoegenGridPanel.Visibility = Visibility.Collapsed;
            BedrijfToevoegenBtn.IsChecked = false;
            BedrijfOpzoekenBtn.IsChecked = false;
            ContractToevoegenBtn.IsChecked = false;
            ContractOpzoekenBtn.IsChecked = false;
            WerknemerToevoegenBtn.IsChecked = false;
            WerknemerOpzoekenBtn.IsChecked = false;
            BezoekerToevoegenBtn.IsChecked = false;
            BezoekerOpzoekenBtn.IsChecked = false;
            BezoekenToevoegenBtn.IsChecked = false;
            BezoekenOpzoekenBtn.IsChecked = false;
            TopSecondDP.Background = Brushes.LightGray;
        }
        private void TopRowBedrijfBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                RadioButton radioButton = (RadioButton)sender;
                TopRowBedrijfBtnExtras(radioButton.Name);
                if (radioButton.Name == "BedrijfToevoegenBtn")
                {
                    BedrijfToevoegenGridPanel.Visibility = Visibility.Visible;
                    BedrijfToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BedrijfOpzoekenBtn")
                {
                    BedrijfOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BedrijfOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "ContractToevoegenBtn")
                {
                    ContractToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "ContractOpzoekenBtn")
                {
                    ContractOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "WerknemerToevoegenBtn")
                {
                    WerknemerToevoegenGridPanel.Visibility = Visibility.Visible;
                    WerknemerToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "WerknemerOpzoekenBtn")
                {
                    WerknemerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    WerknemerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekerToevoegenBtn")
                {
                    BezoekerToevoegenGridPanel.Visibility = Visibility.Visible;
                    BezoekerToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekerOpzoekenBtn")
                {
                    BezoekerOpzoekenGridPanel.Visibility = Visibility.Visible;
                    BezoekerOpzoekenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekenToevoegenBtn")
                {
                    BezoekToevoegenGridPanel.Visibility = Visibility.Visible;
                    BezoekenToevoegenBtn.Opacity = 1;
                }
                else if (radioButton.Name == "BezoekenOpzoekenBtn")
                {
                    BezoekenOpzoekenBtn.Opacity = 1;
                }
            }
        }
        private void TopRowBedrijfBtnExtras(string name)
        {
            if (name != "BedrijfToevoegenBtn")
            {
                BedrijfToevoegenBtn.Opacity = 0.5;
                BedrijfToevoegenBtn.IsChecked = false;
                BedrijfToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BedrijfOpzoekenBtn")
            {
                BedrijfOpzoekenGridPanel.Visibility = Visibility.Collapsed;
                BedrijfOpzoekenBtn.IsChecked = false;
                BedrijfOpzoekenBtn.Opacity = 0.5;
            }
            if (name != "ContractToevoegenBtn")
            {
                ContractToevoegenBtn.Opacity = 0.5;
                ContractToevoegenBtn.IsChecked = false;
            }
            if (name != "ContractOpzoekenBtn")
            {
                ContractOpzoekenBtn.Opacity = 0.5;
                ContractOpzoekenBtn.IsChecked = false;
            }
            if (name != "WerknemerToevoegenBtn")
            {
                WerknemerToevoegenBtn.Opacity = 0.5;
                WerknemerToevoegenBtn.IsChecked = false;
                WerknemerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "WerknemerOpzoekenBtn")
            {
                WerknemerOpzoekenBtn.Opacity = 0.5;
                WerknemerOpzoekenBtn.IsChecked = false;
                WerknemerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekerToevoegenBtn")
            {
                BezoekerToevoegenBtn.Opacity = 0.5;
                BezoekerToevoegenBtn.IsChecked = false;
                BezoekerToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekerOpzoekenBtn")
            {
                BezoekerOpzoekenBtn.Opacity = 0.5;
                BezoekerOpzoekenBtn.IsChecked = false;
                BezoekerOpzoekenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekenToevoegenBtn")
            {
                BezoekenToevoegenBtn.Opacity = 0.5;
                BezoekenToevoegenBtn.IsChecked = false;
                BezoekToevoegenGridPanel.Visibility = Visibility.Collapsed;
            }
            if (name != "BezoekenOpzoekenBtn")
            {
                BezoekenOpzoekenBtn.Opacity = 0.5;
                BezoekenOpzoekenBtn.IsChecked = false;
            }
        }
        private void ListviewWerknemerOpzoeken_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListviewWerknemerOpzoeken.SelectedItem is Werknemer)
            {
                Werknemer werknemer = (Werknemer)ListviewWerknemerOpzoeken.SelectedItem;
                WerknemerOpzoekenTextBoxNaam.Text = werknemer.Naam;
                WerknemerOpzoekenTextBoxVoornaam.Text = werknemer.Voornaam;
                WerknemerOpzoekenTextBoxEmail.Text = werknemer.Email;
            }
            if (ListviewBedrijfOpzoeken.SelectedItem is Bedrijf)
            {
                Bedrijf bedrijf = (Bedrijf)ListviewBedrijfOpzoeken.SelectedItem;
                BedrijfOpzoekenTextBoxNaam.Text = bedrijf.Naam;
                BedrijfOpzoekenTextBoxBTW.Text = bedrijf.BTWNummer;
                BedrijfOpzoekenTextBoxTelefoon.Text = bedrijf.Telefoon;
                BedrijfOpzoekenTextBoxEmail.Text = bedrijf.Email;
                BedrijfOpzoekenTextBoxLand.Text = bedrijf.Adres.Land;
                BedrijfOpzoekenTextBoxStraat.Text = bedrijf.Adres.Straat;
                BedrijfOpzoekenTextBoxPostcode.Text = bedrijf.Adres.Postcode;
                BedrijfOpzoekenTextBoxNr.Text = bedrijf.Adres.Nummer;
                BedrijfOpzoekenTextBoxPlaats.Text = bedrijf.Adres.Plaats;
            }
            if (ListviewBezoekerOpzoeken.SelectedItem is Bezoeker)
            {
                Bezoeker bezoeker = (Bezoeker)ListviewBezoekerOpzoeken.SelectedItem;
                BezoekerOpzoekenTextBoxNaam.Text = bezoeker.Naam;
                BezoekerOpzoekenTextBoxVoornaam.Text = bezoeker.Voornaam;
                BezoekerOpzoekenTextBoxEmail.Text = bezoeker.Email;
                BezoekerOpzoekenTextBoxBedrijfNaam.Text = bezoeker.Bedrijf;
            }
        }
        private void WerknemerOpvragenFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(Button))
            {
                Button button = (Button)sender;
                if (button.Name == "BedrijfOpvragenFilterBtn")
                {
                    bedrijven.Clear();
                    Bedrijf bedrijf1 = new Bedrijf(1, "Bosteels brewery", "BE0123123123", "info@example.com");
                    bedrijf1.ZetAdres(new Adres(1, "Bijvoegstraat", "20", "9530", "Eigem", "Belgie"));
                    bedrijf1.ZetTelefoon("0491732014");
                    bedrijven.Add(bedrijf1);
                    Bedrijf bedrijf2 = new Bedrijf(2, "Bosteels Harbor", "BE0123123158", "infoExample@example.com");
                    bedrijf2.ZetAdres(new Adres(2, "Coremareel", "82", "9530", "Eigem", "Belgie"));
                    bedrijf2.ZetTelefoon("0491732123");
                    bedrijven.Add(bedrijf2);
                }
                else if (button.Name == "WerknemerOpvragenFilterBtn")
                {
                    werknemersList.Clear();
                    Werknemer werknemer1 = new Werknemer(1, "Jonssen", "Robrecht", "Hr Consultent");
                    werknemer1.ZetEmail("info@example.com");
                    werknemersList.Add(werknemer1);
                    werknemersList.Add(new Werknemer(2, "Janssen", "Jan", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(3, "Meenens", "Hozee", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(4, "David", "Achmed", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(5, "DeMaire", "Pieter", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(6, "Jonssen", "Robrecht", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(7, "Janssen", "Jan", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(8, "Meenens", "Hozee", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(9, "David", "Achmed", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(10, "DeMaire", "Pieter", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(11, "Jonssen", "Robrecht", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(12, "Janssen", "Jan", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(13, "Meenens", "Hozee", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(14, "David", "Achmed", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(15, "DeMaire", "Pieter", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(16, "Jonssen", "Robrecht", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(17, "Janssen", "Jan", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(18, "Meenens", "Hozee", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(19, "David", "Achmed", "Hr Consultent"));
                    werknemersList.Add(new Werknemer(20, "DeMaire", "Pieter", "Hr Consultent"));
                }
                else if (button.Name == "BezoekerOpvragenFilterBtn")
                {
                    bezoekers.Clear();
                    bezoekers.Add(new Bezoeker(1, "Geeroms", "Jantje", "Jantje.Geeroms@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(2, "Stanton", "Rumaysa", "Rumaysa.Stanton@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(3, "Sims", "Eben", "Eben.Sims@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(4, "Pena", "Hannah", "Hannah.Pena@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(5, "Wall", "Eboni", "Eboni.Wall@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(6, "Rasmussen", "Zayan", "Zayan.Rasmussen@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(7, "Hope", "Adaline", "Adaline.Hope@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(8, "Knights", "Zeeshan", "Zeeshan.Knights@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(9, "Cooley", "Kayleigh", "Kayleigh.Cooley@hotmail.com", "allphi"));
                    bezoekers.Add(new Bezoeker(10, "Baldwin", "David", "David.Baldwin@hotmail.com", "allphi"));
                }
            }
        }
    }
}
