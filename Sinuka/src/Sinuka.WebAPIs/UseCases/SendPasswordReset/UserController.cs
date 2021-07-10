using System.Threading.Tasks;
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
        
        [HttpPost]
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