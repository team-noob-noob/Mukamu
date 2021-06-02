using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Factories;
using Sinuka.Application.Interfaces;

namespace Sinuka.Application.UseCases.Register
{
    public class RegisterUseCase
    {
        private readonly IRegisterPresenter _presenter;
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUseCase(
            IRegisterPresenter presenter,
            IUserRepository userRepository, 
            IUserFactory userFactory,
            IUnitOfWork unitOfWork)
        {
            this._presenter = presenter;
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
    }
}
