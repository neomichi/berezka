using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Berezka.Data.Interfaces;

namespace Berezka.Data.ViewModel
{    
    public record AccountRegisterView: IAccountView
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Email { get; init; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Password { get; init; }

       
    }
}
