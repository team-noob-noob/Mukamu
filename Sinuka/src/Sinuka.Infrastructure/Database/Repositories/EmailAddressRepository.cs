using System.Linq;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class EmailAddressRepository : IEmailAddressRepository
    {
        private readonly ISinukaDbContext _dbContext;

        public EmailAddressRepository(ISinukaDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task<EmailAddress?> GetEmailAddressByVerificationString(string verificationString)
            => await Task.FromResult(this._dbContext.EmailAddresses.FirstOrDefault(x => x.VerificationString == verificationString));
    }
}
