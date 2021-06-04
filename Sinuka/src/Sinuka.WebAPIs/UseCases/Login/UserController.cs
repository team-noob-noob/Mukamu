using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinuka.Application.UseCases.Login;

namespace Sinuka.WebAPIs.UseCases.Login
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase, ILoginPresenter
    {
        private IActionResult _viewModel;
        private readonly ILoginUseCase _useCase;

        public UserController(ILoginUseCase useCase)
            => this._useCase = useCase;

        void ILoginPresenter.IncorrectCredentials()
            => this._viewModel = this.BadRequest(new {message = "Incorrect username or password"});

        void ILoginPresenter.SessionCreated(string session)
            => this._viewModel = this.Ok(new {session});

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginInput input)
        {
            this._useCase.SetPresenter(this);

            await this._useCase.Run(input);

            return this._viewModel;
        }
    }
}
