
using BL_Projectwerk.Interfaces;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Managers
{
    public class AddressManager
    {
        private IAddressRepository addressRepo;
        private ICompanyRepository companyRepo;

        public AddressManager(IAddressRepository addressRepo, ICompanyRepository companyRepo)
        {
            this.addressRepo = addressRepo;
            this.companyRepo = companyRepo;
        }

        public bool ExistAddress(Address address) { // Even toegevoegd voor probeersel mock test.
            return addressRepo.AddressExistsWithoutId(address);
        }

        public int AddAddress(Address address)
        {
            try
            {
                if (address == null) throw new AddressManagerException("AddressManager - AddAddress - Address is null");
                if (!addressRepo.AddressExistsWithoutId(address)) return addressRepo.AddAddress(address);
                else return addressRepo.GetAddressId(address); 
            }
            catch (Exception ex)
            {
                throw new AddressManagerException("AddAddress", ex);
            }
        }

        //public void DeleteAddress(int addressId)
        //{
        //    try
        //    {
        //        if (!companyRepo.CompanyExistsWithId()
        //        {
        //            throw new AddressManagerException("AddressManager - DeleteAddress - Address does not exist");
        //        }
        //        else if (!companyRepo.CompaniesPresentAtAddress(addressId))
        //        {
        //            throw new AddressManagerException("AddressManager - DeleteAddress - Not able to delete an address when a company is still present");
        //        }
        //        else addressRepo.DeleteAddress(addressId);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new AddressManagerException("DeleteAddress", ex);
        //    }
        //}

        public void UpdateAddress(int id,string? street,string? number,string? postalcode,string? place, string? country)
        {
            try
            {
                //if (adres == null) throw new AdresManagerException("UpdateAdres");
                if (!addressRepo.AddressExistsWithId(id)) throw new AddressManagerException("AddressManager - UpdateAddress - Address does not exist");
                addressRepo.UpdateAddress(id,street,number,postalcode,place,country);
            }
            catch (Exception ex)
            {
                throw new AddressManagerException("UpdateAddress", ex);
            }
        }
    }
}
