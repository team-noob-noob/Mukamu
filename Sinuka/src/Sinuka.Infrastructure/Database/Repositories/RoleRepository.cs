using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly SinukaDbContext _dbContext;

        public RoleRepository(SinukaDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task AddRole(Role newRole)
            => await this._dbContext.Roles.AddAsync(newRole);
    }
}
