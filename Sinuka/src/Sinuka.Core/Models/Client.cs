using System;

namespace Sinuka.Core.Models
{
    public class Client : IEntity
    {
        public Client(string Name, string Secret)
        {
            this.Name = Name;
            this.Secret = Secret;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public DateTime? DeletedAt { get; set; }
    }
}
