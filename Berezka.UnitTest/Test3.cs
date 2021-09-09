using System;
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
            Assert.IsTrue("asd".EQ("ASD"));
            Assert.IsTrue("ASD".EQ("ASD"));
            Assert.IsTrue("ASD".EQ("asd"));

            Assert.IsTrue(Guid.Empty.ToString().ToGuid().Equals(Guid.Empty));
            Assert.IsTrue("hello".StrToHash().EQ("hello".StrToHash()));
        
         
            
            int test = 5;
            Assert.IsTrue(test.ToString().ToInt() == test);

          

        }

    }
}