using System;

namespace Mukamu.Core.Models
{
    // TODO: find a store for images
    /// <summary>Represents a file/image uploaded to a post or message</summary>
    public class Attachment : IEntity
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
