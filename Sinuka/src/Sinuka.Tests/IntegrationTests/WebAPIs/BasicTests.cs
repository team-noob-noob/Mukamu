using System.Threading.Tasks;
using NUnit.Framework;
using Sinuka.WebAPIs;

namespace Sinuka.Tests.IntegrationTests.WebAPIs
{
    [TestFixture, Category("integration")]
    public class BasicTests
    {
        private CustomWebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            this._factory = new CustomWebApplicationFactory<Startup>();
        }

        [Test]
        [TestCase("/")]
        [TestCase("/weatherforecast")]
        public async Task Get_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = this._factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
