using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface IPasswordResetFactory
    {
        PasswordReset CreatePasswordReset(User user);
    }
}
