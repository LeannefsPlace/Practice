using LizaItogPractice.MainForms;
using LizaItogPractice.Service;
using LizaItogPractice.Util;

namespace LizaItogPractice
{
    public partial class FormEnter : Form
    {
        UserAuthService userAuthService = new UserAuthService();

        public FormEnter()
        {
            InitializeComponent();
        }

        private bool checkedCredentials = false;

        public bool CheckedCredentials
        {
            get => checkedCredentials;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            string credentials = textBox1.Text;
            string login = textBox1.Text;
            string password = textBox2.Text;
            checkedCredentials = userAuthService.checkCredentials(login, password, out message);
            label1.Text = message;
            if (checkedCredentials)
            {
                if (new Util.Credentials(credentials).role == "user")
                {
                    MainForm a = new MainForm(new Util.Credentials(credentials));
                    this.Hide();
                    a.ShowDialog();
                }
                else if (new Util.Credentials(credentials).role == "admin")
                {
                    AdminForm a = new AdminForm();
                    this.Hide();
                    a.ShowDialog();
                }
                else
                {
                    ManagerForm a = new ManagerForm();
                    this.Hide(); a.ShowDialog();
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormLogin a = new FormLogin();
            a.ShowDialog();
            if (a.CheckedCredentials)
            {
                string credentials = a.Login;
                MainForm b = new MainForm(new Util.Credentials(credentials));
                this.Hide();
                b.ShowDialog();
                this.Close();
            }
        }
    }
}