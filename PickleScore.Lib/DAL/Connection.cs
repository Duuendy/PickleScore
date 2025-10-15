using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace PickleScore.Web.DAL
{
    public class Connection
    {
        private readonly string _connectionString;

        public Connection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }

}