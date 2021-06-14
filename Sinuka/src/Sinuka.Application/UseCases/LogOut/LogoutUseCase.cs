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
            this._presenter.Done();

            var session = await this._sessionRepo.FindSessionByToken(input.Token);

            if(session is null)
                return;

            this._sessionRepo.RemoveSession(session);           
        }

        public void SetPresenter(ILogoutPresenter presenter)
            => this._presenter = presenter;
    }
}
