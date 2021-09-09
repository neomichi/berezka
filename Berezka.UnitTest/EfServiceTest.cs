using System;
using System.Configuration;
using System.Linq;
using Berezka.Data;
using Berezka.Data.EnumType;
using Berezka.Data.Service;
using Berezka.WebApp.Service;

using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using RestSharp;
using Assert = NUnit.Framework.Assert;


namespace Berezka.UnitTest
{
  
    public class EfTest
    {

        [Test]
        public void EfServiceTest()
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
                Assert.False(accountService.EmailFree("admin@test.ru").Result);
                Assert.False(accountService.UrlFree("manager").Result);

                var account = accountService.GetAllAccount().First();
                Assert.AreEqual(account.Id,accountService.GetAccount(account.Id).Id);
                
               
        }

       









        
    }
}
