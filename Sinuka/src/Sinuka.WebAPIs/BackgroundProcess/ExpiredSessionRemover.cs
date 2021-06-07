using System.Threading.Tasks;
using Sinuka.Core.Models;
using Sinuka.Core.Interfaces.Repositories;

namespace Sinuka.WebAPIs.BackgroundProcess
{
    public class ExpiredSessionRemover
    {
        private readonly ISessionRepository _sessionRepository;

        public ExpiredSessionRemover(ISessionRepository sessionRepository)
            => this._sessionRepository = sessionRepository;

        public async Task RemoveSession(string token)
        {
            var session = await this._sessionRepository.FindSessionByToken(token);
            this._sessionRepository.RemoveSession(session);
        }
    }
}
