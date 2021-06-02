using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sinuka.Infrastructure.Database
{
    public class SinukaDbContextFactory : IDesignTimeDbContextFactory<SinukaDbContext>
    {
        public SinukaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SinukaDbContext>();
            optionsBuilder.UseMySql(
                Sinuka.Infrastructure.Configurations.DbConfig.DbConnectionString,
                options => {
                    options.ServerVersion(Sinuka.Infrastructure.Configurations.DbConfig.MySqlVersion, Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql);
                    options.MaxBatchSize(1);
                }
            );
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            return new SinukaDbContext(optionsBuilder.Options);
        }
    }
}
