using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Mukamu.Infrastructure.Database
{
    public class MukamuDbContextFactory : IDesignTimeDbContextFactory<MukamuDbContext>
    {
        public MukamuDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MukamuDbContext>();
            optionsBuilder.UseMySql(
                Mukamu.Infrastructure.Configurations.DbConfig.DbConnectionString, 
                new MySqlServerVersion(Mukamu.Infrastructure.Configurations.DbConfig.MySqlVersion));
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.EnableDetailedErrors();
            return new MukamuDbContext(optionsBuilder.Options);
        }
    }
}
