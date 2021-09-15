using System;
using System.Linq;
using Berezka.Data;
using Berezka.Data.Service;
using Berezka.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;



namespace Berezka.UnitTest
{
    [TestClass]
    public class AccountServiceTest
    {

        [TestMethod]
        public void ServiceTest()
        {

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetSection("ConnectionStrings")["PostgreSQL"];

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(connectionString);

            var context = new MyDbContext(builder.Options);
            
                IAccountService accountService = new AccountService(context);

                Assert.IsTrue(accountService.EmailFree("asd@asd.ru").Result);
                Assert.IsTrue(accountService.UrlFree("asdas").Result);
                Assert.IsFalse(accountService.EmailFree("admin@test.ru").Result);
                Assert.IsFalse(accountService.UrlFree("manager").Result);

                var accountView = accountService.GetAllAccount().First();
                Assert.AreEqual(accountView.Id,accountService.GetAccount(accountView.Id).Id);
                
                
                var alv1 = new AccountLoginView() { Email = "admin@test.ru", Password = "LikeMe123" };
                var alv2 = new AccountLoginView() { Email = "admin@test.ru", Password = "LikeMe123!" };
                var alv3 = new AccountLoginView() { Email = "admin@test.ru1", Password = "LikeMe123!" };
                
                Assert.IsNull(accountService.GetAccount(alv1));
                Assert.IsNotNull(accountService.GetAccount(alv2));
                Assert.IsNull(accountService.GetAccount(alv3));




               var accountView1 = new AccountView() { Email = "unit@test.ru", Password = "unitTest123!", Url = "unitTest" };
               var accountView2=  accountService.CreateOrEditAccount(accountView1);
               Assert.IsTrue(accountView1.Email.EQ(accountView2.Email));


            var account =   context.Accounts.First(x=>x.Id == accountView2.Id);
          
            accountService.RemoveAccount(accountView2.Email);
            accountService.RemoveAccount(accountView2.Id);
            accountService.RemoveAccount(account);



           

        }
    }
}
