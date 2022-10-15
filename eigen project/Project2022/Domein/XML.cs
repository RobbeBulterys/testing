using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project2022.Domein
{
    public class XML
    {
        private string filenameProject = "C:\\Users\\Robbe Bulterys\\Desktop\\school 2021-2022\\eigen project\\Project.xml";
        private string filenameProjectEncription = "C:\\Users\\Robbe Bulterys\\Desktop\\school 2021-2022\\eigen project\\Project-Encription.xml";
        public void CreateXMLFile(List<User> users)
        {
            XmlTextWriter writer = new XmlTextWriter(filenameProject, System.Text.Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteComment("Creating an xml file using c#");
            writer.WriteStartElement("Users");
            foreach (User user in users)
            {
                writer.WriteStartElement("User");
                writer.WriteElementString("Firstname", user.FirstName);
                writer.WriteElementString("Lastname", user.LastName);
                writer.WriteElementString("Username", user.Username);
                writer.WriteElementString("Password", user.Password);
                writer.WriteStartElement("Darts");
                writer.WriteStartElement("ScoreTraining");
                foreach (DartsScoreTrainingClass dartsScore in user.DartScoreTrainingScores)
                {
                    writer.WriteElementString("TotalPoints", dartsScore.TotalPoints.ToString());
                    writer.WriteElementString("AveragePoints", dartsScore.AveragePoints.ToString());
                    writer.WriteElementString("TotalOfTurns", dartsScore.TotalOfTurns.ToString());
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
        #region Xml file's uitlezen
        public List<User> XMLProjectFileUitlezen()
        {
            string firstName = "";
            string lastName = "";
            string userName = "";
            string password = "";
            string totalPoints = "";
            string averagePoints = "";
            string totalOfTurns = "";
            List<User> users = new List<User>();
            List<DartsScoreTrainingClass> dartsScores = new List<DartsScoreTrainingClass>();
            XmlReader reader = XmlReader.Create(filenameProject);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "Firstname":
                            firstName = reader.ReadElementContentAsString();
                            break;
                        case "Lastname":
                            lastName = reader.ReadElementContentAsString();
                            break;
                        case "Username":
                            userName = reader.ReadElementContentAsString();
                            break;
                        case "Password":
                            password = reader.ReadElementContentAsString();
                            break;
                        case "TotalPoints":
                            totalPoints = reader.ReadElementContentAsString();
                            break;
                        case "AveragePoints":
                            averagePoints = reader.ReadElementContentAsString();
                            break;
                        case "TotalOfTurns":
                            totalOfTurns = reader.ReadElementContentAsString();
                            break;
                    }
                }
                if (totalOfTurns != "")
                {
                    dartsScores.Add(new DartsScoreTrainingClass(double.Parse(totalPoints), double.Parse(averagePoints), int.Parse(totalOfTurns)));
                    totalPoints = "";
                    averagePoints = "";
                    totalOfTurns = "";
                }
                if (password != "" && reader.Name == "User")
                {
                    List<DartsScoreTrainingClass> copyDartsScores = new List<DartsScoreTrainingClass>();
                    dartsScores.ForEach(a => copyDartsScores.Add(a));
                    users.Add(new User(firstName, lastName, userName, password, copyDartsScores));
                    firstName = "";
                    lastName = "";
                    userName = "";
                    password = "";
                    dartsScores.Clear();
                }
            }
            reader.Close();
            return users;
        }
        public Dictionary<string, int> XMLProjectEncriptionFileUitlezen()
        {
            Dictionary<string, int> numbersDictionary = new Dictionary<string, int>();
            string EncriptionKey = "";
            string EncriptionValue = "";
            XmlReader reader = XmlReader.Create(filenameProjectEncription);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    switch (reader.Name)
                    {
                        case "EncriptionKey":
                            EncriptionKey = reader.ReadElementContentAsString();
                            break;
                        case "EncriptionValue":
                            EncriptionValue = reader.ReadElementContentAsString();
                            break;
                    }
                }
                if (EncriptionValue != "")
                {
                    numbersDictionary.Add(EncriptionKey, int.Parse(EncriptionValue));
                    EncriptionKey = "";
                    EncriptionValue = "";
                }
            }
            reader.Close();
            return numbersDictionary;
        }
        #endregion
    }
}
