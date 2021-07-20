using System;
using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    public class User : IEntity
    {
        public User(Guid externalId)
        {
            this.ExternalId = externalId;
        }

        public Guid Id { get; set; }

        /// <summary>Id from the external id service</summary>
        public Guid ExternalId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
            = new List<Post>();
            
        public virtual ICollection<Conversation> Conversations { get; set; }
            = new List<Conversation>();
    }
}
