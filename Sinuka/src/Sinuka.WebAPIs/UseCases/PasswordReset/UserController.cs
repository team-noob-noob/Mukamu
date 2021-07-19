using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /// <summary>Resets the password if the given Reset Token is valid</summary>
        /// <returns>Returns an indication if the reset is complete</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that the reset token is invalid or one of the property in the input is missing/incorrect</response>
        /// <response code="200">Indicates that the reset is complete</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
