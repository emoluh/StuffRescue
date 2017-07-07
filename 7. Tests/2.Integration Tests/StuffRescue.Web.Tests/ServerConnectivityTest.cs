using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StuffRescue.Web.Tests
{
    public class ServerConnectivityTest : IClassFixture<TestFixture<Startup>>
    {
        private readonly HttpClient _client;
        public ServerConnectivityTest(TestFixture<Startup> fixture)
        {
            // Arrange
            _client = fixture.Client;
        }
        [Fact]
        public async Task ReturnPong()
        {
            // Act
            var response = await _client.GetAsync("/api/Ping");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal("Pong",
                responseString);
        }
    }
}
