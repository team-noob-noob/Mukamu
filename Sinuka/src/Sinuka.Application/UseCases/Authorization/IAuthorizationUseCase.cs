using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.Authorization
{
    public interface IAuthorizationUseCase
    {
        Task Run(AuthorizationInput input);
        void SetPresenter(IAuthorizationPresenter presenter);
    }
}
