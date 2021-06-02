using System.Threading.Tasks;
using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User newUser);
        Task<User?> FindUserByUsername(string username);
        Task<User?> FindUserByEmail(string email);
        Task<bool> CheckCredentials(string username, string password);
    }
}
