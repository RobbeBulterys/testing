using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Project2022.Domein
{
    public class DomeinController
    {
        XML xmlBestand = new XML();
        private Dictionary<string, int> numbersDictionary = new Dictionary<string, int>();
        public DomeinController()
        {

        }
        #region Creating an new account
        // Creating a new account
        public User CreateAccount(string _firstName, string _lastName, string _userName, string _firstPassword, string _secondPassword)
        {
            if (_firstName == "Enter text here..." || _firstName == "") throw new IncorrectException("your firstname cannot be empty");
            if (_lastName == "Enter text here..." || _lastName == "") throw new IncorrectException("your lastname cannot be empty");
            if (_userName == "Enter text here..." || _userName == "") throw new IncorrectException("your username cannot be empty");
            if (_firstPassword == "" || _secondPassword == "") throw new IncorrectException("your password cannot be empty");
            if (_firstPassword != _secondPassword) throw new IncorrectException("your password is incorrect");
            return new User(CorrectNaming(_firstName), CorrectNaming(_lastName), CorrectNaming(_userName), Encription(_firstPassword));
        }
        // Setting the first letter in uppercase
        public string CorrectNaming(string tekst)
        {
            tekst.ToLower();
            string correctText = tekst[0].ToString().ToUpper();
            for (int i = 1; i < tekst.Length; i++)
            {
                correctText += tekst[i];
            }
            return correctText;
        }
        #endregion
        #region Xml bestand
        // Creating an xml file on the basis of a list
        public void CreateXMLFile(List<User> users)
        {
            xmlBestand.CreateXMLFile(users);
        }
        // Reading an xml file and putting data in a list of users
        public List<User> XMLFileUitlezen()
        {
            return xmlBestand.XMLProjectFileUitlezen();
        }
        #endregion
        #region Encription for password
        public string Encription(string password)
        {
            numbersDictionary = xmlBestand.XMLProjectEncriptionFileUitlezen();
            string totalnumberInString = "";
            for(int i = 0; i < password.Length; i++)
            {
                foreach(var kvp in numbersDictionary)
                {
                    if (password[i].ToString() == kvp.Key)
                    {
                        totalnumberInString += kvp.Value;
                    }
                }
            }
            return HexaEncription(totalnumberInString);
        }
        private string HexaEncription(string password)
        {
            decimal stringpassword = decimal.Parse(password);
            string hexadecimal = "";
            Boolean loop = true;
            while (loop)
            {
                if(stringpassword < 16)
                {
                    hexadecimal += HexaNumberEncription(stringpassword);
                    loop = false;
                }
                else
                {
                    decimal number = stringpassword % 16;
                    decimal secondnumber = (stringpassword - number) / 16;
                    stringpassword = secondnumber;
                    hexadecimal += HexaNumberEncription(number);
                }
            }
            return hexadecimal;
        }
        private string HexaNumberEncription(decimal number)
        {
            string hexaNumber = "";
            if (number == 1) hexaNumber = "1";
            if (number == 2) hexaNumber = "2";
            if (number == 3) hexaNumber = "3";
            if (number == 4) hexaNumber = "4";
            if (number == 5) hexaNumber = "5";
            if (number == 6) hexaNumber = "6";
            if (number == 7) hexaNumber = "7";
            if (number == 8) hexaNumber = "8";
            if (number == 9) hexaNumber = "9";
            if (number == 10) hexaNumber = "A";
            if (number == 11) hexaNumber = "B";
            if (number == 12) hexaNumber = "C";
            if (number == 13) hexaNumber = "D";
            if (number == 14) hexaNumber = "E";
            if (number == 15) hexaNumber = "F";
            return hexaNumber;
        }
        #endregion
    }
}
