using System.Collections.Generic;
using Mukamu.Core.Models;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface IConverstationFactory
    {
        Conversation CreateConversation(ICollection<User> users);
    }
}
