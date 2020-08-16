namespace CreditCheck.IntegrationTest.API
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;
    using CreditCheck;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.AspNetCore.Hosting;


    public class CustomerApiTest
    {
        private readonly HttpClient _client;

        public CustomerApiTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>());
            this._client = server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task CustomerGetAllTestAsync(string method)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/customers/");

            // Act
            var response = await this._client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("GET", 1)]
        public async Task CustomerGetTestAsync(string method, int? id = null)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/customers/{id}");

            // Act
            var response = await this._client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
