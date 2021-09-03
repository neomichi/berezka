using System;
using System.Collections.Generic;
using System.Text;
using Berezka.Data.EnumType;

namespace Berezka.Data.ViewModel
{
    public struct TokenResult
    {
        public Guid Value { get; set; }
        public TokenState ReturnType { get; set; }

        public TokenResult(Guid val)
        {
            Value = Guid.Empty;
            ReturnType = TokenState.Bad;
            Value = val;
        }
    }
    
}
