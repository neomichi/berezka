using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Berezka.Data.EnumType;

namespace Berezka.Data.ViewModel
{
    public record MessageView
    {
        public Guid Id { get; init; }
        [Required]
        public Guid From { get; init; }
        [Required]
        public Guid To { get; init; }
        [Required]
        [StringLength(600, MinimumLength = 1)]
        public string Text { get; init; }
        [Required]
        public MessageType messageType { get; init; }
        [Required]
        public MessageStatus messageStatus { get; init; }
    }
}
