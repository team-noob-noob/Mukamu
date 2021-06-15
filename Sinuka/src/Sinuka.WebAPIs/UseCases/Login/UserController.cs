using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class UserController : ControllerBase, ILoginPresenter
    {
        private IActionResult _viewModel;
        private readonly ILoginUseCase _useCase;
        private readonly IBackgroundJobClient _jobClient;

        public UserController(ILoginUseCase useCase, IBackgroundJobClient jobClient)
        {
            this._useCase = useCase;
            this._jobClient = jobClient;
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

        [HttpPost]
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
