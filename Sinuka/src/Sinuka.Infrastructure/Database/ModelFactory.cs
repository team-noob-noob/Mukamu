using System;
using Sinuka.Infrastructure.Security;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Cryptography;
using System.Dynamic;

namespace Sinuka.Infrastructure.Database
{
    public class ModelFactory : 
    IUserFactory, 
    ISessionFactory,
    ISessionTokenFactory,
    IRefreshTokenFactory,
    IPasswordResetFactory
    {
        private readonly IHashingService _hashingService;

        public ModelFactory(IHashingService hashingService)
            => this._hashingService = hashingService;

        public User CreateUser(string username, string password, string email)
        {
            var hashedPassword = this._hashingService.Hash(password);
            var token = KeyGenerator.GetUniqueKeyOriginal_BIASED(
                Sinuka.Infrastructure.Configurations.TokenConfig.RefreshTokenLength
            );
            var emailInstance = new EmailAddress(email, token);
            return new User(username, hashedPassword, emailInstance);
        }

        public Session CreateSession(User user, Client client)
        {
            dynamic payload = new ExpandoObject();
            payload.Email = user.Email;
            payload.Id = user.Id;
            return new Session(user, this.CreateSessionToken(payload), client);
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

        public PasswordReset CreatePasswordReset(User user)
        {
            var token = KeyGenerator.GetUniqueKeyOriginal_BIASED(
                Sinuka.Infrastructure.Configurations.TokenConfig.RefreshTokenLength
            );
            var expiresAt = DateTime.Now 
                + Sinuka.Infrastructure.Configurations.TokenConfig.ResetTokenLifetimeLength;
            return new PasswordReset(user, token, expiresAt);
        }
    }
}
