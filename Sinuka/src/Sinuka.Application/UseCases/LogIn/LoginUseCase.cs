using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.Login
{
    public class LoginUseCase : ILoginUseCase
    {
        private ILoginPresenter _presenter;
        private readonly IUserRepository _userRepo;
        private readonly ISessionFactory _sessionFactory;
        private readonly ISessionRepository _sessionRepo;
        private readonly IClientRepository _clientRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRefreshTokenFactory _refreshTokenFactory;

        // TODO: Move validations somewhere; too much responsibility; decorator maybe?
        public LoginUseCase(
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            ISessionFactory sessionFactory,
            IUnitOfWork unitOfWork,
            IClientRepository clientRepository,
            IRefreshTokenFactory refreshTokenFactory
        )
        {
            this._sessionFactory = sessionFactory;
            this._sessionRepo = sessionRepository;
            this._userRepo = userRepository;
            this._unitOfWork = unitOfWork;
            this._clientRepo = clientRepository;
            this._refreshTokenFactory = refreshTokenFactory;
        }

        public async Task Run(LoginInput input)
        {
            var client = this._clientRepo.VerifyClientData(
                input.ClientId,
                input.ClientName,
                input.ClientSecret
            );
            if(client is null)
            {
                this._presenter.IncorrectClient();
                return;
            }

            var user = await this._userRepo.CheckCredentials(
                input.username, input.password);

            if(user is null)
            {
                this._presenter.IncorrectCredentials();
                return;
            }

            var session = this._sessionFactory.CreateSession(user);

            if(input.RememberLogin)
                session.RefreshToken = this._refreshTokenFactory.CreateRefreshToken();
            
            await this._sessionRepo.AddSession(session);

            await this._unitOfWork.Save();

            if(client.PostLoginRedirect is null)
                this._presenter.SessionCreated(session, input.RememberLogin);
            else
                this._presenter.RedirectCreatedSession(session, client.PostLoginRedirect, input.RememberLogin);
        }

        public void SetPresenter(ILoginPresenter presenter)
            => this._presenter = presenter;
    }
}
