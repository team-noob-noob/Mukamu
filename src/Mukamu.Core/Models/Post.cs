using System;
using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    /// <summary>Represents a post on the public wall</summary>
    public class Post : IEntity
    {
        public Post(User user, string message, Attachment? attachment)
        {
            this.Poster = user;
            this.Message = message;
            this.Attachment = attachment;
        }

        public Guid Id { get; set; }
        
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
            = new List<Comment>();

        public virtual Attachment? Attachment { get; set; }

        /// <summary>The user who posted</summary>
        public virtual User Poster { get; set; }
    }
}

