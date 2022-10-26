using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DL_Projectwerk
{
    public class AdresRepoADO : IAdresRepository
    {
        private string connectieString;
        public AdresRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool BestaatAdresZonderId(Adres adres)
        {
            SqlConnection conn = GetConnection();
            string query = @"SELECT COUNT(*) FROM dbo.Adres WHERE "
            + "Land = @land AND Postcode = @Postcode AND Plaats = @Plaats AND Straat = @Straat AND Nummer = @Nummer AND IsVerwijderd = 0";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@Land", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Postcode", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Plaats", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Straat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Nummer", SqlDbType.VarChar));

                    cmd.Parameters["@Land"].Value = adres.Land;
                    cmd.Parameters["@Postcode"].Value = adres.Postcode;
                    cmd.Parameters["@Plaats"].Value = adres.Plaats;
                    cmd.Parameters["@Straat"].Value = adres.Straat;
                    cmd.Parameters["@Nummer"].Value = adres.Nummer;

                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("BestaatAdresZonderId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool BestaatAdresMetId(int id)
        {
            SqlConnection conn = GetConnection();
            string query = @"SELECT COUNT(*) FROM dbo.Adres WHERE AdresId = @AdresId AND IsVerwijderd = 0";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@AdresId", SqlDbType.Int));

                    cmd.Parameters["@AdresId"].Value = id;

                    int n = (int)cmd.ExecuteScalar();
                    if (n > 0) return true;
                    else return false;
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("BestaatAdresMetId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public void UpdateAdres(int id,string? straat, string? nummer, string? postcode, string? plaats, string? land)
        {
            SqlConnection conn = GetConnection();

            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    string query = "update Adres set ";

                    if (!string.IsNullOrWhiteSpace(straat))
                    {
                        query += "Straat = @straat";
                        cmd.Parameters.AddWithValue("@straat", straat);
                    }
                    if (!string.IsNullOrWhiteSpace(nummer) && query != "update Adres set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(nummer)) 
                    { 
                        query += "Nummer = @nummer";
                        cmd.Parameters.AddWithValue("@nummer", nummer);
                    }
                    if (!string.IsNullOrWhiteSpace(postcode) && query != "update Adres set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(postcode))
                    {
                        query += "Postcode = @postcode";
                        cmd.Parameters.AddWithValue("@postcode", postcode);
                    }
                    if (!string.IsNullOrWhiteSpace(plaats) && query != "update Adres set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(plaats)) 
                    { 
                        query += "Plaats = @plaats";
                        cmd.Parameters.AddWithValue("@plaats", plaats);
                    }
                    if (!string.IsNullOrWhiteSpace(land) && query != "update Adres set ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(land)) 
                    { 
                        query += "Land = @land";
                        cmd.Parameters.AddWithValue("@land", land);
                    }

                    query += " where AdresId = @adresId";
                    cmd.Parameters.AddWithValue("@adresId", id);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("UpdateAdres");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderAdres(int id)
        {
            SqlConnection conn = GetConnection();
            string query = @"update Adres set IsVerwijderd = 1 where AdresId = @adresid";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@adresid", SqlDbType.Int));

                    cmd.Parameters["@adresid"].Value = id;

                    cmd.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    throw new AdresRepoADOException("VerwijderAdres");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Adres VoegAdresToe(Adres adres)
        {
            SqlConnection conn = GetConnection();
            string query = @"INSERT INTO dbo.Adres (Land,Postcode,Plaats,Straat,Nummer) output Inserted.AdresId VALUES (@Land,@Postcode,@Plaats,@Straat,@Nummer)";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@Land", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Postcode", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Plaats", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Straat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Nummer", SqlDbType.VarChar));


                    cmd.Parameters["@Land"].Value = adres.Land;
                    cmd.Parameters["@Postcode"].Value = adres.Postcode;
                    cmd.Parameters["@Plaats"].Value = adres.Plaats;
                    cmd.Parameters["@Straat"].Value = adres.Straat;
                    cmd.Parameters["@Nummer"].Value = adres.Nummer;


                    int nieuwId = (int)cmd.ExecuteScalar();
                    Adres nieuwAdres = new Adres(nieuwId, adres.Straat, adres.Nummer, adres.Postcode, adres.Plaats, adres.Land); //TODO ZETID
                    return nieuwAdres;
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("VoegAdresToe");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public Adres GeefAdresMetId(Adres adres)
        {
            SqlConnection conn = GetConnection();
            string query = @"SELECT AdresId FROM dbo.Adres WHERE "
            + "Land = @land AND Postcode = @Postcode AND Plaats = @Plaats AND Straat = @Straat AND Nummer = @Nummer";
            using (SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new SqlParameter("@Land", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Postcode", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Plaats", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Straat", SqlDbType.VarChar));
                    cmd.Parameters.Add(new SqlParameter("@Nummer", SqlDbType.VarChar));

                    cmd.Parameters["@Land"].Value = adres.Land;
                    cmd.Parameters["@Postcode"].Value = adres.Postcode;
                    cmd.Parameters["@Plaats"].Value = adres.Plaats;
                    cmd.Parameters["@Straat"].Value = adres.Straat;
                    cmd.Parameters["@Nummer"].Value = adres.Nummer;

                    int id = (int)cmd.ExecuteScalar();
                    Adres a = new Adres(id, adres.Straat, adres.Nummer, adres.Postcode, adres.Plaats, adres.Land);
                    return a;
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("GeefAdresMetId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
