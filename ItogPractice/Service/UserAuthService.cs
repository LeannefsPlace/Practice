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

        public bool checkCredentials(string username, string password, out string message)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM \"user\" WHERE login = '{username}'", connection);
                if(command.ExecuteScalar() != null && BCrypt.Net.BCrypt.Verify(password, command.ExecuteScalar().ToString()))
                {
                    message = $"Welcome, {username}!";
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

        public bool UserLogin(string username, string password, string fullname, string email, out string message)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM \"user\" WHERE login = '{username}'", connection);
                if (command.ExecuteScalar() == null)
                {
                    message = $"Welcome, {username}!";
                    new NpgsqlCommand($"INSERT INTO \"user\" (login, password, full_name, role_id, email) values ('{username}', '{BCrypt.Net.BCrypt.HashPassword(password)}', '{fullname}', (SELECT id FROM role WHERE name = 'user'), '{email}' )", connection).ExecuteNonQuery();
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

        public bool EditCredentials(string newUsername, string newFullname, string newEmail, out string message)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT password FROM \"user\" WHERE login = '{newUsername}'", connection);
                if (command.ExecuteScalar() != null)
                {
                    message = $"Congradulations, all went as expected, {newUsername}!";
                    new NpgsqlCommand($"UPDATE \"user\" set full_name = '{newFullname}', email = '{newEmail}' WHERE login = '{newUsername}'", connection).ExecuteNonQuery();
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
                message = ex.Message;
                return false;
            }
}
    }
}
