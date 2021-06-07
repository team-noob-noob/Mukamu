using System;

namespace Mukamu.Core.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }

        /// <summary>Id from the external id service</summary>
        public Guid ExternalId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
