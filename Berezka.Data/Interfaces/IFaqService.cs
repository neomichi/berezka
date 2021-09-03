using System;
using System.Collections.Generic;
using Berezka.Data.ViewModel;

namespace Berezka.Data.Service
{
    public interface IFaqService
    {
        void AddQuestionAnswers(FaqView faqView);
        IEnumerable<KeyValuePair<string, Guid>> GetAllCategories();
        List<FaqData> GetFaqTest();
        string GetAnswer(Guid id);
    }
}