using System;

namespace Sinuka.Core.Models
{
    public class Role : IEntity
    {
        public Role() {}
        public Role(string Name, string Description)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Description = Description;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
