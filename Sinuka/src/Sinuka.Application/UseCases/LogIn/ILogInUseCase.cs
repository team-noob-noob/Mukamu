using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.Login
{
    public interface ILoginUseCase
    {
        Task Run(LoginInput input);
        void SetPresenter(ILoginPresenter presenter);
    }
}
