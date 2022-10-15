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
    /// Interaction logic for DartsScoreTraining.xaml
    /// </summary>
    public partial class DartsScoreTraining : Window
    {
        private DomeinController dc = new DomeinController();
        private User user = new User();
        private DartsScoreTrainingClass scoreTraining;
        private int AantalBeurten = 0;
        private List<double> _totaleScore = new List<double>();
        public DartsScoreTraining(DomeinController _dc, User _user, int _aantalBeurten)
        {
            InitializeComponent();
            ChangeComponentsSize(24, 36);
            dc = _dc;
            user = _user;
            AantalBeurten = _aantalBeurten;
            ExtraMethode();
        }
        private void ExtraMethode()
        {
            lblAantalBeurten.Content = AantalBeurten;
            lblUsername.Content = user.Username;
            btnPrevious.Content = "Back";
            btnNext.Content = "Next";
            lblScore.Content = "";
            btnNumpadLeft.Content = "<--";
            scoreTraining = new DartsScoreTrainingClass(AantalBeurten);
        }
        #region taskbar buttons
        private void btnHomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainScreen window = new MainScreen(dc, user);
            window.Title = user.Username;
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
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                ChangeComponentsSize(16, 24);
            }
            else if (this.WindowState != WindowState.Maximized)
            {
                this.WindowState = WindowState.Maximized;
                ChangeComponentsSize(24, 36);
            }
        }
        private void ChangeComponentsSize(int size1, int size2)
        {
            lblBeurtenOver.FontSize = size1;
            lblAantalBeurten.FontSize = size2;
            lblTotaleScoreContext.FontSize = size1;
            lblTotalScore.FontSize = size2;
            lbl3DartGem.FontSize = size1;
            lblAverage.FontSize = size2;
            btnPrevious.FontSize = size2;
            btnNext.FontSize = size2;
            lblScore.FontSize = size2;
            btnNumpad.FontSize = size1;
            btnScore.FontSize = size1;
            btnNumpadLeft.FontSize = size2;
            btn0.FontSize = size2;
            btn1.FontSize = size2;
            btn2.FontSize = size2;
            btn3.FontSize = size2;
            btn4.FontSize = size2;
            btn5.FontSize = size2;
            btn6.FontSize = size2;
            btn7.FontSize = size2;
            btn8.FontSize = size2;
            btn9.FontSize = size2;
            btnSingle.FontSize = size2;
            btnDouble.FontSize = size2;
            btnTriple.FontSize = size2;
            btnGridScore1.FontSize = size2;
            btnGridScore2.FontSize = size2;
            btnGridScore3.FontSize = size2;
            btnGridScore4.FontSize = size2;
            btnGridScore5.FontSize = size2;
            btnGridScore6.FontSize = size2;
            btnGridScore7.FontSize = size2;
            btnGridScore8.FontSize = size2;
            btnGridScore9.FontSize = size2;
            btnGridScore10.FontSize = size2;
            btnGridScore11.FontSize = size2;
            btnGridScore12.FontSize = size2;
            btnGridScore13.FontSize = size2;
            btnGridScore14.FontSize = size2;
            btnGridScore15.FontSize = size2;
            btnGridScore16.FontSize = size2;
            btnGridScore17.FontSize = size2;
            btnGridScore18.FontSize = size2;
            btnGridScore19.FontSize = size2;
            btnGridScore20.FontSize = size2;
            btnGridScoreLeft.FontSize = size2;
            btnGridScore25.FontSize = size2;
            btnGridScoreBull.FontSize = size2;
            btnGridScoreMis.FontSize = size2;
            btnGridScoreRight.FontSize = size2;
        }
        #endregion
        #region Numpad
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "1";
        }
        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "2";
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "3";
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "4";
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "5";
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "6";
        }
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "7";
        }
        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "8";
        }
        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "9";
        }
        private void btn0_Click(object sender, RoutedEventArgs e)
        {
            lblScore.Content += "0";
        }
        private void btnNumpadLeft_Click(object sender, RoutedEventArgs e)
        {
            string content = lblScore.Content.ToString();
            lblScore.Content = "";
            for (int i = 0; i < content.Length - 1; i++)
            {
                lblScore.Content += content[i].ToString();
            }
        }
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (lblScore.Content.ToString() != "")
            {
                double point = double.Parse(lblScore.Content.ToString());
                scoreTraining.AddPointToList(point);
                lblAantalBeurten.Content = scoreTraining.AmountOfTurns;
                lblTotalScore.Content = scoreTraining.TotalPoints;
                lblAverage.Content = scoreTraining.AveragePoints;
                lblScore.Content = "";
                AantalBeurtenMethode();
            }
        }
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            scoreTraining.ReturnToPreviousPoint();
            lblAantalBeurten.Content = scoreTraining.AmountOfTurns;
            lblTotalScore.Content = scoreTraining.TotalPoints;
            lblAverage.Content = scoreTraining.AveragePoints;
            lblScore.Content = "";
        }
        private void AantalBeurtenMethode()
        {
            if (lblAantalBeurten.Content.ToString() == "0")
            {
                List<DartsScoreTrainingClass> dartsScores = user.DartScoreTrainingScores;
                dartsScores.Add(scoreTraining);
                user.DartScoreTrainingScores = dartsScores;
                List<User> users = new List<User>();
                foreach (User u in dc.XMLFileUitlezen())
                {
                    if (u.Username == user.Username) users.Add(user);
                    else users.Add(u);
                }
                dc.CreateXMLFile(users);
                DartsMainScreen window = new DartsMainScreen(dc, user);
                window.Title = user.Username;
                window.WindowState = WindowState.Maximized;
                window.Show();
                this.Close();
            }
        }
        #endregion
        #region Button Numpad / Score
        private void btnNumpad_Click(object sender, RoutedEventArgs e)
        {
            btnNumpad.Background = Brushes.CadetBlue;
            btnScore.Background = Brushes.Transparent;
            GridNumpad.Visibility = Visibility.Visible;
            GridScore.Visibility = Visibility.Hidden;
        }
        private void btnScore_Click(object sender, RoutedEventArgs e)
        {
            btnNumpad.Background = Brushes.Transparent;
            btnScore.Background = Brushes.CadetBlue;
            GridNumpad.Visibility = Visibility.Hidden;
            GridScore.Visibility = Visibility.Visible;
            lblScore.Content = "";
        }
        #endregion
        #region Button Single / Double / Triple
        private void BtnScoreBackgroundChange()
        {
            btnSingle.Background = Brushes.Transparent;
            btnDouble.Background = Brushes.Transparent;
            btnTriple.Background = Brushes.Transparent;
            btnSingle.Foreground = Brushes.White;
            btnDouble.Foreground = Brushes.White;
            btnTriple.Foreground = Brushes.White;
        }
        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            BtnScoreBackgroundChange();
            btnSingle.Background = Brushes.LightGray;
            btnSingle.Foreground = Brushes.Blue;
        }
        private void btnDouble_Click(object sender, RoutedEventArgs e)
        {
            BtnScoreBackgroundChange();
            btnDouble.Background = Brushes.LightGray;
            btnDouble.Foreground = Brushes.Blue;
        }
        private void btnTriple_Click(object sender, RoutedEventArgs e)
        {
            BtnScoreBackgroundChange();
            btnTriple.Background = Brushes.LightGray;
            btnTriple.Foreground = Brushes.Blue;
        }
        #endregion

        private void ScoreScoreTelling(int number)
        {
            if (lblFirstArrow.Content.ToString().Contains("Image"))
            {
                int getal = number;
                if (getal == 0)
                {
                    lblFirstArrow.Content = "Miss";
                    _totaleScore.Add(getal);
                }
                else if (getal == 50)
                {
                    lblFirstArrow.Content = "Bull - 50";
                    _totaleScore.Add(getal);
                }
                else if (getal == 25)
                {
                    lblFirstArrow.Content = "25";
                    _totaleScore.Add(getal);
                }
                else if (btnSingle.Foreground == Brushes.Blue)
                {
                    lblFirstArrow.Content = $"S{number} - {number}";
                    _totaleScore.Add(getal);
                } 
                else if (btnDouble.Foreground == Brushes.Blue)
                {
                    lblFirstArrow.Content = $"D{number} - {number * 2}";
                    _totaleScore.Add(getal * 2);
                } 
                else if (btnTriple.Foreground == Brushes.Blue)
                {
                    lblFirstArrow.Content = $"T{number} - {number * 3}";
                    _totaleScore.Add(getal * 3);
                }
            } 
            else if (lblSecondArrow.Content.ToString().Contains("Image"))
            {
                int getal = number;
                if (getal == 0)
                {
                    lblSecondArrow.Content = "Miss";
                    _totaleScore.Add(getal);
                }
                else if (getal == 50)
                {
                    lblSecondArrow.Content = "Bull - 50";
                    _totaleScore.Add(getal);
                }
                else if (getal == 25)
                {
                    lblSecondArrow.Content = "25";
                    _totaleScore.Add(getal);
                }
                else if (btnSingle.Foreground == Brushes.Blue)
                {
                    lblSecondArrow.Content = $"S{number} - {number}";
                    _totaleScore.Add(getal);
                }
                else if (btnDouble.Foreground == Brushes.Blue)
                {
                    lblSecondArrow.Content = $"D{number} - {number * 2}";
                    _totaleScore.Add(getal * 2);
                }
                else if (btnTriple.Foreground == Brushes.Blue)
                {
                    lblSecondArrow.Content = $"T{number} - {number * 3}";
                    _totaleScore.Add(getal * 3);
                }
            } 
            else if (lblthirdArrow.Content.ToString().Contains("Image"))
            {
                int getal = number;
                if (getal == 0)
                {
                    lblthirdArrow.Content = "Miss";
                    _totaleScore.Add(getal);
                }
                else if (getal == 50)
                {
                    lblthirdArrow.Content = "Bull - 50";
                    _totaleScore.Add(getal);
                }
                else if (getal == 25)
                {
                    lblthirdArrow.Content = "25";
                    _totaleScore.Add(getal);
                }
                else if (btnSingle.Foreground == Brushes.Blue)
                {
                    lblthirdArrow.Content = $"S{number} - {number}";
                    _totaleScore.Add(getal);
                }
                else if (btnDouble.Foreground == Brushes.Blue)
                {
                    lblthirdArrow.Content = $"D{number} - {number * 2}";
                    _totaleScore.Add(getal * 2);
                }
                else if (btnTriple.Foreground == Brushes.Blue)
                {
                    lblthirdArrow.Content = $"T{number} - {number * 3}";
                    _totaleScore.Add(getal * 3);
                }
            }
        }
        private void btnGridScore1_Click(object sender, RoutedEventArgs e)
        {
            int number = 0;
            Button button = (Button)sender;
            if (button.Content.ToString() == "Bull") number = 50;
            else if (button.Content.ToString() == "Mis") number = 0;
            else number = int.Parse(button.Content.ToString());
            ScoreScoreTelling(number);
            BtnScoreBackgroundChange();
            btnSingle.Background = Brushes.LightGray;
            btnSingle.Foreground = Brushes.Blue;
        }

        private void btnGridScoreLeft_Click(object sender, RoutedEventArgs e)
        {
            List<double> copyTotaleScore = new List<double>();
            _totaleScore.ForEach(a => copyTotaleScore.Add(a));
            _totaleScore.Clear();
            if (!lblthirdArrow.Content.ToString().Contains("Image"))
            {
                lblthirdArrow.Content = ImageDartsThirdArrow;
                _totaleScore.Add(copyTotaleScore[0]);
                _totaleScore.Add(copyTotaleScore[1]);
            }
            else if (!lblSecondArrow.Content.ToString().Contains("Image"))
            {
                lblSecondArrow.Content = ImageDartsSecondArrow;
                _totaleScore.Add(copyTotaleScore[0]);
            }
            else if (!lblFirstArrow.Content.ToString().Contains("Image")) lblFirstArrow.Content = ImageDartsFirstArrow;
            else if (lblthirdArrow.Content.ToString().Contains("Image") && lblSecondArrow.Content.ToString().Contains("Image") && lblFirstArrow.Content.ToString().Contains("Image"))
            {
                scoreTraining.ReturnToPreviousPoint();
                lblAantalBeurten.Content = scoreTraining.AmountOfTurns;
                lblTotalScore.Content = scoreTraining.TotalPoints;
                lblAverage.Content = scoreTraining.AveragePoints;
            }
        }
        private void btnGridScoreRight_Click(object sender, RoutedEventArgs e)
        {
            if (!lblthirdArrow.Content.ToString().Contains("Image") && !lblSecondArrow.Content.ToString().Contains("Image") && !lblFirstArrow.Content.ToString().Contains("Image"))
            {
                double score = 0;
                _totaleScore.ForEach(a => score += a);
                scoreTraining.AddPointToList(score);
                lblAantalBeurten.Content = scoreTraining.AmountOfTurns;
                lblTotalScore.Content = scoreTraining.TotalPoints;
                lblAverage.Content = scoreTraining.AveragePoints;
                lblFirstArrow.Content = ImageDartsFirstArrow;
                lblSecondArrow.Content = ImageDartsSecondArrow;
                lblthirdArrow.Content = ImageDartsThirdArrow;
                _totaleScore.Clear();
                AantalBeurtenMethode();
            }
        }
    }
}
