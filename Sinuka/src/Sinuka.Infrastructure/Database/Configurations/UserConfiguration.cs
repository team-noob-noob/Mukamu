using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany<Session>(u => u.Sessions);
            builder.HasKey(u => u.Id);
        }
    }
}
