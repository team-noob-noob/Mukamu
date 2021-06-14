using System;
using System.Linq;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly SinukaDbContext _dbContext;

        public ClientRepository(SinukaDbContext dbContext)
            => this._dbContext = dbContext;
        

        public async Task<Client?> VerifyClientData(Guid id, string name, string secret)
            => await Task.Run(
                    () => 
                        this._dbContext.Clients
                            .FirstOrDefault(c => c.Id.Equals(id) && c.Name.Equals(name) && c.Secret.Equals(secret))
                );
        
    }
}
