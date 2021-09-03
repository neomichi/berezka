using System;
using System.Collections.Generic;
using System.Text;
using Berezka.Data.EnumType;

namespace Berezka.Data.Model
{
   public class Comment:Entity
    {
       // public Account Account { get; set; }

        public Guid AccountId { get; set; }

        public string Text { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public CommentType CommentType { get; set; }


    }
}
