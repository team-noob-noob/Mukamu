using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Factories
{
    public interface IRoleFactory
    {
        Role CreateRole(string name, string description);
    }
}
