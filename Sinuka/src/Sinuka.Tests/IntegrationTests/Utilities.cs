using System;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Database;
using Sinuka.Infrastructure.Services;

namespace Sinuka.Tests.IntegrationTests
{
    public class Utilities
    {
        public static Client TestClient = new Client("TEST", "TEST", "TEST");
        public static User TestUser = new User(
            "test", 
            new BcryptHashingService().Hash("test"),
            new EmailAddress("test@test.test", "test")
        );

        public static void InitializeDb(SinukaDbContext dbContext)
        {
            dbContext.Clients.Add(TestClient);
            dbContext.Users.Add(TestUser);
            dbContext.SaveChanges();
        }

        public static void ReinitializeDb(SinukaDbContext dbContext)
        {
            dbContext.Users.Clear();
            dbContext.Clients.Clear();
            dbContext.SaveChanges();
        }
    }
}
