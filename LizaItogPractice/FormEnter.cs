using LizaItogPractice.Service;

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

        public int Userid
        {
            get; set;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message;
            int credentials = 0;
            string login = textBox1.Text;
            string password = textBox2.Text;
            checkedCredentials = userAuthService.checkCredentials(login, password, out message, out credentials);
            label1.Text = message;
            if (checkedCredentials)
            {
                Userid = credentials;
                this.Close();
            }
        }
    }
}