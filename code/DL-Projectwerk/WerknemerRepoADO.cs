using BL_Projectwerk.Domein;
using DL_Projectwerk.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk {
    internal class WerknemerRepoADO {
        private string connectieString;
        public WerknemerRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }

        public bool BestaatWerknemer(int werknemerId) {
            if (werknemerId <= 0) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - ongeldig werknemerId"); }
            SqlConnection connection = GetConnection();
            string query = "";
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add();

                    SqlDataReader reader = cmd.ExecuteReader();

                } catch (Exception ex) {
                    throw new RepositoryADOException("", ex);
                } finally {
                    connection.Close();
                }
            }
        }

        public void VoegWerknemerToe(Werknemer werknemer) {
            if (werknemer is null) { throw new WerknemerRepoADOException("WernemerRepoADO - VoegWerknemerToe - werknemer is null"); }
            SqlConnection connection = GetConnection();
            string query = "";
            using (SqlCommand cmd = connection.CreateCommand()) {
                try {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add();

                    SqlDataReader reader = cmd.ExecuteReader();

                } catch (Exception ex) {
                    throw new RepositoryADOException("", ex);
                } finally {
                    connection.Close();
                }
            }
        }
    }
}
