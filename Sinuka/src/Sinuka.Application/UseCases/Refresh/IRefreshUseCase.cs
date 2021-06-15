using System.Threading.Tasks;

namespace Sinuka.Application.UseCases.Refresh
{
    public interface IRefreshUseCase
    {
        Task Run(RefreshInput input);
        void SetPresenter(IRefreshPresenter presenter);
    }
}
