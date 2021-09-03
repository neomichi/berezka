using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Berezka.Data.Model
{
    public abstract class Entity : IEntity
    {
        public bool Visible { get; set; } = true;

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;

        public Entity() { }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("uuid_generate_v4()")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return Id.ToString();
        }

        public bool IsEmpty { get { return Id == Guid.Empty; } }
   
        public override bool Equals(object obj)
        {
            var entity = obj as Entity;

            if (entity == null)
                return false;           

            return Id == entity.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        
    }
}
