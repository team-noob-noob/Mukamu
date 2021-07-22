using System.Threading.Tasks;

namespace Mukamu.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Save();
    }
}
