using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Sinuka.Application.UseCases.Refresh;
using Sinuka.Core.Models;

namespace Sinuka.WebAPIs.UseCases.Refresh
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SessionController : ControllerBase, IRefreshPresenter
    {
        private IActionResult _viewModel;
        private readonly IRefreshUseCase _useCase;

        public SessionController(IRefreshUseCase useCase)
            => this._useCase = useCase;

        void IRefreshPresenter.Done(Session session)
            => this._viewModel = Ok(new {
                session_token = session.SessionToken.Token,
                session_token_expiry = session.SessionToken.ExpiresAt,
                refresh_token = session.RefreshToken.Token,
                refresh_token_expiry = session.RefreshToken.ExpiresAt,
            });

        void IRefreshPresenter.InvalidToken()
            => this._viewModel = BadRequest(new {message = "Invalid Refresh Token"});

        [HttpPut]
        public async Task<IActionResult> Refresh([FromBody] RefreshInput input)
        {
            var result = new RefreshInputValidation().Validate(input);
            if(!result.IsValid)
                return this.BadRequest(result.Errors);

            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }

    public class RefreshInputValidation : AbstractValidator<RefreshInput>
    {
        public RefreshInputValidation()
        {
            RuleFor(r => r.RefreshToken).NotEmpty();
        }
    }
}
