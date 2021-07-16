using Microsoft.EntityFrameworkCore;
using Sinuka.Infrastructure.Database;

namespace Sinuka.Tests.EndToEndTests
{
    public class SinukaTestDbContext : SinukaDbContext, ISinukaDbContext
    {
        public SinukaTestDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {}
    }
}
