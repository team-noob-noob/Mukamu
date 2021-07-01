using System;
using System.Threading.Tasks;
using Sinuka.Application.Interfaces;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;

namespace Sinuka.Application.UseCases.Refresh
{
    public class RefreshUseCase : IRefreshUseCase
    {
        private IRefreshPresenter _presenter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionRepository _sessionRepo;
        private readonly IRefreshTokenFactory _refreshTokenFactory;
        private readonly ISessionFactory _sessionFactory;

        public RefreshUseCase(
            ISessionRepository sessionRepo,
            IRefreshTokenFactory refreshTokenFactory,
            ISessionFactory sessionFactory,
            IUnitOfWork unitOfWork
        )
        {
            this._sessionRepo = sessionRepo;
            this._refreshTokenFactory = refreshTokenFactory;
            this._sessionFactory = sessionFactory;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(RefreshInput input)
        {
            var session = await this._sessionRepo.FindSessionByRefreshToken(input.RefreshToken);
            if (session is null || session.RefreshToken is null || session.RefreshToken.IsExpired())
            {
                this._presenter.InvalidToken();
                return;
            }

            // Only used to get the new ExpiresAt
            var newRefreshToken = this._refreshTokenFactory.CreateRefreshToken();
            session.RefreshToken.ExpiresAt = newRefreshToken.ExpiresAt;

            session.SessionToken = this._sessionFactory.CreateSession(session.User).SessionToken;

            await this._unitOfWork.Save();
        }

        public void SetPresenter(IRefreshPresenter presenter)
            => this._presenter = presenter;
    }
}
