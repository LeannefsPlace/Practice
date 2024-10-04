using LizaItogPractice.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LizaItogPractice
{
    public partial class FormLogin : Form
    {
        UserAuthService service = new UserAuthService();

        public bool CheckedCredentials
        {
            get; set;
        }

        public string Login
        {
            get; set;
        }

        public FormLogin()
        {
            //час третий, силы закончились здесь (они в любом случае не начиались :) )

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string msg;
            int usrid;
            if (textBox2.Text == textBox4.Text)
            {
                if (service.UserLogin(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, out msg))
                {
                    CheckedCredentials = true;
                    Login = textBox1.Text;
                    this.Close();
                };
            }
            else 
            {
                msg = "Password input faliure!";
            }
            label1.Text = msg;
        }
    }
}
