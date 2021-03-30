namespace MyPokedex.Tests.Infrastructure.IntegrationTests.PokeAPIClientTests
{
    using Microsoft.Extensions.Configuration;
    using MyPokedex.Core;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using MyPokedex.Tests.Helper;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class PokeServiceTests
    {
        #region private members
        private readonly IConfiguration config = ConfigBuilder.InitConfiguration();
        #endregion

        [Fact]
        public async Task Given_ValidRequest_When_GetBasicPokemonInfoAsync_IsCalled_Then_Returns_PokemonInfo()
        {
            //Arrange
            var httpClient = new HttpClient() { BaseAddress = new Uri(config["PokeService:BaseUri"]) };
            string queryInput = "mewtwo";

            //Act
            var pokeService = new PokeService(httpClient);
            var result = await pokeService.GetBasicPokemonInfoAsync(queryInput).ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Given_InvalidQueryParameterValue_When_GetBasicPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var httpClient = new HttpClient() { BaseAddress = new Uri(config["PokeService:BaseUri"]) };
            string queryInput = "hello";

            //Act & Assert
            var pokeService = new PokeService(httpClient);
            await Assert.ThrowsAsync<HttpResponseException>(() => pokeService.GetBasicPokemonInfoAsync(queryInput));
        }

        [Fact]
        public async Task Given_InvalidRequest_When_GetBasicPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var httpClient = new HttpClient() { BaseAddress = new Uri(config["PokeService:BaseUri"].Replace("v2","")) };
            string queryInput = "hello";

            //Act & Assert
            var pokeService = new PokeService(httpClient);
            await Assert.ThrowsAsync<HttpResponseException>(() => pokeService.GetBasicPokemonInfoAsync(queryInput));
        }
    }
}
