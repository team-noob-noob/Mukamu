using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.PasswordReset;

namespace Sinuka.WebAPIs.UseCases.PasswordReset
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class UserController : ControllerBase, IPasswordResetPresenter
    {
        private readonly IPasswordResetUseCase _useCase;
        private IActionResult? _viewModel;

        public UserController(IPasswordResetUseCase useCase)
            => this._useCase = useCase;

        void IPasswordResetPresenter.Done()
            => this._viewModel = Ok(new { Message = "Password Reset Complete" });

        void IPasswordResetPresenter.InvalidResetToken()
            => this._viewModel = BadRequest(new { Message = "Invalid Reset Token" });

        [HttpPut]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetInput input)
        {
            var result = new PasswordResetInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public sealed class PasswordResetInputValidation : AbstractValidator<PasswordResetInput>
    {
        public PasswordResetInputValidation()
        {
            RuleFor(x => x.NewPassword).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.NewPassword);
            RuleFor(x => x.ResetToken).NotEmpty();
        }
    }
}
