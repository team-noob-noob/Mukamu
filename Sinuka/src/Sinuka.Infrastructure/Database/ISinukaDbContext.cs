using Microsoft.EntityFrameworkCore;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database
{
    public interface ISinukaDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Session> Sessions { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Client> Clients { get; set; }
        DbSet<EmailAddress> EmailAddresses { get; set; }
        DbSet<SessionToken> SessionTokens { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        DbSet<PasswordReset> PasswordResets { get; set; }
    }
}
