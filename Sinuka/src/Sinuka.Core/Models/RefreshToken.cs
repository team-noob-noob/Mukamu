using System;

namespace Sinuka.Core.Models
{
    public class RefreshToken : IEntity
    {
        public RefreshToken(string token, DateTime expiresAt)
        {
            this.Id = Guid.NewGuid();
            this.Token = token;
            this.ExpiresAt = expiresAt;
        }

        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
