using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mukamu.Core.Models;

namespace Mukamu.Infrastructure.Database
{
    public class MukamuDbContext : DbContext, IMukamuDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }

        public MukamuDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(MukamuDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                Mukamu.Infrastructure.Configurations.DbConfig.DbConnectionString, 
                new MySqlServerVersion(Mukamu.Infrastructure.Configurations.DbConfig.MySqlVersion));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
        }

        private void UpdateEntities()
        {
            var newEntitites = this.ChangeTracker.Entries()
            .Where(
                x =>
                    x.State == EntityState.Added && 
                    !(x.Entity is null) && 
                    !(x.Entity as Mukamu.Core.Models.IEntity is null)
            )
            .Select(x => x.Entity as Mukamu.Core.Models.IEntity );

            var modifiedEntities = this.ChangeTracker.Entries()
            .Where(
                x =>
                    x.State == EntityState.Modified && 
                    !(x.Entity is null) && 
                    !(x.Entity as Mukamu.Core.Models.IEntity  is null)
            )
            .Select(x => x.Entity as Mukamu.Core.Models.IEntity );

            var currentTime = DateTime.UtcNow;

            foreach(var newEntity in newEntitites)
            {
                newEntity.CreatedAt = currentTime;
                newEntity.UpdatedAt = currentTime;
            }

            foreach(var modifiedEntity in modifiedEntities)
                modifiedEntity.UpdatedAt = currentTime;
        }

        public override int SaveChanges()
        {
            this.UpdateEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            this.UpdateEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
