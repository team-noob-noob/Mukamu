using Sinuka.Infrastructure.Security;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database
{
    public class ModelFactory : IUserFactory, ISessionFactory
    {
        public User CreateUser(string username, string password, string email)
        {
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 7);
            return new User(username, hashedPassword, email);
        }

        public Session CreateSession(User user)
        {
            var payload = new {
                user.Email,
                user.Id,
            };
            return new Session(user, JwtTokenGenerator.GeneratorToken(payload));
        }
    }
}
