using LizaItogPractice.MainForms;
using LizaItogPractice.Util;
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

namespace LizaItogPractice
{
    public partial class HelloForm : Form
    {
        public HelloForm()
        {
            InitializeComponent();
        }

        public Credentials Cred {  get; set; }
        public bool Authenticated { get; set; }

        private void button1_Click(object sender, EventArgs e)
        {
            FormEnter form = new FormEnter();
            form.ShowDialog();

            if (form.CheckedCredentials)
            {
                Cred = new Credentials(form.Userid);
                this.Hide();
                if (Cred.role == "admin")
                {
                    new AdminForm().ShowDialog();
                }
                else
                {
                    new MainForm(this.Cred).ShowDialog();
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin form = new FormLogin();
            form.ShowDialog();

            if (form.CheckedCredentials)
            {
                Cred = new Credentials(form.Userid);
                this.Hide();

                new MainForm(this.Cred).ShowDialog();
                this.Close();
            }
        }
    }
}
