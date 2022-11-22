
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface IAddressRepository
    {
        bool AddressExistsWithoutId(Address address);
        bool AddressExistsWithId(int id);
        void UpdateAddress(int id, string? street, string? number, string? postalcode, string? place, string? country);
        void DeleteAddress(int id);
        int AddAddress(Address address);
        int GetAddressId(Address address);
    }
}
