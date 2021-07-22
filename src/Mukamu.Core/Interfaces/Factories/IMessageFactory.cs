using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface IMessageFactory
    {
        Message CreateMessage(string actualMessage, User sender);
    }
}
