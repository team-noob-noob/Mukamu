using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.Logout
{
    public interface ILogoutUseCase
    {
        Task Run(LogoutInput input);
        void SetPresenter(ILogoutPresenter presenter);
    }
}
