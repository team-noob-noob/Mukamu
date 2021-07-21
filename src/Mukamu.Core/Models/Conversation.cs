using System;
using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    /// <summary>Represents an exchange of Message between users</summary>
    public class Conversation : IEntity
    {
        public Conversation(ICollection<User> users)
        {
            this.Users = users;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
            = new List<Message>();

        public virtual ICollection<User> Users { get; set; }
            = new List<User>();
    }
}
