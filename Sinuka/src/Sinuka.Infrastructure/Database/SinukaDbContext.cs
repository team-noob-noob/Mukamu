using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sinuka.Core.Infrastructure.Database
{
    public class SinukaDbContext : DbContext
    {
        public SinukaDbContext(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(SinukaDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                Sinuka.Infrastructure.Configurations.DbConfig.DbConnectionString,
                options => {
                    options.ServerVersion(Sinuka.Infrastructure.Configurations.DbConfig.MySqlVersion, Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
                    options.MaxBatchSize(1);
                }
            );
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
                    !(x.Entity as Sinuka.Core.Models.IEntity is null)
            )
            .Select(x => x.Entity as Sinuka.Core.Models.IEntity );

            var modifiedEntities = this.ChangeTracker.Entries()
            .Where(
                x =>
                    x.State == EntityState.Modified && 
                    !(x.Entity is null) && 
                    !(x.Entity as Sinuka.Core.Models.IEntity  is null)
            )
            .Select(x => x.Entity as Sinuka.Core.Models.IEntity );

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
