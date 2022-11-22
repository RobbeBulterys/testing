using BL_Projectwerk.Exceptions;
using System.IO;

namespace BL_Projectwerk.Domein {
    public class Address 
    {
        public Address(string street, string number, string postalcode, string place, string country) 
        {
            SetStreet(street);
            SetNumber(number);
            SetPostalcode(postalcode);
            SetPlace(place);
            SetCountry(country);
        }

        public Address(int id, string street, string number, string postalcode, string place, string country) 
        {
            // Adres vanuit databank
            SetId(id);
            SetStreet(street);
            SetNumber(number);
            SetPostalcode(postalcode);
            SetPlace(place);
            SetCountry(country);
        }

        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Postalcode { get; set; }
        public string Place { get; set; }
        public string Country { get; set; }

        public void SetId(int id) 
        {
            if (id < 1) { throw new AddressException("Adress - SetId - Id invalid; Less than 1"); }
            Id = id;
        }

        public void SetStreet(string street) 
        {
            if (string.IsNullOrWhiteSpace(street)) { throw new AddressException("Adress - SetStreet - No street data entry"); }
            Street = street.Trim();
        }

        public void SetNumber(string number) 
        {
            if (string.IsNullOrWhiteSpace(number)) { throw new AddressException("Adress - SetNumber - No number data entry"); }
            if (!Verify.IsValidAdressNumberSyntax(number)) { throw new AddressException("Adress - SetNumber - No valid number"); }
            Number = number.Trim();
        }

        public void SetPostalcode(string postalcode) 
        {
            if (string.IsNullOrWhiteSpace(postalcode)) { throw new AddressException("Adress - SetPostalcode - No postalcode data entry"); }
            Postalcode = postalcode.Trim();
        }

        public void SetPlace(string place) 
        {
            if (string.IsNullOrWhiteSpace(place)) { throw new AddressException("Adress - SetPlace - No place data entry"); }
            Place = place.Trim();
        }

        public void SetCountry(string country) 
        {
            if (string.IsNullOrWhiteSpace(country)) { throw new AddressException("Adress - SetCountry - No country data entry"); }
            Country = country.Trim();
        }

        public bool HasSameProperties(object? obj) 
        {
            return obj is Address adress &&
                   Id == adress.Id &&
                   Street == adress.Street &&
                   Number == adress.Number &&
                   Postalcode == adress.Postalcode &&
                   Place == adress.Place &&
                   Country == adress.Country;
        }

        public override string ToString() 
        {
            return $"Address: {Id} - {Street} - {Number} - {Postalcode} - {Place} - {Country}";
        }

        public override int GetHashCode() 
        {
            return HashCode.Combine(Id, Street, Number, Postalcode, Place, Country);
        }

        public override bool Equals(object? obj) 
        {
            return obj is Address address &&
                   Id == address.Id;
        }
    }
}
