using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.PasswordReset
{
    public class PasswordResetUseCase : IPasswordResetUseCase
    {
        private IPasswordResetPresenter _presenter;
        private readonly IPasswordResetRepository _passwordResetRepo;
        private readonly IHashingService _hashingService;
        private readonly IUnitOfWork _unitOfWork;

        public PasswordResetUseCase(
            IPasswordResetRepository passwordResetRepository,
            IHashingService hashingService,
            IUnitOfWork unitOfWork
        )
        {
            this._passwordResetRepo = passwordResetRepository;
            this._hashingService = hashingService;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(PasswordResetInput input)
        {
            var passwordReset 
                = await this._passwordResetRepo
                .FindPasswordResetByResetToken(input.ResetToken);
            if (passwordReset is null || passwordReset.IsExpired())
            {
                this._presenter.InvalidResetToken();
                return;
            }

            var hashedNewPassword = this._hashingService.Hash(input.NewPassword);
            
            passwordReset.User.HashedPassword = hashedNewPassword;

            await this._unitOfWork.Save();

            this._presenter.Done();
        }

        public void SetPresenter(IPasswordResetPresenter presenter)
            => this._presenter = presenter;
    }
}
