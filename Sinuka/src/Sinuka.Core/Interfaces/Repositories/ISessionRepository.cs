using System.Threading.Tasks;
using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Repositories
{
    public interface ISessionRepository
    {
        Task AddSession(Session session);
        void RemoveSession(Session session);
        Task<Session?> FindSessionBySessionToken(string token);
        Task<Session?> FindSessionByRefreshToken(string token);
    }
}
