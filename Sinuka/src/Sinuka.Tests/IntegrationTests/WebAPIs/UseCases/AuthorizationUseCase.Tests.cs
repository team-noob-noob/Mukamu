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
using Sinuka.Application.UseCases.Authorization;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Services;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    [TestFixture, Category("integration")]
    public class AuthorizationUseCaseTests
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
        public async Task Authorize_ShouldReturn200_IfValidSessionToken()
        {
            // Arrange
            var client = this._factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/session/authorize/?sessionToken={this._sessionToken}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Authorize_ShouldReturn400_IfInvalidSessionToken()
        {
            // Arrange
            var client = this._factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/session/authorize/?session=Token=THIS IS A TEST");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
