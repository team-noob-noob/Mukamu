using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.Register;

namespace Sinuka.WebAPIs.UseCases.Register
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class UserController : ControllerBase, IRegisterPresenter
    {
        private readonly IRegisterUseCase _registerUseCase;
        private IActionResult? _viewModel;

        public UserController(IRegisterUseCase registerUseCase)
            => this._registerUseCase = registerUseCase;

        void IRegisterPresenter.UsernameIsTaken()
            => this._viewModel = this.BadRequest(new { Message = "Username is already taken" });

        void IRegisterPresenter.EmailIsTaken()
            => this._viewModel = this.BadRequest(new { Message = "Email is already taken" });

        void IRegisterPresenter.UserCreated()
            => this._viewModel = this.Ok(new { Message = "User is created" });

        /// <summary>Creates a new User/Account</summary>
        /// <returns>Returns the user id</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that the username or email is taken, or one of the property in the input is missing/incorrect</response>
        /// <response code="200">Returns the user id</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterInput input)
        {
            var result = new RegisterInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._registerUseCase.SetPresenter(this);

            await this._registerUseCase.Run(input);

            return this._viewModel;
        }
    }

    public sealed class RegisterInputValidation : AbstractValidator<RegisterInput>
    {
        public RegisterInputValidation()
        {
            RuleFor(r => r.Email).NotEmpty().NotNull();
            RuleFor(r => r.Password).NotEmpty().NotNull();
            RuleFor(r => r.Username).NotEmpty().NotNull();
        }
    }
}
