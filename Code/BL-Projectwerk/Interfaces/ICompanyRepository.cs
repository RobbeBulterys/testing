
using BL_Projectwerk.Domein;

namespace BL_Projectwerk.Interfaces
{
    public interface ICompanyRepository
    {
        bool CompanyExistsWithoutId(string vatnumber, string name, string email);
        bool CompanyExistsWithId(int id);
        void UpdateCompanyAddress(int id, int addressId);
        void UpdateCompany(int id, string? vatnumber, string? name, string? email, string? phonenumber);
        void DeleteCompany(int id);
        void AddCompany(Company company);
        List<Company> GetCompanies();
        List<Company> SearchCompanies(string? vatnumber, string? name, string? email, string? phonenumber);
        Company GetCompanyOnId(int id);
        //bool CompaniesPresentAtAddress(int id);
    }
}
