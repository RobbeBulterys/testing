﻿using BL_Projectwerk.Domein;
using BL_Projectwerk.Interfaces;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace DL_Projectwerk.repoADO
{
    public class AdresRepoADO : IAdresRepository
    {
        private string connectieString;
        private string dbname = "ID367284_VRS";
        
        public AdresRepoADO(string connectieString)
        {
            this.connectieString = connectieString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectieString);
        }

        public bool BestaatAdresZonderId(Adres adres)
        {
            MySqlConnection conn = GetConnection();
            string query = $@"SELECT COUNT(*) FROM {dbname}.Address WHERE Country = @country AND PostalCode = @postalCode AND City = @city AND Street = @street AND AddressNumber = @number AND IsDeleted = 0";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@country", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@postalCode", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@city", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@street", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@number", MySqlDbType.VarChar));

                    cmd.Parameters["@country"].Value = adres.Land;
                    cmd.Parameters["@postalCode"].Value = adres.Postcode;
                    cmd.Parameters["@city"].Value = adres.Plaats;
                    cmd.Parameters["@street"].Value = adres.Straat;
                    cmd.Parameters["@number"].Value = adres.Nummer;

                    int n = (int)(long)cmd.ExecuteScalar();
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
            MySqlConnection conn = GetConnection();
            string query = $@"SELECT COUNT(*) FROM {dbname}.Address WHERE AddressId = @addressId AND IsDeleted = 0";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@addressId", MySqlDbType.Int32));

                    cmd.Parameters["@addressId"].Value = id;

                    int n = (int)(long)cmd.ExecuteScalar();
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

        public void UpdateAdres(int id, string? straat, string? nummer, string? postcode, string? plaats, string? land)
        {
            // todo controleren of de parameters vertaald zijn, of niet (de blauwe parameters dan)
            MySqlConnection conn = GetConnection();

            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();

                    string query = $"UPDATE {dbname}.Address SET ";

                    if (!string.IsNullOrWhiteSpace(straat))
                    {
                        query += "Street = @street";
                        cmd.Parameters.AddWithValue("@street", straat);
                    }
                    if (!string.IsNullOrWhiteSpace(nummer) && query != $"UPDATE {dbname}.Address SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(nummer))
                    {
                        query += "AddressNumber = @number";
                        cmd.Parameters.AddWithValue("@number", nummer);
                    }
                    if (!string.IsNullOrWhiteSpace(postcode) && query != $"UPDATE {dbname}.Address SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(postcode))
                    {
                        query += "PostalCode = @postalCode";
                        cmd.Parameters.AddWithValue("@postalCode", postcode);
                    }
                    if (!string.IsNullOrWhiteSpace(plaats) && query != $"UPDATE {dbname}.Address SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(plaats))
                    {
                        query += "City = @city";
                        cmd.Parameters.AddWithValue("@city", plaats);
                    }
                    if (!string.IsNullOrWhiteSpace(land) && query != $"UPDATE {dbname}.Address SET ") query += ", ";
                    if (!string.IsNullOrWhiteSpace(land))
                    {
                        query += "Country = @country";
                        cmd.Parameters.AddWithValue("@country", land);
                    }

                    query += " WHERE AddressId = @addressId";
                    cmd.Parameters.AddWithValue("@addressId", id);
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("UpdateAdres", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void VerwijderAdres(int id)
        {
            MySqlConnection conn = GetConnection();
            string query = @"UPDATE Address set IsDeleted = 1 WHERE AddressId = @addressid";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@addressid", MySqlDbType.Int32));

                    cmd.Parameters["@addressid"].Value = id;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new AdresRepoADOException("VerwijderAdres", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int VoegAdresToe(Adres adres)
        {
            MySqlConnection conn = GetConnection();
            string query = $@"INSERT INTO {dbname}.Address (Country, PostalCode, City, Street, AddressNumber) VALUES (@country, @postalCode, @city, @street, @number); SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@country", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@postalCode", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@city", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@street", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@number", MySqlDbType.VarChar));


                    cmd.Parameters["@country"].Value = adres.Land;
                    cmd.Parameters["@postalCode"].Value = adres.Postcode;
                    cmd.Parameters["@city"].Value = adres.Plaats;
                    cmd.Parameters["@street"].Value = adres.Straat;
                    cmd.Parameters["@number"].Value = adres.Nummer;


                    int nieuwId = (int)(long)(ulong)cmd.ExecuteScalar();
                    return nieuwId;
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

        public int GeefAdresId(Adres adres)
        {
            MySqlConnection conn = GetConnection();
            string query = @$"SELECT AddressId FROM {dbname}.Address WHERE "
            + "Country = @country AND PostalCode = @postalCode AND City = @city AND Street = @street AND AddressNumber = @number AND IsDeleted=0";
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;

                    cmd.Parameters.Add(new MySqlParameter("@country", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@postalCode", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@city", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@street", MySqlDbType.VarChar));
                    cmd.Parameters.Add(new MySqlParameter("@number", MySqlDbType.VarChar));

                    cmd.Parameters["@country"].Value = adres.Land;
                    cmd.Parameters["@postalCode"].Value = adres.Postcode;
                    cmd.Parameters["@city"].Value = adres.Plaats;
                    cmd.Parameters["@street"].Value = adres.Straat;
                    cmd.Parameters["@number"].Value = adres.Nummer;

                    int id = (int)cmd.ExecuteScalar();
                    return id;
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
