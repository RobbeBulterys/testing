using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2022.Domein
{
    public class User
    {
        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private List<DartsScoreTrainingClass> _dartScoreTrainingScores;

        public User(string firstName, string lastName, string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            DartScoreTrainingScores = new List<DartsScoreTrainingClass>();
        }
        public User()
        {

        }
        public User(string firstName, string lastName, string username, string password, List<DartsScoreTrainingClass> dartsScores)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            DartScoreTrainingScores = dartsScores;
        }

        public string FirstName { get => _firstName; set => _firstName = value; }
        public string LastName { get => _lastName; set => _lastName = value; }
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public List<DartsScoreTrainingClass> DartScoreTrainingScores { get => _dartScoreTrainingScores; set => _dartScoreTrainingScores = value; }
    }
}
