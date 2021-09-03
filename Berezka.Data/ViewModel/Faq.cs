using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.ViewModel
{

    public class FaqItems
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Visible { get; set; }
    }

    public class FaqData
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public List<FaqItems> FaqItems { get; set; } = new List<FaqItems>();
        
    }

    
}
