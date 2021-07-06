using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.VerifyEmail
{
    public class VerifyEmailUseCase : IVerifyEmailUseCase
    {
        private IVerifyEmailPresenter _presenter;
        private readonly IEmailAddressRepository _emailRepo;
        private readonly IUnitOfWork _unitOfWork;

        public VerifyEmailUseCase(
            IEmailAddressRepository emailAddressRepository,
            IUnitOfWork unitOfWork
        )
        {
            this._emailRepo = emailAddressRepository;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(VerifyEmailInput input)
        {
            var email = await this._emailRepo.GetEmailAddressByVerificationString(input.VerificationString);
            if (email is null)
            {
                this._presenter.InvalidVerifyString();
                return;
            }

            email.VerificationString = null;

            await this._unitOfWork.Save();

            this._presenter.Done();
        }

        public void SetPresenter(IVerifyEmailPresenter presenter)
            => this._presenter = presenter;
    }
}
