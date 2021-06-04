using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly SinukaDbContext _dbContext;

        public SessionRepository(SinukaDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task AddSession(Session session)
            => await this._dbContext.Sessions.AddAsync(session);
    }
}
