using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berezka.Data.Model
{
    abstract record EntityView
    {
        public Guid Id { get; init; }

        public bool Visible { get; set; }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
