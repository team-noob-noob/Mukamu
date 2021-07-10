using System.Threading.Tasks;
using System.Linq;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Interfaces.Services;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SinukaDbContext _dbContext;
        private readonly IHashingService _hashingService;

        public UserRepository(SinukaDbContext dbContext, IHashingService hashingService)
        {
            this._dbContext = dbContext;
            this._hashingService = hashingService;
        }

        public async Task AddUser(User newUser) => await this._dbContext.Users.AddAsync(newUser);

        public async Task<User?> CheckCredentials(string username, string password)
        {
            var user = await this.FindUserByUsername(username);
            if(user is null) return null;
            return this._hashingService.Compare(password, user.HashedPassword) ? user : null;
        }

        public async Task<User?> FindUserByEmail(string email) 
        {
            var result = await Task.FromResult(
                this._dbContext.Users.FirstOrDefault(
                    u => !u.DeletedAt.HasValue && u.Email.Email == email)
                    
            );
            return result;
        }

        public async Task<User?> FindUserByUsername(string username) 
        {
            var result = await Task.FromResult(
                this._dbContext.Users.FirstOrDefault(
                    u => !u.DeletedAt.HasValue && u.Username == username)
            );
            return result;
        }
    }
}
