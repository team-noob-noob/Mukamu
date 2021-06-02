using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string username, string password, string email);
    }
}
