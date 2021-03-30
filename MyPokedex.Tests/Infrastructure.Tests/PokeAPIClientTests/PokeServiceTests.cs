namespace MyPokedex.Tests.Infrastructure.Tests.PokeAPIClientTests
{
    using System.Threading.Tasks;
    using Xunit;
    using Moq;
    using System.Net.Http;
    using AutoFixture;
    using System;
    using System.Threading;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using Moq.Protected;
    using System.Net;
    using System.Net.Http.Headers;
    using MyPokedex.Core;
    using System.Linq;
    using MyPokedex.Tests.Data;

    public class PokeServiceTests
    {
        #region private members
        private readonly Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
        private readonly Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        private readonly Fixture fixture = new Fixture();
        #endregion

        private HttpClient SetupClient(HttpStatusCode statusCode, StringContent inputData)
        {
            var mockResponse = new HttpResponseMessage(statusCode) { Content = inputData };
            mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(mockResponse);
            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = fixture.Create<Uri>();

            mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

            return client;
        }

        [Fact]
        private async Task Given_ValidInput_When_GetAsync_HttpRequest_Returns_ValidResponse()
        {
            //Arrange
            var client = SetupClient(HttpStatusCode.OK, new StringContent(PokeApiClientData.jsonData));

            //Act
            var pokeService = new PokeService(client);
            var response = await pokeService.GetBasicPokemonInfoAsync("mewtwo");

            //Assert
            Assert.NotNull(response);
            Assert.Equal("mewtwo", response.Name);
            Assert.True(response.IsLegendary);
            Assert.Equal("It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.", response.FlavorTextEntries.First().FlavorText);
            Assert.Equal("rare", response.Habitat.Name);
        }

        [Fact]
        private async Task Given_NotfoundResponse_When_GetAsync_HttpRequest_Throws_Exception()
        {
            //Arrange
            var client = SetupClient(HttpStatusCode.NotFound, new StringContent(""));

            //Act & Assert
            var pokeService = new PokeService(client);
            await Assert.ThrowsAsync<HttpResponseException>(() => pokeService.GetBasicPokemonInfoAsync("abcd"));
        }

        [Fact]
        private async Task Given_IncorrectResponse_when_GetAsync_HttpRequest_returns_defaultValues()
        {
            //Arrange
            var client = SetupClient(HttpStatusCode.OK, new StringContent(PokeApiClientData.jsonData_Invalid));

            //Act
            var pokeService = new PokeService(client);
            var response = await pokeService.GetBasicPokemonInfoAsync("mewtwo");

            //Assert
            Assert.NotNull(response);
            Assert.Null(response.Name);
            Assert.False(response.IsLegendary);
            Assert.Null(response.FlavorTextEntries);
            Assert.Null(response.Habitat);
        }

        [Fact]
        private async Task Given_ValidInput_WithMissingProperty_When_GetAsync_HttpRequest_Returns_ValidResponse()
        {
            //Arrange
            var client = SetupClient(HttpStatusCode.OK, new StringContent(PokeApiClientData.jsonData_MissingProperty));

            //Act
            var pokeService = new PokeService(client);
            var response = await pokeService.GetBasicPokemonInfoAsync("mewtwo");

            //Assert
            Assert.NotNull(response);
            Assert.Equal("mewtwo", response.Name);
            Assert.True(response.IsLegendary);
            Assert.Null(response.FlavorTextEntries.First().FlavorText);
            Assert.Equal("rare", response.Habitat.Name);
        }
    }
}
