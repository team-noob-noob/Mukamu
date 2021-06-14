using System;

namespace Sinuka.Core.Models
{
    public class Client : IEntity
    {
        public Client(string Name, string Secret, string HostName, string PostLoginRedirect = null, string PostLogoutRedirect = null)
        {
            this.Name = Name;
            this.Secret = Secret;
            this.Id = Guid.NewGuid();
            this.HostName = HostName;
            this.PostLoginRedirect = PostLoginRedirect;
            this.PostLogoutRedirect = PostLogoutRedirect;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string HostName { get; set; }
        public string? PostLoginRedirect { get; set; }
        public string? PostLogoutRedirect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public DateTime? DeletedAt { get; set; }
    }
}
