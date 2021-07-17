using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using NUnit.Framework;
using Bogus;
using FluentAssertions;
using Sinuka.WebAPIs;
using Sinuka.Application.UseCases.Login;
using Sinuka.Application.UseCases.Register;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    [TestFixture, Category("integration")]
    public class LoginUseCaseTests
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private LoginInput _input = new LoginInput()
        {
            username = new Bogus.DataSets.Internet().UserName(),
            password = new Bogus.DataSets.Internet().Password(),
            
            ClientId = Utilities.TestClient.Id,
            ClientName = Utilities.TestClient.Name,
            ClientSecret = Utilities.TestClient.Secret,
        };

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            this._factory = new CustomWebApplicationFactory<Startup>();

            // Register created credentials
            var registerInput = new RegisterInput()
            {
                Username = this._input.username,
                Password = this._input.password,
                Email = new Bogus.DataSets.Internet().Email(),
            };

            var client =  this._factory.CreateClient();

            var response = await client.PostAsync("/user/register", JsonContent.Create(registerInput));

            response.EnsureSuccessStatusCode();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this._factory.ClearDb();
        }

        [Test]
        public async Task Login_ShouldReturn200_IfGivenValidCredentials()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var body = JsonContent.Create(this._input);

            // Act
            var response = await client.PostAsync("/session/login", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Login_ShouldReturn400_IfGivenInvalidUsername()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new LoginInput()
            {
                username = this._input.username,
                password = new Bogus.DataSets.Internet().Password(),
                
                ClientId = Utilities.TestClient.Id,
                ClientName = Utilities.TestClient.Name,
                ClientSecret = Utilities.TestClient.Secret,
            };
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PostAsync("/session/login", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ShouldReturn400_IfGivenInvalidPassword()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new LoginInput()
            {
                username = new Bogus.DataSets.Internet().UserName(),
                password = this._input.password,
                
                ClientId = Utilities.TestClient.Id,
                ClientName = Utilities.TestClient.Name,
                ClientSecret = Utilities.TestClient.Secret,
            };
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PostAsync("/session/login", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ShouldReturn400_IfGivenInvalidClient()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new LoginInput()
            {
                username = new Bogus.DataSets.Internet().UserName(),
                password = new Bogus.DataSets.Internet().Password(),
                
                ClientId = System.Guid.NewGuid(),
                ClientName = "",
                ClientSecret = "",
            };
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PostAsync("/session/login", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Login_ShouldReturn400_IfGivenEmptyObject()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new LoginInput();
            var body = JsonContent.Create(badInput);

            // Act
            var response = await client.PostAsync("/session/login", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
