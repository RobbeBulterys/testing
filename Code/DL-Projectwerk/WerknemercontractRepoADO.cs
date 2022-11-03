using BL_Projectwerk.Domein;
using BL_Projectwerk.Exceptions;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using Microsoft.IdentityModel.Xml;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk {
    public class WerknemercontractRepoADO : IWerknemercontractRepository {
        private string connectieString;
        public WerknemercontractRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }

        public bool BestaatContract(int werknemerId, int bedrijfsId) {
            if (werknemerId <= 0 || bedrijfsId <= 0) { throw new WerknemercontractRepoADOException("BestaatContract - werknemer- of bedrijfsId niet ongeldig"); }

            SqlConnection connection = GetConnection();
            string query = "SELECT COUNT(*) FROM Werknemercontract WHERE WerknemerId=@werknemerId AND BedrijfsId=@bedrijfsId AND IsVerwijderd=0;";
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@werknemerId", werknemerId);
                    cmd.Parameters.AddWithValue("@bedrijfsId", bedrijfsId);

                    return (int)cmd.ExecuteScalar() > 0 ? true : false; // Nog eens ternary operator opfrissen :)

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("Bestaatcontract", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public Werknemercontract GeefContract(Werknemer werknemer, Bedrijf bedrijf) {
            if (werknemer.PersoonId <= 0 || bedrijf.Id <= 0) { throw new WerknemercontractRepoADOException("GeefContract - id's werknemer of bedrijf ongeldig"); }
            string query = "SELECT  WerknemerId, BedrijfsId, Functie, Email FROM Werknemercontract WHERE BedrijfsId=@bedrijfsid AND WerknemerId=@werknemerId AND IsVerwijderd=0;";

            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@bedrijfsId", bedrijf.Id);
                    cmd.Parameters.AddWithValue("@werknemerId", werknemer.PersoonId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        string functie = (string)reader["Functie"];

                        Werknemercontract contract = new Werknemercontract(bedrijf, werknemer, functie);

                        if (!(reader["Email"] == DBNull.Value)) {
                            string email = (string)reader["Email"];
                            contract.ZetEmail(email);
                        }

                        return contract;
                    }
                    return null; // komen we normaal niet, tenzij er geen contract is

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("GeefContractenVanBedrijf", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public IEnumerable<Werknemercontract> GeefContractenVanBedrijf(Bedrijf bedrijf) {
            // controles
            if (bedrijf == null) { throw new WerknemercontractRepoADOException("GeefContractenVanBedrijf - bedrijf niet ingevuld"); }
            if (bedrijf.Id <= 0) { throw new WerknemercontractRepoADOException("GeefContractenVanBedrijf - bedrijfsId ongeldig"); }

            // nodig
            List<Werknemercontract> contracten = new List<Werknemercontract>();

            // query
            string query = "SELECT  wc.WerknemerId, wc.Functie, wc.Email, w.Naam, w.Voornaam FROM Werknemercontract wc LEFT JOIN Werknemer w ON w.WerknemerId=wc.WerknemerId WHERE BedrijfsId=@bedrijfsid AND wc.IsVerwijderd=0;";

            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@bedrijfsId", bedrijf.Id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        int werknemerId = (int)reader["WerknemerId"];
                        string functie = (string)reader["Functie"];
                        string naam = (string)reader["Naam"];
                        string voornaam = (string)reader["Voornaam"];

                        Werknemer werker = new Werknemer(werknemerId, naam, voornaam);
                        Werknemercontract contract = new Werknemercontract(bedrijf, werker, functie);

                        if (!(reader["Email"] == DBNull.Value)) {
                            string email = (string)reader["Email"];
                            contract.ZetEmail(email);
                        }

                        contracten.Add(contract);
                    }
                    return contracten;

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("GeefContractenVanBedrijf", ex);
                } finally {
                    connection.Close();
                }
            }
        } // Ready

        public IEnumerable<Werknemercontract> GeefContractenVanWerknemer(Werknemer werknemer) {
            // controles
            if (werknemer == null) { throw new WerknemercontractRepoADOException("GeefContractenVanWerknemer - werknemer niet ingevuld"); }
            if (werknemer.PersoonId <= 0) { throw new WerknemercontractRepoADOException("GeefContractenVanWerknemer - werknemerId ongeldig"); }

            // nodig
            List<Werknemercontract> contracten = new List<Werknemercontract>();

            // query
            string query = "SELECT wc.BedrijfsId, wc.Functie, wc.Email AS EmailWerknemer, b.Naam, b.BTWnummer, b.Email AS EmailBedrijf, b.Telefoon, a.AdresId, a.Land, a.Postcode, a.Plaats, a.Straat, a.Nummer FROM Werknemercontract wc LEFT JOIN Bedrijf b ON b.BedrijfsId=wc.BedrijfsId LEFT JOIN Adres a ON b.AdresId=a.AdresId WHERE WerknemerId=@werknemerId AND wc.IsVerwijderd=0;";

            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@werknemerId", werknemer.PersoonId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        // verplichte velden
                        int bedrijfsId = (int)reader["BedrijfsId"];
                        string functie = (string)reader["Functie"];
                        string naam = (string)reader["Naam"];
                        string btwNummer = (string)reader["BTWnummer"];
                        string emailBedrijf = (string)reader["EmailBedrijf"];

                        Bedrijf bedrijf = new Bedrijf(bedrijfsId, naam, btwNummer, emailBedrijf);
                        Werknemercontract contract = new Werknemercontract(bedrijf, werknemer, functie);

                        // optionele gegevens (emailWerknemer contract, telefoon bedrijf en adres bedrijf)
                        if (reader["EmailWerknemer"] != DBNull.Value) {
                            string email = (string)reader["EmailWerknemer"];
                            contract.ZetEmail(email);
                        }
                        if (reader["Telefoon"] != DBNull.Value) {
                            string telefoon = (string)reader["Telefoon"];
                            bedrijf.ZetTelefoon(telefoon);
                        }
                        if (reader["AdresId"] != DBNull.Value) {
                            int adresId = (int)reader["AdresId"];
                            string land = (string)reader["Land"];
                            string postcode = (string)reader["Postcode"];
                            string plaats = (string)reader["Plaats"];
                            string straat = (string)reader["Straat"];
                            string nummer = (string)reader["Nummer"];
                            Adres a = new Adres(adresId, straat, nummer, postcode, plaats, land);
                            bedrijf.ZetAdres(a);
                        }
                        contracten.Add(contract);
                    }
                    return contracten;

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("GeefContractenVanWerknemer", ex);
                } finally {
                    connection.Close();
                }
            }
        } //ready


        public void UpdateContract(int werknemerId, int bedrijfsId, string? functie, string? email) {
            if (werknemerId <= 0 || bedrijfsId <= 0) { throw new WerknemercontractRepoADOException("UpdateContract - werknemerid of bedrijfsid ongeldig"); }
            if (string.IsNullOrWhiteSpace(functie) && string.IsNullOrWhiteSpace(email)) { throw new WerknemercontractRepoADOException("UpdateContract - functie en email beiden niet ingevuld"); }

            // functie of email is dus ingevuld
            string query = "UPDATE Werknemercontract SET ";

            SqlConnection conn = GetConnection();

            using (SqlCommand cmd = conn.CreateCommand()) {
                try {
                    conn.Open();

                    // kijken wat er in de query bijkomt
                    if (!string.IsNullOrWhiteSpace(functie)) {
                        query += "Functie=@functie";
                        cmd.Parameters.AddWithValue("@functie", functie);
                        cmd.Parameters.Add("@funct", System.Data.SqlDbType.NVarChar);
                    }

                    if (!string.IsNullOrWhiteSpace(email) && query != "UPDATE Werknemercontract SET ") { query += ", "; } // Als er al eerder een parameter (functie) is ingevuld, dan voegen we een ", " spatie toe voor meerdere updates
                    if (!string.IsNullOrWhiteSpace(email)) {
                        query += "Email=@email";
                        cmd.Parameters.AddWithValue("@email", email);
                    }


                    query += " WHERE IsVerwijderd=0 AND WerknemerId=@werknemersId AND BedrijfsId=@bedrijfsId";

                    cmd.Parameters.AddWithValue("@werknemersId", werknemerId);
                    cmd.Parameters.AddWithValue("@bedrijfsId", bedrijfsId);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("UpdateContract", ex);
                } finally {
                    conn.Close();
                }
            }
        }

        public void VerwijderContract(int werknemerId, int bedrijfsId) {

            // query
            string sql = "UPDATE Werknemercontract SET IsVerwijderd=1, ActiefWerkend=0 WHERE WerknemerId=@werknemerId AND BedrijfsId=@bedrijfsId;";

            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@werknemerId", werknemerId);
                    cmd.Parameters.AddWithValue("@bedrijfsId", bedrijfsId);

                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("VerwijderContract", ex);
                } finally {
                    connection.Close();
                }
            }
        } // Ready


        public void VoegContractToe(Werknemercontract contract) {
            // controles
            if (contract == null) { throw new WerknemercontractRepoADOException("VoegContractToe - geen contract ingevuld"); }

            // query
            string sql = "INSERT INTO Werknemercontract (WerknemerId, BedrijfsId, Functie, Email) VALUES (@werknemerId, @bedrijfsId, @functie, @email);";

            SqlConnection connection = GetConnection();
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@werknemerId", contract.Werknemer.PersoonId);
                    cmd.Parameters.AddWithValue("@bedrijfsId", contract.Bedrijf.Id);
                    cmd.Parameters.AddWithValue("@functie", contract.Functie);

                    // Optionele parameters
                    if (contract.Email != null) {
                        cmd.Parameters.AddWithValue("@email", contract.Email);
                    } else {
                        cmd.Parameters.AddWithValue("@email", DBNull.Value);
                    }

                    cmd.ExecuteNonQuery();

                } catch (Exception ex) {
                    throw new WerknemercontractRepoADOException("VoegContractToe", ex);
                } finally {
                    connection.Close();
                }
            }
        } // Ready

        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

    }
}
