using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.Register
{
    public interface IRegisterUseCase
    {
        Task Run(RegisterInput input);
        void SetPresenter(IRegisterPresenter presenter);
    }
}
