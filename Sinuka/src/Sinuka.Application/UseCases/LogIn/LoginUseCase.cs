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
        private readonly IUnitOfWork _unitOfWork;

        public LoginUseCase(
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            ISessionFactory sessionFactory,
            IUnitOfWork unitOfWork
        )
        {
            this._sessionFactory = sessionFactory;
            this._sessionRepo = sessionRepository;
            this._userRepo = userRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(LoginInput input)
        {
            var user = await this._userRepo.CheckCredentials(
                input.username, input.password);

            if(user is null)
            {
                this._presenter.IncorrectCredentials();
                return;
            }

            var session = this._sessionFactory.CreateSession(user);

            await this._sessionRepo.AddSession(session);

            await this._unitOfWork.Save();

            this._presenter.SessionCreated(session.Token);
        }

        public void SetPresenter(ILoginPresenter presenter)
            => this._presenter = presenter;
    }
}
