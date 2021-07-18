using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Bogus;
using FluentAssertions;
using Sinuka.Core.Models;
using Sinuka.WebAPIs;
using Sinuka.Application.UseCases.Logout;
using Sinuka.Application.UseCases.Login;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    [TestFixture, Category("integration")]
    public class LogoutUseCaseTests
    {
        private CustomWebApplicationFactory<Startup> _factory;

        private string _sessionToken;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            this._factory = new CustomWebApplicationFactory<Startup>();

            // TODO: add types to responses to read from JSON instead
            using (var scope = this._factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<SinukaTestDbContext>();

                try
                {
                    db.Users.Add(Utilities.TestUser);
                    db.SaveChanges();

                    var client = this._factory.CreateClient();
                    var input = new LoginInput()
                    {
                        username = Utilities.TestUser.Username,
                        password = "test",
                        
                        ClientId = Utilities.TestClient.Id,
                        ClientName = Utilities.TestClient.Name,
                        ClientSecret = Utilities.TestClient.Secret,
                    };
                    var body = JsonContent.Create(input);
                    var response = await client.PostAsync("/session/login", body);
                    response.EnsureSuccessStatusCode();

                    var session = db.Sessions.Include(s => s.SessionToken).First();
                    this._sessionToken = session.SessionToken.Token;
                }
                catch
                {
                    throw;
                }
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this._factory.ClearDb();
        }

        [Test]
        public async Task Logout_ShouldReturn200_IfGivenValidSessionToken()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var input = new LogoutInput()
            {
                Token = this._sessionToken,
            };
            var body = JsonContent.Create(input);

            // Act
            var response = await client.PutAsync("/session/logout", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
