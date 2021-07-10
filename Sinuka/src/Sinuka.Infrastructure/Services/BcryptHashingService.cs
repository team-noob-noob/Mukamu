using Sinuka.Core.Interfaces.Services;

namespace Sinuka.Infrastructure.Services
{
    public class BcryptHashingService : IHashingService
    {
        public bool Compare(string plaintext, string hash)
            => BCrypt.Net.BCrypt.EnhancedVerify(plaintext, hash);

        // TODO: Move the '7' to configurations as part of SecurityConfigs or something
        public string Hash(string plaintext)
            => BCrypt.Net.BCrypt.EnhancedHashPassword(plaintext, 7);
    }
}
