using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.Model
{
   public class AccountRole:Entity
    {
        public Account Account { get; set; }

        public Guid AccountId { get; set; }

        public Role Role { get; set; }

        public Guid RoleId { get; set; }

       
    }
}
