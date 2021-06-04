using System.Threading.Tasks;
using Sinuka.Application.Interfaces;
using Sinuka.Core.Interfaces.Repositories;

namespace Sinuka.Application.UseCases.Authorization
{
    public class AuthorizationUseCase : IAuthorizationUseCase
    {
        private IAuthorizationPresenter _presenter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionRepository _sessionRepo;

        public AuthorizationUseCase(
            IUnitOfWork unitOfWork,
            ISessionRepository sessionRepository
        )
        {
            this._unitOfWork = unitOfWork;
            this._sessionRepo = sessionRepository;
        }

        public async Task Run(AuthorizationInput input)
        {
            if(await this._sessionRepo.FindSessionByToken(input.Token) is null)
            {
                this._presenter.InvalidToken();
                return;
            }

            this._presenter.ValidToken();
        }

        public void SetPresenter(IAuthorizationPresenter presenter)
            => this._presenter = presenter;
    }
}
