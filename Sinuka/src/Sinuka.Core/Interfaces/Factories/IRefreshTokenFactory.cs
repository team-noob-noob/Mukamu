using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface IRefreshTokenFactory
    {
        RefreshToken CreateRefreshToken();
    }
}
