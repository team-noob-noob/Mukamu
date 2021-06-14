using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;

namespace Sinuka.Application.UseCases.Logout
{
    public class LogoutUseCase : ILogoutUseCase
    {
        private ILogoutPresenter _presenter;
        private readonly ISessionRepository _sessionRepo;

        public LogoutUseCase(ISessionRepository sessionRepository)
            => this._sessionRepo = sessionRepository;

        public async Task Run(LogoutInput input)
        {
            var session = await this._sessionRepo.FindSessionByToken(input.Token);

            if(session is not null)
                this._sessionRepo.RemoveSession(session);

            this._presenter.Done();
        }

        public void SetPresenter(ILogoutPresenter presenter)
            => this._presenter = presenter;
    }
}
