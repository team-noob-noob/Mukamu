using System;

namespace Sinuka.Core.Models
{
    public class Session : IEntity
    {
        public Session() {}
        public Session(User user, SessionToken sessionToken)
        {
            this.User = user;
            this.SessionToken = sessionToken;
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public virtual SessionToken SessionToken { get; set; }
        public virtual RefreshToken? RefreshToken { get; set; }
        public virtual User User { get; set; }
        public virtual Client Client { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
    }
}
