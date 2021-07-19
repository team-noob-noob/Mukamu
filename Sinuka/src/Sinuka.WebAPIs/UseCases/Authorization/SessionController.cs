using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.Authorization;


namespace Sinuka.WebAPIs.UseCases.Authorization
{
    [Produces("application/json")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class SessionController : ControllerBase, IAuthorizationPresenter
    {
        private IActionResult _viewModel = null;
        private readonly IAuthorizationUseCase _useCase;

        public SessionController(IAuthorizationUseCase useCase)
            => this._useCase = useCase;

        void IAuthorizationPresenter.InvalidToken()
            => this._viewModel = this.Accepted(new { IsTokenValid = false });

        void IAuthorizationPresenter.ValidToken()
            => this._viewModel = this.Ok(new { IsTokenValid = true });

        /// <summary>Checks whether the given SessionToken is valid or not</summary>
        /// <returns>Flag/bool whether the Session Token is valid or not</returns>
        /// <response code="202">Indicates that the Session Token is invalid</response>
        /// <response code="200">Indicates that the Session Token is valid</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Authorize([FromQuery] AuthorizationInput input)
        {
            var result = new AuthorizationInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public class AuthorizationInputValidation : AbstractValidator<AuthorizationInput>
    {
        public AuthorizationInputValidation()
        {
            RuleFor(c => c.Token).NotNull().NotEmpty();
        }
    }
}
