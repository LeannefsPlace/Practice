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
                dgv.Columns.Clear();
                NpgsqlCommand command = new NpgsqlCommand(query, connection);
                DataTable dt = new DataTable();
                new NpgsqlDataAdapter(command).Fill(dt);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
        }

        private void fillTable(DataGridView view, string text)
        {
            DataGridViewColumn column = new DataGridViewButtonColumn();
            column.Name = "Buttons";
            dataGridView1.Columns.Add(column);
            if (view.Rows.Count > 0)
                foreach (DataGridViewRow row in view.Rows)
                {
                    row.Cells["Buttons"].Value = text;
                }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            update(dataGridView1, "SELECT * FROM \"user\"");
            fillTable(dataGridView1, "Удалить");
            update(dataGridView2, "SELECT * FROM \"role\"");
            update(dataGridView3, "SELECT * FROM \"tour\"");
            update(dataGridView4, "SELECT * FROM \"ticket\"");
            update(dataGridView5, "SELECT * FROM \"tickettype\"");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                NpgsqlCommand commandThree = new NpgsqlCommand($"DELETE FROM \"user\" WHERE login = '{dataGridView1.Rows[e.RowIndex].Cells["login"].Value}'", connection);
                commandThree.ExecuteNonQuery();
                update(dataGridView1, "SELECT * FROM \"user\"");
                fillTable(dataGridView1, "Удалить");
            }
        }
    }
}
