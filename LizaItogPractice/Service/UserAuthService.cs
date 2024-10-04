using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace LizaItogPractice.Service
{
    public class UserAuthService
    {
        private NpgsqlConnection connection;
        
        public UserAuthService() {
            connection = DAO.GetDAO().Connection;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"select firstAdmin('{BCrypt.Net.BCrypt.HashPassword("admin")}')", connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

        public bool checkCredentials(string username, string password, out string message, out int userid)
        {
            userid = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM hospital.users WHERE login = '{username}'", connection);
                if(command.ExecuteScalar() != null && BCrypt.Net.BCrypt.Verify(password, command.ExecuteScalar().ToString()))
                {
                    message = $"Welcome, {username}!";
                    userid = int.Parse(new NpgsqlCommand($"SELECT usersid FROM hospital.users WHERE login = '{username}'", connection).ExecuteScalar().ToString());
                    return true;
                }
                else
                {
                    message = $"Credentials invalid or expired!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = $"Connection settings invalid! Please, report it to admin";
                return false;
            }
        }

        public bool UserLogin(string username, string password, string fullname, out string message, out int userid)
        {
            userid = 0;
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM hospital.users WHERE login = '{username}'", connection);
                if (command.ExecuteScalar() == null)
                {
                    message = $"Welcome, {username}!";
                    new NpgsqlCommand($"INSERT INTO hospital.users (login, password, fullname, role) values ('{username}', '{BCrypt.Net.BCrypt.HashPassword(password)}', '{fullname}', (SELECT roleid FROM hospital.roles WHERE rolename = 'user') )", connection).ExecuteNonQuery();
                    userid = int.Parse(new NpgsqlCommand($"SELECT usersid FROM hospital.users WHERE login = '{username}'", connection).ExecuteScalar().ToString());
                    return true;
                }
                else
                {
                    message = $"Credentials invalid or expired!";
                    return false;
                }
            }
            catch (Exception ex)
            {
                message = $"Connection settings invalid! Please, report it to admin";
                return false;
            }
        }

        public bool EditCredentials(int userId, string newUsername, string newFullname, out string message)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM hospital.users WHERE usersid = '{userId}'", connection);
                if (command.ExecuteScalar() != null)
                {
                    message = $"Congradulations, all went as expected, {newUsername}!";
                    new NpgsqlCommand($"UPDATE hospital.users set login = '{newUsername}', fullname = '{newFullname}' WHERE usersid = '{userId}'", connection).ExecuteNonQuery();
                    return true;
                }
                else
                {
                    message = $"Credentials invalid or expired!";
                    return false;
                }
        }
            catch (Exception ex)
            {
                message = $"Connection settings invalid! Please, report it to admin";
                return false;
            }
}
    }
}
