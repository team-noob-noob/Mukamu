using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sinuka.Core.Models;


namespace Sinuka.Infrastructure.Database.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasOne<User>(s => s.User);
            builder.HasOne<Client>(s => s.Client);
        }
    }
}
