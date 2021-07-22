using System;
using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    public class User : IEntity
    {
        public User() 
        {
            this.Id = Guid.NewGuid();
        }
        public User(Guid externalId)
        {
            this.Id = Guid.NewGuid();
            this.ExternalId = externalId;
        }

        public Guid Id { get; set; }

        /// <summary>Id from the external id service</summary>
        public Guid ExternalId { get; set; }

        /// <summary>The nickname that will be displayed to the public</summary>
        public string DisplayName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
            = new List<Post>();
            
        public virtual ICollection<Conversation> Conversations { get; set; }
            = new List<Conversation>();

        public void AddPost(Post newPost)
            => this.Posts.Add(newPost);
    }
}
