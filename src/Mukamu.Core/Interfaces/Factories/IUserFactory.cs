using Mukamu.Core.Models;
using System;

namespace Mukamu.Core.Interfaces.Factories
{
    public interface IUserFactory
    {
        User CreateUser(Guid externalId);
    }
}
