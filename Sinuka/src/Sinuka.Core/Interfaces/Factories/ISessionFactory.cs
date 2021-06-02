using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface ISessionFactory
    {
        Session CreateSession(User user, string token);
    }
}