using System.Configuration;
using Berezka.Data.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Berezka.UnitTest
{
    [TestClass]
    public class TestConfiguration
    {
      
        #region Property  
        public Mock<IConfiguration> mock = new Mock<IConfiguration>();
        #endregion
       
        [TestMethod]
        public void TestTConfiguration()
        {

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");
            

           // Assert.AreEqual(a, b);
        }
    }
}
