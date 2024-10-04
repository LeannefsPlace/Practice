using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;

namespace LizaItogPractice.Util
{
    public class Credentials
    {
        public string password;
        public string login;
        public string fullname;
        public string role;
        public string email;
        public Credentials(string userid) 
        {
            //С каждым разом открывать Visual Studio все морально труднее

            NpgsqlConnection connection = DAO.GetDAO().Connection;
            NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM \"user\", \"role\" WHERE login = '{userid}' and role_id = id", connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                password = reader["password"].ToString();
                login = reader["login"].ToString();
                fullname = reader["full_name"].ToString();
                email = reader["email"].ToString();
                role = reader["name"].ToString();
            }

            reader.Close();
        }
    }
}
