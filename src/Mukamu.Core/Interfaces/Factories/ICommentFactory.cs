using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface ICommentFactory
    {
        Comment CreateComment(string message, User commenter);
    }
}
