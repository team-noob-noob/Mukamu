using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.Authorization;

namespace Sinuka.WebAPIs.UseCases.Authorization
{
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

        [HttpGet]
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
