using System;

namespace Sinuka.Core.Models
{
    public class Session : IEntity
    {
        public Guid Id { get; set; }
        public Guid User_Id { get; set; }
        public string Token { get; set; }

        public virtual User user { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}