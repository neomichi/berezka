using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Berezka.Data.Model
{
    public class Role:Entity
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string Title { get; set; }       

        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
