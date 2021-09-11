using Berezka.Data;
using Berezka.Data.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Berezka.UnitTest
{
    [TestClass]
    public class FaqServiceTest
    {
        [TestMethod]
        public void FaqTest()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();


            var connectionString = configuration.GetSection("ConnectionStrings")["PostgreSQL"];

            var builder = new DbContextOptionsBuilder<MyDbContext>();
            builder.UseNpgsql(connectionString);

            var context = new MyDbContext(builder.Options);
            
            IFaqService faqService = new FaqService(context);
            var faqList = faqService.GetAllFaq();
            var a = faqList;
            
            
            // void AddQuestionAnswers(FaqView faqView);
            // IEnumerable<KeyValuePair<string, Guid>> GetAllCategories();
            // List<FaqData> GetAllFaq();
            // string GetAnswer(Guid id);
           
       
            
          Assert.IsTrue(true);  
        }
    }
}