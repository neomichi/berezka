using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Berezka.Data.Model
{
    public class Account : Entity
    {
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Email { get; set; }
        [StringLength(60, MinimumLength = 3)]
        public string Avatar { get; set; } = "default.png";
        [StringLength(60, MinimumLength = 3)]
        public string Url { get; set; }
        [StringLength(160, MinimumLength = 3)]
        public string Fio { get; set; }
        [StringLength(256, MinimumLength = 6)]  
        public string Password { get; private set; }               
        [NotMapped]
        public string SetPassword { set { this.Password = Helper.StrToHash(value); } }              

        public ICollection<AccountRole> AccountRoles { get; set; }


        public string[] GetListRoles()
        {
            var ar = new List<string>();
            if (AccountRoles!=null && AccountRoles.Any())
            {
                var roles=  this.AccountRoles.Select(x => x.Role.Title).ToArray();
                ar.AddRange(roles);
                
            }
            return ar.ToArray();
        }


    }

    

}
