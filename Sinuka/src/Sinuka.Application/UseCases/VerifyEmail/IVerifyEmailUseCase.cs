using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.VerifyEmail
{
    public interface IVerifyEmailUseCase
    {
        void SetPresenter(IVerifyEmailPresenter presenter);
        Task Run(VerifyEmailInput input);
    }
}
