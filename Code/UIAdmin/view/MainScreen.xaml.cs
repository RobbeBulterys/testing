using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Managers;
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
using System.Windows.Shapes;
using UIAdmin.Domein;

namespace UIAdmin.view
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : Window
    {
        private bool _isMaximized = false;
        private AdresManager _adresManager;
        private BedrijfManager _bedrijfManager;
        private BezoekerManager _bezoekerManager;
        private WerknemerManager _werknemerManager;
        private BezoekManager _bezoekManager;
        private WerknemercontractManager _werknemercontractManager;
        private ObservableCollection<Bedrijf> bedrijven = new ObservableCollection<Bedrijf>();
        private ObservableCollection<Bezoeker> bezoekers = new ObservableCollection<Bezoeker>();
        private ObservableCollection<Werknemer> werknemersList = new ObservableCollection<Werknemer>();
        private ObservableCollection<Bezoek> bezoeken = new ObservableCollection<Bezoek>();
        private ObservableCollection<ContractVoorLijst> contracts = new ObservableCollection<ContractVoorLijst>();
        public MainScreen(AdresManager adresManager, BedrijfManager bedrijfManager, BezoekerManager bezoekerManager, WerknemerManager werknemerManager, WerknemercontractManager werknemercontract, BezoekManager bezoekManager)
        {
            InitializeComponent();

            BedrijvenDataGrid.ItemsSource = bedrijven;
            BezoekerDataGrid.ItemsSource = bezoekers;
            WerknemerDataGrid.ItemsSource = werknemersList;
            BezoekDataGrid.ItemsSource = contracts;
            HomeBtn.IsChecked = true;
            _adresManager = adresManager;
            _bedrijfManager = bedrijfManager;
            _bezoekerManager = bezoekerManager;
            _werknemerManager = werknemerManager;
            _bezoekManager = bezoekManager;
            _werknemercontractManager = werknemercontract;
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
        private void MenuButtonsBtn_Checked(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() == typeof(RadioButton))
            {
                BedrijfBorder.Visibility = Visibility.Collapsed;
                BezoekerBorder.Visibility = Visibility.Collapsed;
                WerknemerBorder.Visibility = Visibility.Collapsed;
                BezoekBorder.Visibility = Visibility.Collapsed;
                RadioButton button = (RadioButton)sender;
                if (button.Name == "BedrijvenBtn")
                {
                    BedrijfBorder.Visibility = Visibility.Visible;
                }
                if (button.Name == "BezoekersBtn")
                {
                    BezoekerBorder.Visibility = Visibility.Visible;
                }
                if (button.Name == "WerknemersBtn")
                {
                    WerknemerBorder.Visibility = Visibility.Visible;
                }
                if (button.Name == "BezoekenBtn")
                {
                    BezoekBorder.Visibility = Visibility.Visible;
                }
            }
        }
        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #region Bedrijf
        private void BedrijfSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            bedrijven.Clear();
            if (!string.IsNullOrWhiteSpace(BedrijfSearchBox.Text))
            {
                if (BedrijfSearchComboBox.Text == "Bedrijfsnaam")
                {
                    try
                    {
                        List<Bedrijf> b = _bedrijfManager.ZoekBedrijven(null, BedrijfSearchBox.Text, null, null).ToList();
                        foreach (Bedrijf bedrijf in b)
                        {
                            bedrijven.Add(bedrijf);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if(BedrijfSearchComboBox.Text == "BTW-nummer")
                {
                    try
                    {
                        List<Bedrijf> b = _bedrijfManager.ZoekBedrijven(BedrijfSearchBox.Text, null, null, null).ToList();
                        foreach (Bedrijf bedrijf in b)
                        {
                            bedrijven.Add(bedrijf);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (BedrijfSearchComboBox.Text == "Telefoon")
                {
                    try
                    {
                        List<Bedrijf> b = _bedrijfManager.ZoekBedrijven(null, null, null, BedrijfSearchBox.Text).ToList();
                        foreach (Bedrijf bedrijf in b)
                        {
                            bedrijven.Add(bedrijf);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (BedrijfSearchComboBox.Text == "Email")
                {
                    try
                    {
                        List<Bedrijf> b = _bedrijfManager.ZoekBedrijven(null, null, BedrijfSearchBox.Text, null).ToList();
                        foreach (Bedrijf bedrijf in b)
                        {
                            bedrijven.Add(bedrijf);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
            }
            else
            {
                List<Bedrijf> b = _bedrijfManager.GeefBedrijven().ToList();
                foreach (Bedrijf bedrijf in b)
                {
                    bedrijven.Add(bedrijf);
                }
            }
        }
        private void BedrijfAddBedrijf_Click(object sender, RoutedEventArgs e)
        {
            AddBedrijfScreen screen = new AddBedrijfScreen(_adresManager, _bedrijfManager);
            screen.Show();
        }
        private void BedrijfGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (BedrijvenDataGrid.SelectedItem.GetType() == typeof(Bedrijf))
            {
                Bedrijf b = (Bedrijf)BedrijvenDataGrid.SelectedItem;
                EditBedrijfScreen screen = new EditBedrijfScreen(_adresManager, _bedrijfManager, _werknemercontractManager, _bezoekManager, b);
                screen.Show();
            }
        }
        private void BedrijvenDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (BedrijvenDataGrid.SelectedItems.Count == 1)
            //{
            //    if (BedrijvenDataGrid.SelectedItem.GetType() == typeof(Bedrijf))
            //    {
            //        Bedrijf b = (Bedrijf)BedrijvenDataGrid.SelectedItem;
            //        MessageBox.Show($"{b.Id}");
            //    }
            //}
        }
        private void BedrijfGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BedrijvenDataGrid.SelectedItem.GetType() == typeof(Bedrijf))
            {
                Bedrijf b = (Bedrijf)BedrijvenDataGrid.SelectedItem;
                if (b.Adres != null) _bedrijfManager.VerwijderBedrijf(b, b.Adres.Id);
                else _bedrijfManager.VerwijderBedrijf(b, null);
                bedrijven.Clear();
            }
        }
        #endregion
        #region Bezoeker
        private void BezoekerAddBezoeker_Click(object sender, RoutedEventArgs e)
        {
            AddBezoekerScreen screen = new AddBezoekerScreen(_bezoekerManager);
            screen.Show();
        }
        private void BezoekerSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            bezoekers.Clear();
            if (!string.IsNullOrWhiteSpace(BezoekerSearchBox.Text))
            {
                if (BezoekerSearchComboBox.Text == "Naam")
                {
                    try
                    {
                        List<Bezoeker> b = _bezoekerManager.ZoekBezoekers(BezoekerSearchBox.Text, null, null, null).ToList();
                        foreach (Bezoeker bezoeker in b)
                        {
                            bezoekers.Add(bezoeker);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (BezoekerSearchComboBox.Text == "Voornaam")
                {
                    try
                    {
                        List<Bezoeker> b = _bezoekerManager.ZoekBezoekers(null, BezoekerSearchBox.Text, null, null).ToList();
                        foreach (Bezoeker bezoeker in b)
                        {
                            bezoekers.Add(bezoeker);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (BezoekerSearchComboBox.Text == "Bedrijf naam")
                {
                    try
                    {
                        List<Bezoeker> b = _bezoekerManager.ZoekBezoekers(null, null, null, BezoekerSearchBox.Text).ToList();
                        foreach (Bezoeker bezoeker in b)
                        {
                            bezoekers.Add(bezoeker);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (BezoekerSearchComboBox.Text == "Email")
                {
                    try
                    {
                        List<Bezoeker> b = _bezoekerManager.ZoekBezoekers(null, null, BezoekerSearchBox.Text, null).ToList();
                        foreach (Bezoeker bezoeker in b)
                        {
                            bezoekers.Add(bezoeker);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
            }
            else
            {
                List<Bezoeker> b = _bezoekerManager.GeefBezoekers().ToList();
                foreach (Bezoeker bezoeker in b)
                {
                    bezoekers.Add(bezoeker);
                }
            }
        }
        private void BezoekerGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (BezoekerDataGrid.SelectedItem.GetType() == typeof(Bezoeker))
            {
                Bezoeker b = (Bezoeker)BezoekerDataGrid.SelectedItem;
                EditBezoekerScreen screen = new EditBezoekerScreen(_bezoekerManager, _bezoekManager, b);
                screen.Show();
            }
        }
        private void BezoekerGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BezoekerDataGrid.SelectedItem.GetType() == typeof(Bezoeker))
            {
                Bezoeker b = (Bezoeker)BezoekerDataGrid.SelectedItem;
                _bezoekerManager.VerwijderBezoeker(b);
                bezoekers.Clear();
            }
        }
        #endregion
        #region Werknemer
        private void WerknemerAddWerknemer_Click(object sender, RoutedEventArgs e)
        {
            AddWerknemerScreen screen = new AddWerknemerScreen(_werknemerManager);
            screen.Show();
        }
        private void WerknemerSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            werknemersList.Clear();
            if (!string.IsNullOrWhiteSpace(WerknemerSearchBox.Text))
            {
                if (WerknemerSearchComboBox.Text == "Naam")
                {
                    try
                    {
                        List<Werknemer> w = _werknemerManager.ZoekWerknemers(WerknemerSearchBox.Text, null).ToList();
                        foreach (Werknemer werknemer in w)
                        {
                            werknemersList.Add(werknemer);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
                else if (WerknemerSearchComboBox.Text == "Voornaam")
                {
                    try
                    {
                        List<Werknemer> w = _werknemerManager.ZoekWerknemers(null, WerknemerSearchBox.Text).ToList();
                        foreach (Werknemer werknemer in w)
                        {
                            werknemersList.Add(werknemer);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}");
                    }
                }
            }
            else
            {
                List<Werknemer> w = _werknemerManager.GeefAlleWerknemers().ToList();
                foreach (Werknemer werknemer in w)
                {
                    werknemersList.Add(werknemer);
                }
            }

        }
        private void WerknemerGridEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (WerknemerDataGrid.SelectedItem.GetType() == typeof(Werknemer))
            {
                Werknemer w = (Werknemer)WerknemerDataGrid.SelectedItem;
                EditWerknemerScreen screen = new EditWerknemerScreen(_werknemerManager, _werknemercontractManager, _bezoekManager, w);
                screen.Show();
            }
        }
        private void WerknemerGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (WerknemerDataGrid.SelectedItem.GetType() == typeof(Werknemer))
            {
                Werknemer w = (Werknemer)WerknemerDataGrid.SelectedItem;
                try
                {
                    _werknemerManager.VerwijderWerknemer(w.PersoonId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"error!, {ex}");
                }
                werknemersList.Clear();
            }
        }
        #endregion
        #region Bezoek
        private void BezoekAddBezoek_Click(object sender, RoutedEventArgs e)
        {
            AddBezoekScreen screen = new AddBezoekScreen(_bezoekerManager, _bedrijfManager, _werknemercontractManager, _bezoekManager);
            screen.Show();
        }
        private void BezoekSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            contracts.Clear();
            if (!string.IsNullOrWhiteSpace(BezoekSearchBox.Text))
            {
                if (BezoekSearchComboBox.Text == "Bedrijf")
                {
                    List<Bedrijf> bedrijvenList = _bedrijfManager.GeefBedrijven().ToList();
                    foreach (Bedrijf bedrijf in bedrijvenList)
                    {
                        if (bedrijf.Naam == BezoekSearchBox.Text)
                        {
                            List<Bezoek> b = _bezoekManager.ZoekBezoeken(null, bedrijf, null, null).ToList();
                            foreach (Bezoek bezoek in b)
                            {
                                contracts.Add(new ContractVoorLijst(bezoek));
                            }
                            break;
                        }
                    }
                }
            }
            else if (BezoekSearchComboBox.Text == "Today")
            {
                List<Bezoek> b = _bezoekManager.ZoekBezoeken(null, null, null, $"{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList();
                foreach (Bezoek bezoek in b)
                {
                    contracts.Add(new ContractVoorLijst(bezoek));
                }
            }
            else
            {
                List<Bezoek> b = _bezoekManager.GeefBezoeken().ToList();
                foreach (Bezoek bezoek in b)
                {
                    contracts.Add(new ContractVoorLijst(bezoek));
                }
            }
        }
        private void BezoekGridEditButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BezoekGridRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (BezoekDataGrid.SelectedItem.GetType() == typeof(Bezoek))
            {
                Bezoek b = (Bezoek)BezoekDataGrid.SelectedItem;
                MessageBox.Show($"{b.BezoekId}");
                bezoeken.Clear();
            }
        }
        #endregion
    }
}
