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
            this.ExpiresAt = this.CreatedAt + new TimeSpan(1, 0, 0);
        }

        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public virtual User User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
    }
}