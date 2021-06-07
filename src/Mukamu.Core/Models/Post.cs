using System;

namespace Mukamu.Core.Models
{
    public class Post : IEntity
    {
        public Post(User user, string message, Attachment? attachment)
        {
            this.User = user;
            this.Message = message;
            this.Attachment = attachment;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Attachment? Attachment { get; set; }
        public virtual User User { get; set; }
    }
}

