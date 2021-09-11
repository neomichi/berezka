using Berezka.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Berezka.UnitTest
{
    [TestClass]
    public class TestConfiguration
    {
        
        [TestMethod]
        public void ConfigurationTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetSection("ConnectionStrings")["PostgreSQL"];
            Assert.IsTrue(!string.IsNullOrWhiteSpace(connectionString));
        }

        public void EfTest()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = config.GetSection("ConnectionStrings")["PostgreSQL"];

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(connectionString);
        
            Assert.IsNotNull(new MyDbContext(builder.Options));
       
        }
        
        
    }
}
