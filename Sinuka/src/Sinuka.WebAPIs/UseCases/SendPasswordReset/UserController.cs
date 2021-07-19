using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.SendPasswordReset;

namespace Sinuka.WebAPIs.UseCases.SendPasswordReset
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase, ISendPasswordResetPresenter
    {
        private readonly ISendPasswordResetUseCase _useCase;
        private IActionResult _viewModel;

        public UserController(ISendPasswordResetUseCase useCase)
            => this._useCase = useCase;

        void ISendPasswordResetPresenter.Done()
            => this._viewModel = this.Ok(new { Message = "Email Sent" });

        void ISendPasswordResetPresenter.InvalidEmail()
            => this._viewModel = this.BadRequest(new { Message = "Invalid Email" });
        
        /// <summary>Sends an email to the user's email</summary>
        /// <returns>Indicates whether the email hs been sent</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that the email is invalid, or one of the property in the input is missing/incorrect</response>
        /// <response code="200">Indicates that the email has been sent</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendPasswordResetEmail([FromBody] SendPasswordResetInput input)
        {
            var result = new SendPasswordResetInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public sealed class SendPasswordResetInputValidation : AbstractValidator<SendPasswordResetInput>
    {
        public SendPasswordResetInputValidation()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}