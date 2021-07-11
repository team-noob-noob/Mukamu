using System;

namespace Sinuka.Core.Models
{
    public class PasswordReset : IEntity
    {
        public PasswordReset() {}
        public PasswordReset(User user, string resetToken, DateTime expiresAt)
        {
            this.User = user;
            this.ResetToken = resetToken;
            this.ExpiresAt = expiresAt;
        }

        public Guid Id { get; set; }
        public virtual User User { get; set; }
        public string ResetToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public bool IsExpired(DateTime? now = null)
        {
            now = now ?? DateTime.Now;
            return this.ExpiresAt < now;
        }
    }
}
