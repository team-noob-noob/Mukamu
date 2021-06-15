using Sinuka.Core.Models;

namespace Sinuka.Application.UseCases.Login
{
    public interface ILoginPresenter
    {
        void IncorrectCredentials();
        void IncorrectClient();
        void SessionCreated(Session session, bool isRefreshable);
        void RedirectCreatedSession(Session session, string url, bool isRefreshable);
    }
}
