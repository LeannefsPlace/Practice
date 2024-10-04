using LizaItogPractice.Util;
using Microsoft.VisualBasic.ApplicationServices;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
        private int tourid;
        private double cost;

        public RequestForm(Credentials cr, int tourId)
        {
            InitializeComponent();
            credentials = cr;
            this.tourid = tourId;
        }

        private void RequestForm_Load(object sender, EventArgs e)
        {
            //здесь я снова сломался..... Оборачивать все в try catch, конечно, заняло бы столько же времени, сколько и написание этого комментария, но для меня это уже не имеет значения, время относительно, а страдания вечны.

            NpgsqlCommand command = new NpgsqlCommand($"SELECT name FROM tickettype", connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader["name"]);

            reader.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //try
            //{
                NpgsqlCommand commandSec = new NpgsqlCommand($"SELECT cost*cost_modifier as \"totalcost\" FROM tour, tickettype where tour.id = {tourid} and tickettype.name = '{comboBox1.Text}'", connection);
                NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO ticket (user_login, tour_id, type_id, date, locked_cost, status) values ('{credentials.login}', {tourid}, (SELECT id FROM tickettype WHERE name = '{comboBox1.Text}'), '{dateTimePicker1.Value}', '{commandSec.ExecuteScalar().ToString().Replace(",", ".")}', 'pending')", connection);

                command.ExecuteNonQuery();

                this.Close();

            //}
            //catch(Exception ex)
            //{
            //}
}

        private void button2_Click(object sender, EventArgs e)
        {
            NpgsqlCommand command = new NpgsqlCommand($"SELECT cost*cost_modifier as \"totalcost\" FROM tour, tickettype where tour.id = {tourid} and tickettype.name = '{comboBox1.Text}'", connection);
            label2.Text = command.ExecuteScalar().ToString()+" Рублей";
        }
    }
}
