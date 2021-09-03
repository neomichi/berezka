using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Berezka.Data.Model
{
    public class UserClaims
    {
        private MyDbContext _db;
        public UserClaims(MyDbContext db)
        {
            _db = db;
        }

        public Guid Id { get { return GetId(); } }
        public string Email { get { return _user.Claims.First(x => x.Type == ClaimTypes.Email).Value; } }
        public Account AccountId { get { return _db.Accounts.FirstOrDefault(x => x.Id == GetId()); } }
        private readonly ClaimsPrincipal _user = null;
        public UserClaims(ClaimsPrincipal user)
        {
            _user = user;
        }

        private Guid GetId()
        {
            var empty = Guid.Empty;
            var strGuid = _user.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            Guid.TryParse(strGuid, out empty);
            return empty;              
            
        }

        

    }
}
