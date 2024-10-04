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

        private void MainForm_Load(object sender, EventArgs e)
        {
            update();
        }

        private void fillTable(DataGridView view, string text)
        {
            if (view.Rows.Count > 0)
                foreach (DataGridViewRow row in view.Rows)
                {
                    row.Cells["Buttons"].Value = text;
                }
        }

        private void update()
        {
            try
            {
                NpgsqlCommand command = new NpgsqlCommand($"SELECT id as \"номер\", name as \"Тур\", description as \"Описание\", max_capacity as \"Количество мест\", cost as \"Стоимость\", cost_raise_per_day as \"Наценка за день\" FROM tour", connection);
                DataTable dt = new DataTable();
                new NpgsqlDataAdapter(command).Fill(dt);
                dataGridView1.Columns.Clear();
                dataGridView1.DataSource = dt;
                dataGridView1.Update();
                DataGridViewColumn column = new DataGridViewButtonColumn();
                column.Name = "Buttons";
                dataGridView1.Columns.Add(column);

                fillTable(dataGridView1, "запись");

                NpgsqlCommand commandTwo = new NpgsqlCommand($"SELECT ticket.id as \"номер\", name as \"Тур\", date as \"дата\", locked_cost as \"Стоимость тура\" FROM ticket, tour WHERE ticket.tour_id = Tour.id and ticket.status != 'ended' and user_login = '{credentials.login}'", connection);

                DataTable dtTwo = new DataTable();
                new NpgsqlDataAdapter(commandTwo).Fill(dtTwo);
                dataGridView3.Columns.Clear();
                dataGridView3.DataSource = dtTwo;
                dataGridView3.Update();
                DataGridViewColumn columnTwo = new DataGridViewButtonColumn();
                columnTwo.Name = "Buttons";
                dataGridView3.Columns.Add(columnTwo);

                fillTable(dataGridView3, "отмена");

                dataGridView3.Update();

                NpgsqlCommand commandThree = new NpgsqlCommand($"SELECT ticket.id as \"номер\", name as \"Тур\", date as \"дата\", locked_cost as \"Стоимость тура\" FROM ticket, tour WHERE ticket.tour_id = Tour.id and ticket.status = 'ended' and user_login = '{credentials.login}'", connection);

                DataTable dtThree = new DataTable();
                new NpgsqlDataAdapter(commandThree).Fill(dtThree);
                dataGridView2.Columns.Clear();
                dataGridView2.DataSource = dtThree;
                dataGridView2.Update();
            }
            catch (Exception ex)
            {

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            update();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                RequestForm form = new RequestForm(credentials, int.Parse(dataGridView1.Rows[e.RowIndex].Cells["номер"].Value.ToString()));
                form.ShowDialog();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                NpgsqlCommand commandThree = new NpgsqlCommand($"DELETE FROM ticket WHERE id = {dataGridView3.Rows[e.RowIndex].Cells["номер"].Value}", connection);
                commandThree.ExecuteNonQuery();
                update();
            }
        }
    }
}
