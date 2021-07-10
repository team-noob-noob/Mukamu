using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;

namespace Sinuka.Application.UseCases.SendPasswordReset
{
    public class SendPasswordResetUseCase : ISendPasswordResetUseCase
    {
        private ISendPasswordResetPresenter _presenter;
        private IUserRepository _userRepo;
        private IEmailService _emailService;


        public SendPasswordResetUseCase(
            IUserRepository userRepository,
            IEmailService emailService
        )
        {
            this._userRepo = userRepository;
            this._emailService = emailService;
        }

        public async Task Run(SendPasswordResetInput input)
        {
            var user = await this._userRepo.FindUserByEmail(input.Email);
            if (user is null)
            {
                this._presenter.InvalidEmail();
                return;
            }

            await this._emailService.SendEmail(
                input.Email, 
                SendPasswordResetTemplate.Template($"localhost:8080/test"),
                "Reset Password"
            );

            this._presenter.Done();
        }

        public void SetPresenter(ISendPasswordResetPresenter presenter)
            => this._presenter = presenter;
    }
}
