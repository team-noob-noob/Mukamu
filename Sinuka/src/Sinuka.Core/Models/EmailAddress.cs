using System;

namespace Sinuka.Core.Models
{
    public class EmailAddress : IEntity
    {
        public EmailAddress(string email, string verificationString)
        {
            this.Email = email;
            this.VerificationString = verificationString;
        }

        public string Email { get; set; }

        /// <summary>If null, email is verified; otherwise, email is not verified</summary>
        public string? VerificationString { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
