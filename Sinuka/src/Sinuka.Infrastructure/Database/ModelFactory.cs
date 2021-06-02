using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database
{
    public class ModelFactory : IUserFactory, ISessionFactory
    {
        public User CreateUser(string username, string password, string email)
        {
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return new User(username, password, email);
        }

        public Session CreateSession(User user, string token) => new Session(user, token);
    }
}
