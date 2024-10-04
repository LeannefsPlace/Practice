using LizaItogPractice.Util;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LizaItogPractice.MainForms
{
    public partial class AdminForm : Form
    {
        private NpgsqlConnection connection = DAO.GetDAO().Connection;

        public AdminForm()
        {
            InitializeComponent();
        }

        private void update(DataGridView dgv, string query)
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                DataTable dt = new DataTable();
                new NpgsqlDataAdapter(command).Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            update(dataGridView1, "select * from hospital.users");
            update(dataGridView2, "select * from hospital.roles");
            update(dataGridView3, "select * from hospital.requests");
            update(dataGridView4, "select * from hospital.\"procedures\"");
            update(dataGridView5, "select * from hospital.notifications");
        }
    }
}
