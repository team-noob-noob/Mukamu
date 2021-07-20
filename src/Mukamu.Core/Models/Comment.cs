using System;

namespace Mukamu.Core.Models
{
    /// <summary>Represents a comment on a Post</summary>
    public class Comment : IEntity
    {
        public Comment(string message, User commenter)
        {
            this.Message = message;
            this.Commenter = commenter;
        }

        public Guid Id { get; set; }

        /// <summary>The actual message on the post</summary>
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual User Commenter { get; set; }
    }
}
