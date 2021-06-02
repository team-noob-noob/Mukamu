using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.Register
{
    public class RegisterUseCase : IRegisterUseCase
    {
        private IRegisterPresenter _presenter;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUseCase(
            IUserRepository userRepository, 
            IUserFactory userFactory,
            IUnitOfWork unitOfWork)
        {
            this._userRepository = userRepository;
            this._userFactory = userFactory;
            this._unitOfWork = unitOfWork;
        }

        public async Task Run(RegisterInput input)
        {
            if(await this._userRepository.FindUserByUsername(input.Username) is not null)
            {
                this._presenter.UsernameIsTaken();
                return;
            }

            if(await this._userRepository.FindUserByEmail(input.Email) is not null)
            {
                this._presenter.EmailIsTaken();
                return;
            }

            var user = this._userFactory.CreateUser(input.Username, input.Password, input.Email);

            await this._userRepository.AddUser(user);

            await this._unitOfWork.Save();

            this._presenter.UserCreated();
        }

        public void SetPresenter(IRegisterPresenter presenter) => this._presenter = presenter;
    }
}
