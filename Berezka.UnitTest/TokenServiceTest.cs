using System.Linq;
using Berezka.Data;
using Berezka.Data.Service;
using Berezka.WebApp.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Berezka.UnitTest
{
    [TestClass]
    public class TokenServiceTest
    {

        [TestMethod]
        public void ServiceTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetSection("ConnectionStrings")["PostgreSQL"];

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(connectionString);

            var context = new MyDbContext(builder.Options);

            IAccountService accountService = new AccountService(context);

            ICryptoHelper cryptoHelper = new RsaHelper();

            ITokenService tokenService = new TokenService(accountService, configuration, cryptoHelper);

            var accountId = accountService.GetAllAccount().Select(x => x.Id).First();
            var responseToken = tokenService.CreateToken(accountId);
            var guid = tokenService.CheckingRefreshToken(responseToken.refreshToken).Value;
            Assert.AreEqual(guid, accountId);
            Assert.AreEqual(tokenService.CheckingActionToken(responseToken.accessToken).Value, accountId);
        }
    }
}

        
        
    
