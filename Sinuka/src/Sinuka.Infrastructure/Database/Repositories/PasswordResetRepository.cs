using System.Linq;
using System.Threading.Tasks;
using Sinuka.Core.Interfaces.Repositories;
using Sinuka.Core.Models;

namespace Sinuka.Infrastructure.Database.Repositories
{
    public class PasswordResetRepository : IPasswordResetRepository
    {
        private SinukaDbContext _dbContext;

        public PasswordResetRepository(SinukaDbContext dbContext)
            => this._dbContext = dbContext;

        public async Task AddResetPassword(PasswordReset passwordReset)
            => await this._dbContext.PasswordResets.AddAsync(passwordReset);

        public async Task<PasswordReset?> FindPasswordResetByResetToken(string resetToken)
            => await Task.FromResult(this._dbContext.PasswordResets.FirstOrDefault(x => x.ResetToken == resetToken));
    }
}
