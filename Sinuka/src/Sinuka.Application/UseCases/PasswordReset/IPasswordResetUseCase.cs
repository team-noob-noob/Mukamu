using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.PasswordReset
{
    public interface IPasswordResetUseCase
    {
        Task Run(PasswordResetInput input);
        void SetPresenter(IPasswordResetPresenter presenter);
    }
}