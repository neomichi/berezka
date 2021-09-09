using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Berezka.Data.Model;

namespace Berezka.Data.ViewModel
{
    public record AccountView 
    {
        public Guid Id { get; init; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Email { get; init; }
        [StringLength(60, MinimumLength = 3)]
        public string Avatar { get; init; } = "default.png";
        [StringLength(60, MinimumLength = 3)]
        public string Url { get; init; }
        [StringLength(160, MinimumLength = 3)]

        public string Fio { get; init; }

        public int Roles { get; init; }

        public string Password { get; init; }



        public bool Visible { get; } = true;
        public bool isAgree { get; init; }
        public bool SaveToLong { get; init; }

    }

}
