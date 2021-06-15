using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface ISessionTokenFactory
    {
        SessionToken CreateSessionToken(object payload);
    }
}
