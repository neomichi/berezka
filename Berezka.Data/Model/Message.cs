using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Berezka.Data.EnumType;

namespace Berezka.Data.Model
{
    public class Message : Entity
    {
        [Required]       
        public Guid From { get; set; }
        [Required]    
        public Guid To { get; set; }
        [Required]
        [StringLength(600, MinimumLength = 1)]
        public string Text { get; set; }
        [Required]
        public MessageType messageType { get;set;}
        [Required]
        public MessageStatus messageStatus { get; set; }
    }
}
