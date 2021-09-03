using System;
using System.Linq;
using Berezka.Data;
using Berezka.Data.EnumType;
using Berezka.Data.Service;
using Berezka.WebApp.Service;
using Castle.Core.Configuration;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using RestSharp;

namespace Berezka.UnitTest
{
    [TestClass]
    public class EfTest
    {
        IConfiguration _configuration;

        public EfTest (IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [TestMethod]
      
        public  void EfServiceTest()
        {

 

          


            var a = _configuration;
            var b = a;



            var connectionString = "Host=localhost;Port=5432;User ID=postgres;Password=123;Database=Test6;Pooling=true;";

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(connectionString);

            using (var context = new MyDbContext(builder.Options))
            {



                //var b = context.Accounts.Count();
                //var z = b;
                IConfiguration _configuration;
                IAccountService accountService = new AccountService(context);
                ICryptoHelper cryptoHelper = new RsaHelper();
               // ITokenService tokenService = new TokenService(accountService, _configuration, cryptoHelper)

                //ITokenService tokenService = new TokenService(accountService,);

                //accountService.GetAccount().
                // var gg = ass.GetAccount();





            }




        }
    }
}
