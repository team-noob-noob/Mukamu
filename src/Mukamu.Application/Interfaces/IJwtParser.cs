using System;

namespace Mukamu.Application.Interfaces
{
    public interface IJwtParser
    {
        JwtBody ParseJwtString(string jwtString);
    }

    public class JwtBody
    {
        public Guid Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
