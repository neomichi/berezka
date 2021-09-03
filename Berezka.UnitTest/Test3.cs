using System;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Berezka.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Berezka.UnitTest
{
    public class Tests3
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
        public void HelperTest()
        {
            Assert.IsTrue(Helper.EQ("asd", "ASD"));
            Assert.IsTrue(Helper.EQ("ASD", "ASD"));
            Assert.IsTrue(Helper.EQ("ASD", "asd"));

            Assert.IsTrue(Helper.ToGuid(Guid.Empty.ToString()).Equals(Guid.Empty));

            NUnit.Framework.Assert.IsTrue(Helper.StrToHash("hello").EQ(Helper.StrToHash("hello")));

            int test = 5;
            Assert.IsTrue(Helper.ToInt(test.ToString()) == test);

            DateTime dateTime = DateTime.UtcNow;
            string result = Helper.FullToString(dateTime);

            Assert.IsTrue(dateTime.Equals(Helper.ParceFullToDateTime(result)));

        }
        [Test]
        public void LinqEFEquals()
        {
           

               var result0 = EF.Equals("sdsa", "asd");
            var result1 = EF.Equals("sds", "asd");
            var result2 = EF.Equals("asd", "asd");
            var result3 = EF.Equals("ASd", "asd");
            var result4 = EF.Equals("aSd", "asd");
            Assert.AreEqual(result0, result3);
        }

        [Test]
        public void LinqEFLike()
        {
            var a = 1;
            var b = a;
            bool result0 = EF.Functions.Like("asd", "asd");
            bool result1 = EF.Functions.Like("asd", "AasdA");
            bool result2 = EF.Functions.Like("asD", "Asd");
           
            Assert.AreEqual(result0, result2);
        }

        [Test]
        public void EfMock()
        {
            
           // var mockSet = new Mock<MyDbContext>();
           // //mockSet.Setup(x => x);)
           //mockSet.Setup(x=>x.Model).Returns(x=>x)

           // ////IAccountService aa=
           // ///
           // var gbh=mockSet.Object;

           // IAccountService accountService = new AccountService(gbh);

           // var gg=accountService.EmailFree("asd@asd.ru");
           // var ggg = gg;


        }
    }
}