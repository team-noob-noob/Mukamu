using Sinuka.Core.Models;

namespace Sinuka.Application.UseCases.Login
{
    public interface ILoginPresenter
    {
        void IncorrectCredentials();
        void IncorrectClient();
        void SessionCreated(Session session);
        void RedirectCreatedSession(Session session, string url);
    }
}
