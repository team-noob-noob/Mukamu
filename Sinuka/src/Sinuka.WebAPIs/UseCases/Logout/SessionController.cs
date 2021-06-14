using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.Logout;

namespace Sinuka.WebAPIs.UseCases.Logout
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SessionController : ControllerBase, ILogoutPresenter
    {
        private readonly ILogoutUseCase _useCase;
        private IActionResult _viewModel;

        public SessionController(ILogoutUseCase useCase)
            => this._useCase = useCase;

        void ILogoutPresenter.Done()
            => this._viewModel = this.Ok(new {message="Logged out"});

        void ILogoutPresenter.Redirect(string url)
            => this._viewModel = this.Redirect(url);

        [HttpPut]
        public async Task<IActionResult> Logout([FromBody] LogoutInput input)
        {
            var result = new LogoutInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public class LogoutInputValidation : AbstractValidator<LogoutInput>
    {
        public LogoutInputValidation()
        {
            RuleFor(l => l.Token).NotNull().NotEmpty();
        }
    }
}
