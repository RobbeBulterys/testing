﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL_Projectwerk
{
    internal class BezoekRepoADO
    {
        private string connectieString;
        public BezoekRepoADO(string connectieString) {
            this.connectieString = connectieString;
        }
        private SqlConnection GetConnection() {
            return new SqlConnection(connectieString);
        }
    }
}