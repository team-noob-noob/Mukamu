using System;

namespace Mukamu.Core.Models
{
    public class SubComment : IEntity
    {
        public SubComment() 
        {
            this.Id = Guid.NewGuid();
        }
        public SubComment(string message, User commenter) 
        {
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.Commenter = commenter;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual User Commenter { get; set; }
    }
}
