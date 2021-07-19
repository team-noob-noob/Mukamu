using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /// <summary>Removes the Session Token and Refresh Token as valid</summary>
        /// <returns>Returns an indication that removal is complete</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that one of the property in the input is missing/incorrect</response>
        /// <response code="302">Redirects to the client application to notify that the removal is complete</response>
        /// <response code="200">Indicates that the removal is complete</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
