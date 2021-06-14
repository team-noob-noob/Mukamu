using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.Logout
{
    public class LogoutUseCase : ILogoutUseCase
    {
        private ILogoutPresenter _presenter;
        private readonly ISessionRepository _sessionRepo;
        private readonly IUnitOfWork _unitOfWork;

        public LogoutUseCase(
            ISessionRepository sessionRepository,
            IUnitOfWork unitOfWork)
        {
            this._sessionRepo = sessionRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(LogoutInput input)
        {
            var session = await this._sessionRepo.FindSessionByToken(input.Token);

            if(session is not null)
            {
                this._sessionRepo.RemoveSession(session);
                await this._unitOfWork.Save();

                if(session.Client.PostLogoutRedirect is not null)
                {
                    this._presenter.Redirect(session.Client.PostLogoutRedirect);
                    return;
                }
            }
            
            this._presenter.Done();
        }

        public void SetPresenter(ILogoutPresenter presenter)
            => this._presenter = presenter;
    }
}
