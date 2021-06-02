using System.Threading.Tasks;
using System.Linq;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SinukaDbContext _dbContext;

        public UserRepository(SinukaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task AddUser(User newUser) => await this._dbContext.Users.AddAsync(newUser);

        public async Task<bool> CheckCredentials(string username, string password)
        {
            var user = await this.FindUserByUsername(username);
            if(user is null) return false;
            return BCrypt.Net.BCrypt.EnhancedVerify(password, user.HashedPassword);
        }

        public async Task<User?> FindUserByEmail(string email) 
            => await Task.FromResult(this._dbContext.Users.Where(u => u.Email == email).ElementAt(0));

        public async Task<User?> FindUserByUsername(string username) 
            => await Task.FromResult(this._dbContext.Users.Where(u => u.Username == username).ElementAt(0));
    }
}
