using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /// <summary>Marks an email as verified with the given verification string sent from the email</summary>
        /// <returns>Indicates whether the email has been verified</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that the verification string is invalid, or one of the property in the input is missing/incorrect</response>
        /// <response code="200">Indicates that the email has been marked as verified</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> VerifyEmail([FromQuery] VerifyEmailInput input)
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
