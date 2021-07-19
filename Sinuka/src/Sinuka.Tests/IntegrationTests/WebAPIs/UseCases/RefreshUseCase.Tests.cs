using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Bogus;
using FluentAssertions;
using Sinuka.WebAPIs;
using Sinuka.Application.UseCases.Refresh;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Services;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    public class RefreshUseCaseTests
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

                var password = new Bogus.DataSets.Internet().Password();
                var testUser = new User(
                    new Bogus.DataSets.Internet().UserName(),
                    new BcryptHashingService().Hash(password),
                    new EmailAddress(
                        new Bogus.DataSets.Internet().Email(),
                        new Bogus.Randomizer().AlphaNumeric(10)
                    )
                );

                try
                {
                    db.Users.Add(testUser);
                    db.SaveChanges();

                    var client = this._factory.CreateClient();
                    var input = new LoginInput()
                    {
                        username = testUser.Username,
                        password = password,
                        
                        ClientId = Utilities.TestClient.Id,
                        ClientName = Utilities.TestClient.Name,
                        ClientSecret = Utilities.TestClient.Secret,
                        RememberLogin = true
                    };
                    var body = JsonContent.Create(input);
                    var response = await client.PostAsync("/session/login", body);
                    response.EnsureSuccessStatusCode();

                    var session = db.Sessions.Include(s => s.RefreshToken).First();
                    this._sessionToken = session.RefreshToken.Token;
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
        public async Task Refresh_ShouldReturn200_IfValidSessionToken()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var input = new RefreshInput()
            {
                RefreshToken = this._sessionToken,
            };
            var body = JsonContent.Create(input);

            // Act
            var response = await client.PutAsync("/session/refresh", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Refresh_ShouldReturn400_IfInvalidSessionToken()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new RefreshInput()
            {
                RefreshToken = "THIS IS A TEST",
            };
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PutAsync("/session/refresh", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Refresh_ShouldReturn400_IfEmptyInputObject()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new RefreshInput();
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PutAsync("/session/refresh", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
