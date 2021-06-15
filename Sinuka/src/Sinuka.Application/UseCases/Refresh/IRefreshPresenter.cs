using Sinuka.Core.Models;

namespace Sinuka.Application.UseCases.Refresh
{
    public interface IRefreshPresenter
    {
        void InvalidToken();
        void Done(Session session);
    }
}
