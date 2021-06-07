using System;

namespace Mukamu.Core.Models
{
    public class Message : IEntity
    {
        public Guid Id { get; set; }
        public TimeSpan DelayToReciever { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Reciever { get; set; }
        
    }
}
