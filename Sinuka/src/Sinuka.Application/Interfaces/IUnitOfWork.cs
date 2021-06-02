using System.Threading.Tasks;

namespace Sinuka.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public Task<int> Save();
    }
}
