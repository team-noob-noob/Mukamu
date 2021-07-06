using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.VerifyEmail;

namespace Sinuka.WebAPIs.UseCases.VerifyEmail
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class EmailAddressController : ControllerBase, IVerifyEmailPresenter
    {
        private readonly IVerifyEmailUseCase _useCase;
        private IActionResult? _viewModel;

        public EmailAddressController(IVerifyEmailUseCase useCase)
            => this._useCase = useCase;

        void IVerifyEmailPresenter.Done()
            => this._viewModel = this.Ok(new { message = "Email Verified" });

        void IVerifyEmailPresenter.InvalidVerifyString()
            => this._viewModel = this.BadRequest(new { message = "Invalid Verification String" });

        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailInput input)
        {
            var result = new VerifyEmailInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);
            
            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public sealed class VerifyEmailInputValidation : AbstractValidator<VerifyEmailInput>
    {
        public VerifyEmailInputValidation()
        {
            RuleFor(r => r.VerificationString).NotEmpty();
        }
    }
}
