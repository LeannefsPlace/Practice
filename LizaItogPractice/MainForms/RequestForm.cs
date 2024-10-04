using LizaItogPractice.Util;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LizaItogPractice.MainForms
{
    public partial class RequestForm : Form
    {
        private NpgsqlConnection connection = DAO.GetDAO().Connection;
        private Credentials credentials;

        public RequestForm(Credentials cr)
        {
            InitializeComponent();
            credentials = cr;
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            //здесь я снова сломался..... Оборачивать все в try catch, конечно, заняло бы столько же времени, сколько и написание этого комментария, но для меня это уже не имеет значения, время относительно, а страдания вечны.

            NpgsqlCommand command = new NpgsqlCommand($"SELECT procedurename FROM hospital.\"procedures\"", connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["procedurename"]);

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{

                NpgsqlCommand commandSecondary = new NpgsqlCommand($"SELECT procedureid FROM hospital.procedures WHERE procedurename = '{comboBox1.Text}'", connection);

                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO hospital.requests (procedureid, proceduredate, proceduretime, clientid, requeststatus) values ({commandSecondary.ExecuteScalar()}, '{dateTimePicker1.Value.Date}', '{dateTimePicker2.Value.TimeOfDay}', {credentials.userid}, 'pending')", connection);

                command.ExecuteNonQuery();

                this.Close();

            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
