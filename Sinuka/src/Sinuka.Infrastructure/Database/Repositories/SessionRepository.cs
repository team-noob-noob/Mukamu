using System.Linq;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ISinukaDbContext _dbContext;

        public SessionRepository(ISinukaDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task AddSession(Session session)
            => await this._dbContext.Sessions.AddAsync(session);

        public async Task<Session?> FindSessionByRefreshToken(string token)
        {
            var result = await Task.FromResult(
                this._dbContext.Sessions.FirstOrDefault(
                    s => !s.DeletedAt.HasValue && s.RefreshToken.Token == token)
            );
            return result;
        }

        public async Task<Session?> FindSessionBySessionToken(string token)
        {
            var result = await Task.FromResult(
                this._dbContext.Sessions.FirstOrDefault(
                    s => !s.DeletedAt.HasValue && s.SessionToken.Token == token)
            );
            return result;
        }

        public void RemoveSession(Session session)
            => this._dbContext.Sessions.Remove(session);
    }
}
