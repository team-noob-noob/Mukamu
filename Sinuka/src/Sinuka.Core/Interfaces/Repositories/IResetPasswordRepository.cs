using System.Threading.Tasks;
using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Repositories
{
    public interface IPasswordResetRepository
    {
        Task AddResetPassword(PasswordReset passwordReset);
        Task<PasswordReset?> FindPasswordResetByResetToken(string resetToken);
    }
}
