using LizaItogPractice;
using LizaItogPractice.Service;
using LizaItogPractice.Util;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void DAOConnectionTest()
        {
            try
            {
                DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
                Assert.True(true);
            }
            catch (Exception ex)
            {
                Assert.False(true);
            }
        }

        [Fact]
        public void DAOBadConnectionTest()
        {
            try
            {
                DAO dao = new DAO("olegolegolegoleg");
                Assert.False(true);
            }
            catch (Exception ex)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void CredentialCreationTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            Credentials cred = new Credentials(1);

            Assert.Equal("qweqwe", cred.fullname);
        }

        [Fact]
        public void UnableCredetialCreationTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            Credentials cred = new Credentials(2312);
            bool result = (cred.fullname == null);
            Assert.True(result);
        }

        [Fact]
        public void UserAuthCreateTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");

            UserAuthService userAuthService = new UserAuthService();
            Random rand = new Random();
            string msg;
            string login = rand.Next(20000).ToString();
            int userid;
            userAuthService.UserLogin(login, "oleg", "oleg oleg oleg", out msg, out userid);
            Credentials cred = new Credentials(userid);
            Assert.Equal("oleg oleg oleg", cred.fullname);
        }

        [Fact]
        public void UserAuthUnableToCreateTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");

            UserAuthService userAuthService = new UserAuthService();
            Random rand = new Random();
            string msg;
            int userid;
            userAuthService.UserLogin("admin", "oleg", "oleg oleg oleg", out msg, out userid);

            Assert.Equal("Credentials invalid or expired!", msg);
        }

        [Fact]
        public void UserCredentialsCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("admin", "admin", out msg, out int use);
            Assert.True(result);
        }

        [Fact]
        public void UserBadPasswordCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("admin", "notAdmin", out msg, out int use);
            Assert.False(result);
        }

        [Fact]
        public void UserBadLoginCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("notAdmin", "notAdmin", out msg, out int use);
            Assert.False(result);
        }

        [Fact]
        public void UserCredentialsEditTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=Liza;User Id=admin;Password=admin_password");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.EditCredentials(1, "admin", "qweqweqwe", out msg);
            Assert.Equal("Congradulations, all went as expected, admin!", msg);
        }
    }
}