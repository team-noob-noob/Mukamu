using System;

namespace Mukamu.Core.Models
{
    /// <summary>Represents a Private Message from one user to another</summary>
    /// <remarks>
    /// This follows the design of Slowly. The message can only be read once a 
    /// timer is finished. The time is based on the distance of the 2 users.
    /// </remarks>
    public class Message : IEntity
    {
        public Message() {}
        public Message(string actualMessage, User sender)
        {
            this.Sender = sender;
            this.ActualMessage = actualMessage;
        }

        public Guid Id { get; set; }

        /// <summary>The actual message to the receiver</summary>
        public string ActualMessage { get; set; }

        /// <summary>The time when the message will be visible to the reciever</summary>
        public DateTime VisibleAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public virtual User Sender { get; set; }

        public virtual Attachment Attachment { get; set; }
    }
}
