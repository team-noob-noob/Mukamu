using System;

namespace Sinuka.Core.Models
{
    public class Session : IEntity
    {
        public Session() {}
        public Session(User user, string token)
        {
            this.User = user;
            this.Token = token;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Token { get; set; }

        public virtual User User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}