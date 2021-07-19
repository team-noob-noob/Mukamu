using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Hangfire;
using FluentValidation;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Models;
using System.Dynamic;

namespace Sinuka.WebAPIs.UseCases.Login
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SessionController : ControllerBase, ILoginPresenter
    {
        private IActionResult _viewModel;
        private readonly ILoginUseCase _useCase;

        public SessionController(ILoginUseCase useCase)
        {
            this._useCase = useCase;
        }

        void ILoginPresenter.IncorrectCredentials()
            => this._viewModel = this.BadRequest(new {message = "Incorrect username or password"});

        void ILoginPresenter.IncorrectClient()
            => this._viewModel = this.BadRequest(new {message = "Client details are invalid"});

        void ILoginPresenter.SessionCreated(Session session, bool isRefreshable)
        {
            dynamic payload = new ExpandoObject();
            payload.session_token = session.SessionToken.Token;
            payload.session_token_expiry = session.SessionToken.ExpiresAt;
            
            if(isRefreshable)
            {
                payload.refresh_token = session.RefreshToken.Token;
                payload.refresh_token_expiry = session.RefreshToken.ExpiresAt;
            }

            this._viewModel = Ok(payload);
        }

        void ILoginPresenter.RedirectCreatedSession(Session session, string url, bool isRefreshable)
        {
            if(isRefreshable)
            {
                url = QueryHelpers.AddQueryString(url, "refresh_token", session.RefreshToken.Token);
                url = QueryHelpers.AddQueryString(url, "refresh_token_expiry", session.RefreshToken.ExpiresAt.ToString());
            }

            url = QueryHelpers.AddQueryString(url, "session_token", session.SessionToken.Token);
            url = QueryHelpers.AddQueryString(url, "session_token_expiry", session.SessionToken.ExpiresAt.ToString());

            this._viewModel = Redirect(url);
        }

        /// <summary>Checks whether the given credentials are valid</summary>
        /// <returns>SessionToken and RefreshToken</returns>
        /// <response code="500">Indicates that an unhandled error/exception occured</response>
        /// <response code="400">Indicates that either the client or account credentials are invalid, or one of the property in the input is missing/incorrect</response>
        /// <response code="302">Returns the URL to the client with Session Token and Refresh Token as query parameters</response>
        /// <response code="200">Returns the Session Token and Refresh Token</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            var result = new LoginInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public class LoginInputValidation : AbstractValidator<LoginInput>
    {
        public LoginInputValidation()
        {
            RuleFor(l => l.ClientId).NotEmpty().NotNull();
            RuleFor(l => l.ClientName).NotEmpty().NotNull();
            RuleFor(l => l.ClientSecret).NotEmpty().NotNull();
            RuleFor(l => l.username).NotNull();
            RuleFor(l => l.password).NotNull();
            RuleFor(l => l.RememberLogin).NotNull();
        }
    }
}
