using System;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Database;

namespace Sinuka.Tests.EndToEndTests
{
    public class Utilities
    {
        public static void InitializeDb(SinukaDbContext dbContext)
        {
            dbContext.Clients.Add(new Client("TEST", "TEST", "TEST") { Id = Guid.Parse("69a29c6d-80b1-4c7b-a08b-38c7a27aaf4d") });
        }
    }
}
