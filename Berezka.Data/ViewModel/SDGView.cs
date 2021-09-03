using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.ViewModel
{
    public struct SDGView
    {
        public Guid UserId { get; }
        public DateTime Date{ get; }
        public string SecurityKey { get; }

        public SDGView(string securityKey, DateTime date,Guid userId)
        {
            UserId = userId;
            Date = date;
            SecurityKey = securityKey;
        }
    }
}
