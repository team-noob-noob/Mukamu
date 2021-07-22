using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface IPostFactory
    {
        Post CreatePost(string message, User poster);
    }
}
