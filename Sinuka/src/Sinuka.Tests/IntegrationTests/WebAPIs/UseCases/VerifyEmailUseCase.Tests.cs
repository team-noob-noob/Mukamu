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
using Sinuka.Application.UseCases.VerifyEmail;
using Sinuka.Application.UseCases.Login;
using Sinuka.Core.Models;
using Sinuka.Infrastructure.Services;

namespace Sinuka.Tests.IntegrationTests.WebAPIs.UseCases
{
    public class VerifyEmailUseCaseTests
    {
        private CustomWebApplicationFactory<Startup> _factory;
        private string _emailVerifyString;

        [OneTimeSetUp]
        public void OneTimeSetup()
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

                    var user = db.Users.FirstOrDefault();
                    this._emailVerifyString = user.Email.VerificationString;
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
        public async Task VerifyEmail_ShouldReturn200_IfValidVerifyToken()
        {
            // Arrange
            var client = this._factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/emailaddress/verifyemail?VerificationString={this._emailVerifyString}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task VerifyEmail_ShouldReturn400_IfInvalidVerifyToken()
        {
            // Arrange
            var client = this._factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/emailaddress/verifyemail?VerificationString=THIS IS A TEST");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
