using System;
using System.Threading.Tasks;
using Sinuka.Core.Models;

namespace Sinuka.Core.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Client? VerifyClientData(Guid id, string name, string secret);
    }
}
