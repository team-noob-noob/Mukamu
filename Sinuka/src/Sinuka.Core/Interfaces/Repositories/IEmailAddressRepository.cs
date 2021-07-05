using System.Threading.Tasks;
using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Repositories
{
    public interface IEmailAddressRepository
    {
        Task<EmailAddress?> GetEmailAddressByVerificationString(string verificationString);
    }
}
