using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Models;
using Sinuka.WebAPIs.BackgroundProcess;

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

        void ILoginPresenter.SessionCreated(Session session)
        {
            this._viewModel = this.Ok(new {session.Token, session.ExpiresAt});
            this._jobClient.Schedule<ExpiredSessionRemover>(
                x => x.RemoveSession(session.Token), 
                Sinuka.Infrastructure.Configurations.TokenConfig.TokenLifetimeLength);
        }

        void ILoginPresenter.IncorrectClient()
        {
            this._viewModel = this.BadRequest(new {message = "Client details are invalid"});
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }
}
