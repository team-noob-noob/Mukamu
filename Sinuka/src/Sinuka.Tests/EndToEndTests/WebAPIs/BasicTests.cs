using System.Threading.Tasks;
using NUnit.Framework;
using Sinuka.WebAPIs;

namespace Sinuka.Tests.EndToEndTests
{
    [TestFixture, Category("e2e")]
    public class WeatherForecastTests
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
        public async Task Test(string url)
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
