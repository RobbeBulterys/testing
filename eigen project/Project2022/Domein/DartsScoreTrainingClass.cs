using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2022.Domein
{
    public class DartsScoreTrainingClass
    {
        private int _totalOfTurns;
        private int _amountOfTurns;
        private double _totalPoints;
        private double _averagePoints;
        private List<double> ListOfPoints;

        public DartsScoreTrainingClass(int amountOfTurns)
        {
            AmountOfTurns = amountOfTurns;
            TotalPoints = 0;
            AveragePoints = 0;
            ListOfPoints = new List<double>();
            TotalOfTurns = amountOfTurns;
        }

        public DartsScoreTrainingClass(double totalPoints, double averagePoints, int amountOfTurns)
        {
            AmountOfTurns = 0;
            TotalPoints = totalPoints;
            AveragePoints = averagePoints;
            ListOfPoints = new List<double>();
            TotalOfTurns = amountOfTurns;
        }

        public int AmountOfTurns { get => _amountOfTurns; set => _amountOfTurns = value; }
        public double TotalPoints { get => _totalPoints; set => _totalPoints = value; }
        public double AveragePoints { get => _averagePoints; set => _averagePoints = value; }
        public int TotalOfTurns { get => _totalOfTurns; set => _totalOfTurns = value; }

        private void GetAveragePoints()
        {
            if (_totalPoints == 0) _averagePoints = 0;
            else
            {
                _averagePoints = (_totalPoints / ListOfPoints.Count);
                _averagePoints = Math.Round(_averagePoints, 2);
            }
        }
        private void GetTotalePoints()
        {
            _totalPoints = 0;
            for (int i = 0; i < ListOfPoints.Count; i++)
            {
                _totalPoints += ListOfPoints[i];
            }
        }

        public void AddPointToList(double point)
        {
            ListOfPoints.Add(point);
            _amountOfTurns--;
            GetTotalePoints();
            GetAveragePoints();
        }
        public void ReturnToPreviousPoint()
        {
            int CountOfListOfPoints = (ListOfPoints.Count - 1);
            List<double> CopyListOfPoints = new List<double>();
            ListOfPoints.ForEach(a => CopyListOfPoints.Add(a));
            if (CopyListOfPoints.Count > 0)
            {
                if (CopyListOfPoints.Count == 1)
                {
                    ListOfPoints.Clear();
                    _amountOfTurns++;
                    GetTotalePoints();
                    GetAveragePoints();
                }
                else
                {
                    ListOfPoints.Clear();
                    for (int i = 0; i < CountOfListOfPoints; i++)
                    {
                        ListOfPoints.Add(CopyListOfPoints[i]);
                    }
                    _amountOfTurns++;
                    GetTotalePoints();
                    GetAveragePoints();
                }
            }
        }
    }
}
