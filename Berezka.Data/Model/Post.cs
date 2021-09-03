﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Berezka.Data.Model
{
    public class Post:Entity
    {
        public string Title { get; set; }
        public string ShortText { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }

        ICollection<Comment> Comments { get; set; }

    }
}
