using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net;
using NUnit.Framework;
using Bogus;
using FluentAssertions;
using Sinuka.WebAPIs;
using Sinuka.Application.UseCases.Register;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    [TestFixture, Category("integration")]
    public class RegisterUseCaseTests
    {
        private CustomWebApplicationFactory<Startup> _factory;

        private RegisterInput input = new RegisterInput()
        {
            Username = new Bogus.DataSets.Internet().UserName(),
            Password = new Bogus.DataSets.Internet().Password(),
            Email = new Bogus.DataSets.Internet().Email(),
        };

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            this._factory = new CustomWebApplicationFactory<Startup>();
        }


        [Test]
        public async Task Register_ShouldReturn200_IfGivenUnusedEmailAndUsername()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var body = JsonContent.Create<RegisterInput>(input);

            // Act
            var response = await client.PostAsync("/user/register", body);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task Register_ShouldReturn400_IfGivenUsedEmail()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new RegisterInput()
            {
                Username = new Bogus.DataSets.Internet().UserName(),
                Password = new Bogus.DataSets.Internet().Password(),
                Email = input.Email,
            };
            var body = JsonContent.Create<RegisterInput>(badInput);

            // Act
            var response = await client.PostAsync("/user/register", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Register_ShouldReturn400_IfGivenUsedUsername()
        {
            // Arrange
            var client = this._factory.CreateClient();
            var badInput = new RegisterInput()
            {
                Username = input.Username,
                Password = new Bogus.DataSets.Internet().Password(),
                Email = new Bogus.DataSets.Internet().Email(),
            };
            var body = JsonContent.Create<RegisterInput>(badInput);

            // Act
            var response = await client.PostAsync("/user/register", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Regiser_ShouldReturn400_IfGivenEmptyInput()
        {
            var client = this._factory.CreateClient();
            var badInput = new RegisterInput();
            var body = JsonContent.Create<RegisterInput>(badInput);

            // Act
            var response = await client.PostAsync("/user/register", body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
