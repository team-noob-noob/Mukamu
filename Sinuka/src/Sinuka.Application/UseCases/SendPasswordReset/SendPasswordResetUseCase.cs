using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.SendPasswordReset
{
    public class SendPasswordResetUseCase : ISendPasswordResetUseCase
    {
        private ISendPasswordResetPresenter _presenter;
        private IUserRepository _userRepo;
        private IEmailService _emailService;
        private IPasswordResetRepository _passwordResetRepo;
        private IPasswordResetFactory _passwordResetFactory;
        private IUnitOfWork _unitOfWork;

        public SendPasswordResetUseCase(
            IUserRepository userRepository,
            IEmailService emailService,
            IPasswordResetRepository passwordResetRepository,
            IPasswordResetFactory passwordResetFactory,
            IUnitOfWork unitOfWork
        )
        {
            this._userRepo = userRepository;
            this._emailService = emailService;
            this._passwordResetRepo = passwordResetRepository;
            this._passwordResetFactory = passwordResetFactory;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(SendPasswordResetInput input)
        {
            var user = await this._userRepo.FindUserByEmail(input.Email);
            if (user is null)
            {
                this._presenter.InvalidEmail();
                return;
            }

            var passwordResetToken = this._passwordResetFactory.CreatePasswordReset(user);
            await this._passwordResetRepo.AddResetPassword(passwordResetToken);

            await this._emailService.SendEmail(
                input.Email, 
                SendPasswordResetTemplate.Template($"localhost:8080/test/{passwordResetToken.ResetToken}"),
                "Reset Password"
            );

            this._presenter.Done();
        }

        public void SetPresenter(ISendPasswordResetPresenter presenter)
            => this._presenter = presenter;
    }
}
