using LizaItogPractice;
using LizaItogPractice.Service;
using LizaItogPractice.Util;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void DAOConnectionTest()
        {
            try
            {
                DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
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
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            Credentials cred = new Credentials("admin");
            
            Assert.Equal("admin@admin.admin", cred.email);
        }

        [Fact]
        public void UnableCredetialCreationTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            Credentials cred = new Credentials("olegovnikoleg");
            bool result = (cred.email == null);
            Assert.True(result);
        }

        [Fact]
        public void UserAuthCreateTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");

            UserAuthService userAuthService = new UserAuthService();
            Random rand = new Random();
            string msg;
            string login = rand.Next(20000).ToString();
            userAuthService.UserLogin(login, "oleg", "oleg oleg oleg", $"{login}@oleg.olgasa", out msg);
            Credentials cred = new Credentials(login);
            Assert.Equal("oleg oleg oleg", cred.fullname);
        }

        [Fact]
        public void UserAuthUnableToCreateTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");

            UserAuthService userAuthService = new UserAuthService();
            Random rand = new Random();
            string msg;
            userAuthService.UserLogin("admin", "oleg", "oleg oleg oleg", $"random@oleg.olgasa", out msg);

            Assert.Equal("Credentials invalid or expired!", msg);
        }

        [Fact]
        public void UserCredentialsCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("admin", "admin", out msg);
            Assert.True(result);
        }

        [Fact]
        public void UserBadPasswordCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("admin", "notAdmin", out msg);
            Assert.False(result);
        }

        [Fact]
        public void UserBadLoginCheckTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.checkCredentials("notAdmin", "notAdmin", out msg);
            Assert.False(result);
        }

        [Fact]
        public void UserCredentialsEditTest()
        {
            DAO dao = new DAO("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=0903");
            UserAuthService userAuthService = new UserAuthService();
            string msg;
            bool result = userAuthService.EditCredentials("admin", "admin admin new admin", "admin@admin.admin", out msg);
            Assert.Equal("Congradulations, all went as expected, admin!", msg);
        }
    }
}