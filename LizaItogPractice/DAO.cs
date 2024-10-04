using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace LizaItogPractice
{
    public class DAO
    {
        private Npgsql.NpgsqlConnection connection;
        private static DAO singleton;
        
        public Npgsql.NpgsqlConnection Connection {
            get => connection;
        }

        public static DAO GetDAO() => singleton;

        public DAO(String connectionString) 
        {
            connection = new NpgsqlConnection(connectionString);  
            connection.Open();
            singleton = this;
        }

        ~DAO() { 
            connection.Close();
        }
    }
}
