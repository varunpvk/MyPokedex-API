namespace MyPokedex.Tests.ApplicationServices.Tests
{
    using Moq;
    using MyPokedex.ApplicationServices.Features;
    using MyPokedex.Core;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using MyPokedex.Tests.Data;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class BasicPokemonFeatureTests
    {
        private readonly Mock<IPokeService> mockPokeService = new Mock<IPokeService>();
        private IBasicPokemonFeature basicPokemonFeature;
        private readonly IDictionary<DataType, PokemonInfo> pokemonData;
        
        public BasicPokemonFeatureTests()
        {
            pokemonData = new Dictionary<DataType, PokemonInfo> {
                {DataType.Valid,  JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData)},
                {DataType.Invalid, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_Invalid) },
                {DataType.MissingProperty, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_MissingProperty) },
                {DataType.MissingDescription, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_MissingDescription) }
            };
        }

        [Fact]
        public async Task Given_Valid_PokemonName_When_GetBasicPokemonInfoAsync_IsCalled_Returns_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act
            var result = await basicPokemonFeature.GetBasicPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("mewtwo", result.Name);
            Assert.Equal("rare", result.Habitat);
            Assert.True(result.IsLegendary);
            Assert.Equal("It was created by\na scientist after\nyears of horrific\fgene splicing and\nDNA engineering\nexperiments.", result.Description);
        }

        [Fact]
        public async Task Given_EmptyOrNullPokemonName_When_GetBasicPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => basicPokemonFeature.GetBasicPokemonInfoAsync(string.Empty));
            await Assert.ThrowsAsync<ArgumentNullException>(() => basicPokemonFeature.GetBasicPokemonInfoAsync(default));
        }

        [Fact]
        public async Task Given_Invalid_PokemonInfo_When_GetBasicPokemonInfoAsync_IsCalled_Returns_Default_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Invalid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act
            var result = await basicPokemonFeature.GetBasicPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_MissingPropertyInPokemonInfo_When_GetBasicPokemonInfoAsync_IsCalled_Returns_Default_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.MissingProperty];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act
            var result = await basicPokemonFeature.GetBasicPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Default_PokemonInfo_When_GetBasicPokemonInfoAsync_IsCalled_Returns_DefaultValue()
        {
            //Arrange
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(default(PokemonInfo));
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act
            var result = await basicPokemonFeature.GetBasicPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Exception_When_GetBasicPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            basicPokemonFeature = new BasicPokemonFeature(mockPokeService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => basicPokemonFeature.GetBasicPokemonInfoAsync("mewtwo")).ConfigureAwait(false);
        }
    }
}
