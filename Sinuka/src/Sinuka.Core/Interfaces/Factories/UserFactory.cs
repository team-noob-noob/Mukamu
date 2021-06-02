using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateSession(string username, string password, string email);
    }
}
