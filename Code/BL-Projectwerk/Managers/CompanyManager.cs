
using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using System.IO;

namespace BL_Projectwerk.Managers {
    public class CompanyManager {
        private ICompanyRepository companyRepo;
        private EmployeecontractManager ECM = null;

        public CompanyManager(ICompanyRepository companyRepo, EmployeecontractManager eCM) {
            this.companyRepo = companyRepo;
            ECM = eCM;
        }

        public void AddCompany(string vatnumber, string name, string email, string? phonenumber, string? country, string? street, string? number, string? postalcode, string? place) {
            //int? addressId = null;
            try {
                Company company = new Company(name, vatnumber, email);
                if (!string.IsNullOrWhiteSpace(country) && !string.IsNullOrWhiteSpace(street) && !string.IsNullOrWhiteSpace(place) && !string.IsNullOrWhiteSpace(postalcode) && !string.IsNullOrWhiteSpace(number)) {
                    Address address = new Address(street, number, postalcode, place, country);
                    company.SetAddress(address);
                }
                if (companyRepo.CompanyExistsWithoutId(vatnumber, name, email)) throw new CompanyManagerException("CompanyManager - AddCompany - Company already exists");
                companyRepo.AddCompany(company);
            } catch (Exception ex) {
                throw new CompanyManagerException("AddCompany", ex);
            }
        } // UPDATED & TESTED

        public void DeleteCompany(Company company, int? addressid) {
            try {
                if (!companyRepo.CompanyExistsWithId(company.Id)) {
                    throw new CompanyManagerException("CompanyManager - DeleteCompany - Company does not exist");
                } else {

                    IReadOnlyList<Employeecontract> contracts = ECM.GetCompanyContracts(company);
                    foreach (Employeecontract EC in contracts) {
                        ECM.DeleteContract(EC);
                    }
                    companyRepo.DeleteCompany(company.Id);
                }
            } catch (Exception ex) {
                throw new CompanyManagerException("DeleteCompany", ex);
            }

        }

        public void UpdateCompany(int id, string? vatnumber, string? name, string? email, string? phonenumber) {
            try {
                if (!companyRepo.CompanyExistsWithId(id)) throw new CompanyManagerException("CompanyManager - Updatecompany - Company is the same");
                companyRepo.UpdateCompany(id, vatnumber, name, email, phonenumber);
            } catch (Exception ex) {
                throw new CompanyManagerException("UpdateCompany", ex);
            }
        }

        public void UpdateCompanyAddress(int id, int addressId) {
            try {
                if (!companyRepo.CompanyExistsWithId(id)) throw new CompanyManagerException("CompanyManager - UpdateCompanyAddress - Company is the same");
                companyRepo.UpdateCompanyAddress(id, addressId);
            } catch (Exception ex) {
                throw new CompanyManagerException("UpdateCompanyAddress", ex);
            }
        }

        public IReadOnlyList<Company> GetCompanies() {
            List<Company> companies;
            try {
                companies = companyRepo.GetCompanies();
                return companies;
            } catch (Exception ex) {
                throw new CompanyManagerException("GetCompanies", ex);
            }
        }

        public IReadOnlyList<Company> SearchCompanies(string? vatnumber, string? name, string? email, string? phonenumber) {
            List<Company> Companies = new List<Company>();
            //List<Bedrijf> Bedrijven2 = new List<Bedrijf>();
            try {
                if (!string.IsNullOrWhiteSpace(vatnumber) || !string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(email) || !string.IsNullOrWhiteSpace(phonenumber)) {
                    Companies.AddRange(companyRepo.SearchCompanies(vatnumber, name, email, phonenumber));
                }
                //foreach (Bedrijf bedrijf in Bedrijven)
                //{
                //    Bedrijven2.Add(bedrijfRepo.GeefBedrijfOpId(bedrijf.Id));
                //}
                return Companies;
            } catch (Exception ex) {
                throw new CompanyManagerException("SearchCompanies", ex);
            }
        }
    }
}
