using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Berezka.Data.Model
{
    public class FaqQuestionAnswer : Entity
    {   
        [StringLength(600, MinimumLength = 3)]
        public string Answer { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Question { get; set; }

        public virtual FaqCategory FaqCategory { get; set; }

        public Guid FaqCategoryId { get; set; }
    }
}
