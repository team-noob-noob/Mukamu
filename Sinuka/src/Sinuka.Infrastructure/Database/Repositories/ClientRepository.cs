using System;
using System.Linq;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ISinukaDbContext _dbContext;

        public ClientRepository(ISinukaDbContext dbContext)
            => this._dbContext = dbContext;
        
        public Client? VerifyClientData(Guid id, string name, string secret)
            => this._dbContext.Clients
                    .FirstOrDefault(c => c.Id.Equals(id) && c.Name.Equals(name) && c.Secret.Equals(secret));
    }
}
