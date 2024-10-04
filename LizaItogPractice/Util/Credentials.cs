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
        public int userid;
        public string password;
        public string login;
        public string fullname;
        public string role;
        public Credentials(int userid) 
        {
            //С каждым разом открывать Visual Studio все морально труднее

            this.userid = userid;

            NpgsqlConnection connection = DAO.GetDAO().Connection;
            NpgsqlCommand command = new NpgsqlCommand($"SELECT * FROM hospital.users, hospital.roles WHERE usersid = {userid} and role = roleid", connection);
            NpgsqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                this.userid = userid;
                this.password = reader[2].ToString();
                this.login = reader[1].ToString();
                this.fullname = reader[3].ToString();
                this.role = reader[6].ToString();
            }

            reader.Close();
        }
    }
}
