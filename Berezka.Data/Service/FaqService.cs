using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Berezka.Data.Model;
using Berezka.Data.ViewModel;

namespace Berezka.Data.Service
{
    public class FaqService : IFaqService
    {
        private MyDbContext _db;
        public FaqService(MyDbContext db)
        {
            _db = db;
        }
   
        public List<FaqData> GetFaqTest()
        {
            //
            var faqQA = GetFaqQuestionAnswers().Include(x => x.FaqCategory).ToArray();

            var qq = (from item in faqQA
                      group item by new { item.FaqCategoryId, item.FaqCategory.Category } into g
                      select new FaqData
                      {
                          Category = g.Key.Category,
                          Id = g.Key.FaqCategoryId,
                          FaqItems = g.Select(x => new FaqItems
                          {     
                              Question = x.Question,
                              Id = x.Id,
                              Answer=x.Answer,
                              Visible=x.Visible                            

                          }).ToList()
                      }).ToList();

            return qq;
        }


        private IQueryable<FaqQuestionAnswer> GetFaqQuestionAnswers()
        {
            return _db.FaqQuestionAnswers.Where(x => x.Visible).AsNoTracking();
        }
     

        private FaqCategory[] Categories()
        {
            return _db.FaqCategories
                .Where(x => x.Visible)
                .AsNoTracking().ToArray();
        }

        public IEnumerable<KeyValuePair<string, Guid>> GetAllCategories()
        {
            return Categories().Select(x => new KeyValuePair<string, Guid>(x.Category, x.Id));
        }

        public void AddQuestionAnswers(FaqView faqView)
        {
            var fc = new FaqCategory() { Category = faqView.Category.ToLower() };
          
            var categoryId = faqView.CategoryId;
            if (categoryId == Guid.Empty || categoryId==null)
            {
                var categoryExist = _db.FaqCategories.FirstOrDefault(x => x.Category == faqView.Category.ToLower());
                if (categoryExist == null)
                {
                    _db.FaqCategories.Add(fc);
                }
                else fc = categoryExist;
            }

            var questionExist = GetFaqQuestionAnswers().FirstOrDefault(x => x.Question == faqView.Question.ToLower());
            if (questionExist == null)
            {
                _db.FaqQuestionAnswers.Add(
                    new FaqQuestionAnswer { FaqCategoryId = fc.Id, Question = faqView.Question.ToLower(),Visible=false });
            }
            _db.SaveChanges();

        }


        public string GetAnswer(Guid id)
        {
            var result = GetFaqQuestionAnswers().FirstOrDefault(x => x.Id == id);
            if (result==null) return "";
            return result.Answer;
        }


    }
}
