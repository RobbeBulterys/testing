using BL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BL_Projectwerk.Domein {
    public abstract class Person 
    {
        protected Person(int personId, string lastname, string firstname)
        {
            // Persoon uit databank met ID.
            SetId(personId);
            SetLastName(lastname);
            SetFirstName(firstname);
        }

        protected Person(string lastname, string firstname) 
        {
            // Nieuw persoon om toe te voegen aan de databank : databank maakt een ID aan.
            SetLastName(lastname);
            SetFirstName(firstname);
        }

        /*
         * Hieronder overbodige constructors met optionele property email die de repo of UI maar moet toevoegen als extra, anders zijn er te veel contructors. 
        protected Persoon(int persoonId, string naam, string voornaam, string email) {
            // persoon uit databank
            ZetId(persoonId);
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
            ZetEmail(email);
        }

        protected Persoon(string naam, string voornaam, string email) {
            // nieuw persoon voor databank
            ZetNaam(naam);
            ZetVoorNaam(voornaam);
            ZetEmail(email);
        }
        */

        public int PersonId { get; private set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }

        public void SetId(int id) 
        {
            if (id < 1) { throw new PersonException("Person - SetId - Id invalid; Less than 1"); }
            PersonId = id;
        }

        public void SetLastName(string lastname)
        { //een naam kan een spatie bevatten
            if (string.IsNullOrWhiteSpace(lastname)) { throw new PersonException("Person - SetLastName - No lastname data entry"); }
            LastName = lastname.Trim();
        }

        public void SetFirstName(string firstname)
        { 
            if (string.IsNullOrWhiteSpace(firstname)) { throw new PersonException("Person - SetFirstName - No firstname data entry"); }
            firstname = firstname.Trim();
            FirstName = firstname;
        }

        public void SetEmail(string email) 
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new PersonException("Person - SetEmail - No email data entry"); }
            email = email.Trim();
            try 
            {
                if (!Verify.IsValidEmailSyntax(email)) { throw new PersonException("Person - SetEmail - Invalid email"); }
                Email = email;
            } 
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public override string ToString() {
            return $"Person: {LastName} - {FirstName} - {Email}";
        }
    }
}
