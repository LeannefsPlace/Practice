using LizaItogPractice.Service;
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
    public partial class MainForm : Form
    {
        private Credentials credentials;
        private UserAuthService userAuthService = new UserAuthService();
        private NpgsqlConnection connection = DAO.GetDAO().Connection;

        public MainForm(Credentials cr)
        {
            InitializeComponent();
            credentials = cr;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg = "Password is incorrect!";

            if (BCrypt.Net.BCrypt.Verify(textBox3.Text, credentials.password))
                userAuthService.EditCredentials(credentials.userid, textBox2.Text, textBox1.Text, out msg);
            label1.Text = msg;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            update();
        }

        private void update()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT procedurename, proceduredate, proceduretime, fullname, requeststatus FROM hospital.requests, hospital.\"procedures\", hospital.users WHERE hospital.requests.procedureid = hospital.\"procedures\".procedureid and usersid = clientid and usersid = {credentials.userid}", connection);
                DataTable dt = new DataTable();
                new NpgsqlDataAdapter(command).Fill(dt);
                dataGridView1.DataSource = dt;
                command = new NpgsqlCommand($"Select * from hospital.\"notifications\" where targetuserid = {credentials.userid}", connection);
                dt = new DataTable();
                new NpgsqlDataAdapter(command).Fill(dt);
                dataGridView2.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            update();
            RequestForm form = new RequestForm(credentials);
            form.ShowDialog();
            update();
        }
    }
}
