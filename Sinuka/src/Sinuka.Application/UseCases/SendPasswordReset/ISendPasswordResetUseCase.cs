using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.SendPasswordReset
{
    public interface ISendPasswordResetUseCase
    {
        void SetPresenter(ISendPasswordResetPresenter presenter);
        Task Run(SendPasswordResetInput input);
    }
}
