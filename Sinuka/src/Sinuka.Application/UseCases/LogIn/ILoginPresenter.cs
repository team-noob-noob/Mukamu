using Sinuka.Core.Models;

namespace Sinuka.Application.UseCases.Login
{
    public interface ILoginPresenter
    {
        void IncorrectCredentials();
        void SessionCreated(Session session);
    }
}
