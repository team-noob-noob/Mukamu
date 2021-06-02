using System;
using System.Linq;

namespace Sinuka.Core.Models
{
    public class User : IEntity
    {
        public User(string username, string hashedPassword, string email)
        {
            this.Id = Guid.NewGuid();
            this.Username = username;
            this.HashedPassword = hashedPassword;
            this.Email = email;
        }

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public virtual IQueryable<Session> Sessions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
