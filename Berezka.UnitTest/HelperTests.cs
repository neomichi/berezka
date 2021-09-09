using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Berezka.Data;
using Berezka.Data.EnumType;
using Berezka.Data.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Berezka.UnitTest
{
    
    public class HelperTests
    {

            
      
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void RsaTest()
        {
            var test = "Hello";

            var rsa = new RSACryptoServiceProvider(1024);
            var pb = rsa.ToXmlString(false);
            var pk = rsa.ToXmlString(true);
            var secret = RSATool.Encrypt(test, pb);
            var result = RSATool.Decrypt(secret, pk);
            Assert.IsTrue(result.EQ(test));

        }

         [Test]
         public void HelperMethodTest()
         {
             Assert.IsTrue(Helper.EQ("asd", "ASD"));
             Assert.IsTrue(Helper.EQ("ASD", "ASD"));
             Assert.IsTrue(Helper.EQ("ASD", "asd"));
        
             Assert.IsTrue(Helper.ToGuid(Guid.Empty.ToString()).Equals(Guid.Empty));
        
             Assert.IsTrue(Helper.StrToHash("hello").EQ(Helper.StrToHash("hello")));
        
             int test = 5;
             Assert.IsTrue(Helper.ToInt(test.ToString()) == test);
        
             DateTime dateTime = DateTime.UtcNow;
             string result = Helper.FullToString(dateTime);
             DateTime newDateTime = Helper.ParceFullToDateTime(result);
           
        
             var compare = (dateTime - newDateTime).Milliseconds;
        
             Assert.IsTrue(compare == 0);
        
        
             var gg = Helper.EnumToDic<Roles>();
             var rolesDic = Enum.GetValues(typeof(Roles)).Cast<Roles>()
                     .Select(x => new
                     {
                         Id = (int)x,
                         Text = x.ToString()
                     }).ToList();
             var aa= rolesDic;
         }
        [Test]
        public void AesHelper()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build(); 
            
            var userId = Guid.NewGuid();
          var dateTimeNow = DateTime.Now;
          var fullToStringDate= Helper.FullToString(dateTimeNow);
          var securytiKey = "YaTvoiDomTpybaShatalikalitkoXl";
          var aes = new AesHelper(config);
          var ecryptData= aes.EncryptData(userId, fullToStringDate, securytiKey);
          var aesUserId = aes.DecryptData(ecryptData).UserId;
          Assert.Equals(userId,aesUserId );
          
       
        }

    }
}