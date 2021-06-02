using System;

namespace Sinuka.Core.Models
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
