using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sinuka.Application.UseCases.Register;

namespace Sinuka.WebAPIs.UseCases.Register
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterInput input)
        {
            this._registerUseCase.SetPresenter(this);

            await this._registerUseCase.Run(input);

            return this._viewModel;
        }
    }    
}
