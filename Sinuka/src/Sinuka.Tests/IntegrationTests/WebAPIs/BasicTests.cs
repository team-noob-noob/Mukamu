using System.Threading.Tasks;
using NUnit.Framework;
using Sinuka.WebAPIs;

namespace Sinuka.Tests.IntegrationTests.WebAPIs
{
    [TestFixture, Category("integration")]
    public class BasicTests
    {
        private CustomWebApplicationFactory<Startup> _factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this._factory = new CustomWebApplicationFactory<Startup>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            this._factory.ClearDb();
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
