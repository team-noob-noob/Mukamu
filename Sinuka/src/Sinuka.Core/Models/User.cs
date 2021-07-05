using System;
using System.Collections.Generic;

namespace Sinuka.Core.Models
{
    public class User : IEntity
    {
        public User(string username, string hashedPassword, EmailAddress email)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.HashedPassword = hashedPassword;
            this.Email = email;
        }
        public User(string username, string hashedPassword, EmailAddress email, Role role) 
            : this(username, hashedPassword, email) => this.Role = role;
        
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public virtual EmailAddress Email { get; set; }
        public virtual ICollection<Session> Sessions { get; set; } 
            = new List<Session>();
        public virtual Role? Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
