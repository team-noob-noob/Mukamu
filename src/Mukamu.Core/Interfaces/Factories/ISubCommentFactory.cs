using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface ISubCommentFactory
    {
        SubComment CreateSubComment(string message, User commenter);
    }
}
