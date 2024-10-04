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

namespace LizaItogPractice.MainForms
{
    public partial class ManagerForm : Form
    {
        NpgsqlConnection connection = DAO.GetDAO().Connection;
        public ManagerForm()
        {
            InitializeComponent();
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

        private void fillTableTwo(DataGridView view, string text)
        {
            DataGridViewColumn column = new DataGridViewButtonColumn();
            column.Name = "ButtonsTwo";
            dataGridView1.Columns.Add(column);
            if (view.Rows.Count > 0)
                foreach (DataGridViewRow row in view.Rows)
                {
                    row.Cells["ButtonsTwo"].Value = text;
                }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                NpgsqlCommand commandThree = new NpgsqlCommand($"DELETE FROM \"ticket\" WHERE id = {dataGridView1.Rows[e.RowIndex].Cells[6].Value}", connection);
                commandThree.ExecuteNonQuery();
                update(dataGridView1, "Select * from ticket where status = 'Pending'");
                fillTable(dataGridView1, "Принять");
                fillTableTwo(dataGridView1, "Отклонить");
            }
            if (e.ColumnIndex == 7)
            {
                NpgsqlCommand commandThree = new NpgsqlCommand($"UPDATE \"ticket\" SET status = 'Accepted' WHERE id = {dataGridView1.Rows[e.RowIndex].Cells[6].Value}", connection);
                commandThree.ExecuteNonQuery();
                update(dataGridView1, "Select * from ticket where status = 'Pending'");
                fillTable(dataGridView1, "Принять");
                fillTableTwo(dataGridView1, "Отклонить");
            }
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

        private void ManagerForm_Load(object sender, EventArgs e)
        {
            update(dataGridView1, "Select * from ticket where status = 'Pending'");
            fillTable(dataGridView1, "Принять");
            fillTableTwo(dataGridView1, "Отклонить");

            update(dataGridView2, "Select * from ticket where status != 'Pending'");

            update(dataGridView3, "select tour.name, sum(locked_cost) from ticket, tour where tour.id = tour_id group by tour.name");

            update(dataGridView4, "select \"user\".full_name, sum(locked_cost) from ticket, \"user\" where \"user\".login = ticket.user_login group by \"user\".full_name");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update(dataGridView1, "Select * from ticket where status = 'pending'");
            fillTable(dataGridView1, "Принять");
            fillTableTwo(dataGridView1, "Отклонить");

            update(dataGridView2, "Select * from ticket where status != 'pending'");

            update(dataGridView3, "select tour.name, sum(locked_cost) from ticket, tour where tour.id = tour_id group by tour.name");

            update(dataGridView4, "select \"user\".full_name, sum(locked_cost) from ticket, \"user\" where \"user\".login = ticket.user_login group by \"user\".full_name");
        }
    }
}
