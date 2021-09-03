using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Berezka.Data;
using Berezka.Data.EnumType;
using Berezka.Data.Service;
using Microsoft.EntityFrameworkCore;
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

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024);
            var pb = rsa.ToXmlString(false);
            var pk = rsa.ToXmlString(true);
            var secret = RSATool.Encrypt(test, pb);
            var result = RSATool.Decrypt(secret, pk);
            Assert.IsTrue(result.EQ(test));

        }

        [Test]
        public void HelperMetodsTest()
        {
            Assert.IsTrue(Helper.EQ("asd", "ASD"));
            Assert.IsTrue(Helper.EQ("ASD", "ASD"));
            NUnit.Framework.Assert.IsTrue(Helper.EQ("ASD", "asd"));

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
        //[Test]
        //public void AesHelper()
        //{
        //    var aes = new AesHelper();
        //    var userId = Guid.NewGuid();
        //    var DateTimeNo = DateTime.Now;
        //    var fullToStringDate= Helper.FullToString(DateTimeNo);
        //    var securiyKey = "YaTvoiDomTpybaShatalikalitkoXl";

        //    var secret= aes.EncryptData(userId, fullToStringDate, securiyKey);
        //    Assert.Equals(userId, aes.DecryptData(secret).UserId);
        //}

    }
}