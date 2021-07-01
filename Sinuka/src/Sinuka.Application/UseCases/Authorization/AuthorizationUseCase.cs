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
            var session = await this._sessionRepo.FindSessionBySessionToken(input.Token);

            if(session is null || session.SessionToken.IsExpired())
            {
                // If Session only have SessionToken, remove expired token
                // If Session have RefreshToken, dont do anything
                if(session is not null && session?.RefreshToken is null)
                    this._sessionRepo.RemoveSession(session);

                
                this._presenter.InvalidToken();
                
                await this._unitOfWork.Save();

                return;
            }

            this._presenter.ValidToken();
        }

        public void SetPresenter(IAuthorizationPresenter presenter)
            => this._presenter = presenter;
    }
}
