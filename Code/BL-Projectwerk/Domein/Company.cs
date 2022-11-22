using BL_Projectwerk.Exceptions;
using System.Text.RegularExpressions;

namespace BL_Projectwerk.Domein
{
    public class Company
    {
        public Company(int id, string name, string vatNumber, string email) : this(name, vatNumber, email)
        {
            SetId(id);
            //Parkeercontract = parkeercontract;
        }
        public Company(string name, string vatNumber, string email)
        {
            SetName(name);
            SetVATNumber(vatNumber);
            SetEmail(email);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string VATNumber { get; set; }
        public Address Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public ParkeerContract Parkeercontract { get; set; }

        public void SetId(int id)
        {
            if (id < 1) { throw new CompanyException("Company - SetId - Id invalid; Less than 1"); }
            Id = id;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { throw new CompanyException("Company - SetName - No name data entry"); }
            Name = name.Trim();
        }

        public void SetVATNumber(string vatNumber)
        {
            if (string.IsNullOrWhiteSpace(vatNumber)) { throw new CompanyException("Company - SetVATNumber - No VATnumber data entry"); } //TODO : Regex controle geldigheid
            vatNumber = vatNumber.Trim();
            if (Verify.IsExistingVATnumber(vatNumber) == false) { throw new CompanyException("Company - SetVATNumber - VATnumber does not exist"); }
            VATNumber = vatNumber;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) { throw new CompanyException("Company - SetEmail - No email data entry"); }
            email = email.Trim();
            if (Verify.IsValidEmailSyntax(email) == false) { throw new CompanyException("Company - SetEmail - email does not exist"); }
            Email = email;
        }

        public void SetPhoneNumber(string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(phonenumber)) { throw new CompanyException("Company - SetPhoneNumber - No phonenumber data entry"); }
            PhoneNumber = phonenumber.Trim();
        }

        public void SetAddress(Address address)
        {
            if (address == null) { throw new CompanyException("Company - SetAddress - No address data entry"); }
            this.Address = address;
        }

        public void ChangeAddress(Address newAddress)
        {
            if (newAddress == null) { throw new CompanyException("Company - ChangeAddress - No address data entry"); }
            if (this.Address.HasSameProperties(newAddress)) { throw new CompanyException("Company - ChangeAddress - Address is the same"); }
            this.Address = newAddress;
        }

        //public IReadOnlyList<Werknemer> GeefWerknemers()
        //{
        //    return _werknemers.Values.ToList().AsReadOnly();
        //}
        //public void VoegWerknemerToe(Werknemer werknemer) { // werknemer die een contract heeft
        //    // We verwachten dat werknemer een contract heeft met dit bedrijf
        //    if (werknemer == null) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - geen werknemer ingevuld"); }
        //    if (_werknemers.ContainsKey(werknemer.PersoonId)) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - werknemer is al toegevoegd aan het bedrijf"); }
        //    if (!werknemer.BevatContractMetBedrijf(this)) { throw new BedrijfException("Bedrijf - VoegWerknemerToe - werknemer bevat geen contract met dit bedrijf"); }
        //    this._werknemers.Add(werknemer.PersoonId, werknemer);
        //}

        //public void StelWerknemerContractOp(Werknemer werknemer, string functie, string? email)
        //{
        //    // Contract opstellen
        //    Werknemercontract wc = new Werknemercontract(this, werknemer, functie);
        //    // Optionele velden toevoegen
        //    if (email != null) {
        //        // Goede syntax van email moet gecontroleerd worden bij email
        //        //if (!Controle.IsGoedeEmailSyntax(email)) { throw new BedrijfException("Bedrijf - StelWerknemerContractOp - geen goede emailsyntax"); }
        //        wc.ZetEmail(email);
        //    } else {
        //        // niets, geen email meegstuurd
        //    }
        //}

        //public void VerwijderWerknemer(int werknemerId)
        //{
        //    if (werknemerId == 0) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - geen werknemer ingevuld"); }
        //    if (!_werknemers.ContainsKey(werknemerId)) { throw new BedrijfException("Bedrijf - VerwijderWerknemer - werknemer bestaat niet"); }
        //    this._werknemers.Remove(werknemerId);
        //}

        public bool HasSameProperties(Company company)
        {
            if (company == null) { throw new CompanyException("Company - HasSameProperties - No company data entry"); }
            if (company.Id != this.Id) return false;
            if (company.Name != this.Name) return false;
            if (company.VATNumber != this.VATNumber) return false;
            if (company.PhoneNumber != this.PhoneNumber) return false;
            if (company.Email != this.Email) return false;
            return true;
        }

        public override string ToString()
        {
            return $"Company: {Id} - {Name} - {VATNumber}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public override bool Equals(object? obj)
        {
            if (obj is Company company)
            {
                if (company.Id == this.Id)
                { // 5 == 5, 0 == 0
                    if (this.Id == 0)
                    {
                        // 0 == 0
                        return HasSameProperties(company); // intern properties controleren
                    } else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}