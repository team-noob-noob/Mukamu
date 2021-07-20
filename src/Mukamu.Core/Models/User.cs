using System;
using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        /// <summary>Id from the external id service</summary>
        public Guid ExternalId { get; set; }
        
        public virtual ICollection<Conversation> Conversations { get; set; }
            = new List<Conversation>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
