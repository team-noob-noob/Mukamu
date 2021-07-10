using System;
using Sinuka.Infrastructure.Security;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Cryptography;
using System.Dynamic;

namespace Sinuka.Infrastructure.Database
{
    public class ModelFactory : 
    IUserFactory, 
    ISessionFactory,
    ISessionTokenFactory,
    IRefreshTokenFactory
    {
        public User CreateUser(string username, string password, string email)
        {
            // TODO: Move the '7' to configurations as part of SecurityConfigs or something
            var hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password, 7);
            var token = KeyGenerator.GetUniqueKeyOriginal_BIASED(
                Sinuka.Infrastructure.Configurations.TokenConfig.RefreshTokenLength
            );
            var emailInstance = new EmailAddress(email, token);
            return new User(username, hashedPassword, emailInstance);
        }

        public Session CreateSession(User user)
        {
            dynamic payload = new ExpandoObject();
            payload.Email = user.Email;
            payload.Id = user.Id;
            return new Session(user, this.CreateSessionToken(payload));
        }

        public SessionToken CreateSessionToken(dynamic payload)
        {
            var expiresAt = DateTime.Now + Sinuka.Infrastructure.Configurations.TokenConfig.SessionTokenLifetimeLength;
            payload.ExpiresAt = expiresAt;
            var token = JwtTokenGenerator.GeneratorToken(payload);
            return new SessionToken(token, expiresAt);
        }

        public RefreshToken CreateRefreshToken()
        {
            var token = KeyGenerator.GetUniqueKeyOriginal_BIASED(
                Sinuka.Infrastructure.Configurations.TokenConfig.RefreshTokenLength
            );
            var expiresAt = DateTime.Now 
                + Sinuka.Infrastructure.Configurations.TokenConfig.RefreshTokenLifetimeLength;
            return new RefreshToken(token, expiresAt);
        }
    }
}
