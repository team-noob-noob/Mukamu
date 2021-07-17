using System;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Database;

namespace Sinuka.Tests.IntegrationTests
{
    public class Utilities
    {
        public static Client TestClient = new Client("TEST", "TEST", "TEST");

        public static void InitializeDb(SinukaDbContext dbContext)
        {
            dbContext.Clients.Add(TestClient);
        }
    }
}
