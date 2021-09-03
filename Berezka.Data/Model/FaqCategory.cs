using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Berezka.Data.Model
{
    public class FaqCategory:Entity
    { 
        [Required]
        [StringLength(60,MinimumLength =3)]
        public string Category { get; set; }

    }



}
