using System.Collections.Generic;

namespace Mukamu.Core.Models
{
    /// <summary>Represents an exchange of Message between 2 users</summary>
    public class Conversation
    {
        public virtual ICollection<Message> Messages { get; set; }
            = new List<Message>();
        public virtual ICollection<User> Users { get; set; }
    }
}
