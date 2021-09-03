using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.ViewModel
{
    public class FaqView
    {
        public Guid? CategoryId { get; set; }
        public string Category { get; set; }
        public string Question { get; set; }
    
    }    
}
