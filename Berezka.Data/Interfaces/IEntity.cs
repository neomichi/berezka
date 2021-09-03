using System;

namespace Berezka.Data.Model
{
    public interface IEntity
    {
        DateTime CreateAt { get; set; } 
        Guid Id { get; set; }
        bool Visible { get; set; }

        bool Equals(object obj);
        int GetHashCode();
        bool IsEmpty { get; }
        string ToString();
    }
}