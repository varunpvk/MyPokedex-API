using Moq;
using MyPokedex.ApplicationServices.Features;
using MyPokedex.Core;
using MyPokedex.Infrastructure.FunTranslationsClient;
using MyPokedex.Infrastructure.PokeAPIClient;
using MyPokedex.Tests.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Tests.ApplicationServices.Tests
{
    public class TranslatedPokemonFeatureTests
    {
        private readonly Mock<IPokeService> mockPokeService = new Mock<IPokeService>();
        private readonly Mock<ITranslationsService> mockTranslationsService = new Mock<ITranslationsService>();
        private ITranslatedPokemonFeature translatedPokemonFeature;

        private readonly IDictionary<DataType, PokemonInfo> pokemonData;
        private readonly IDictionary<TranslationType, TranslatedShakespheareInfo> shakespeareTranslationsData;
        private readonly IDictionary<TranslationType, TranslatedYodaInfo> yodaTranslationsData;

        public TranslatedPokemonFeatureTests()
        {
            pokemonData = new Dictionary<DataType, PokemonInfo> {
                {DataType.Valid,  JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData)},
                {DataType.Invalid, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_Invalid) },
                {DataType.MissingProperty, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_MissingProperty) },
                {DataType.MissingDescription, JsonConvert.DeserializeObject<PokemonInfo>(PokeApiClientData.jsonData_MissingDescription) },
            };

            shakespeareTranslationsData = new Dictionary<TranslationType, TranslatedShakespheareInfo> {
                {TranslationType.Valid, JsonConvert.DeserializeObject<TranslatedShakespheareInfo>(FunTranslationsClientData.jsonData_Valid_Shakespeare) }
            };

            yodaTranslationsData = new Dictionary<TranslationType, TranslatedYodaInfo> {
                {TranslationType.Valid, JsonConvert.DeserializeObject<TranslatedYodaInfo>(FunTranslationsClientData.jsonData_Valid_Yoda) }
            };
        }

        [Fact]
        public async Task Given_Rare_But_NotLegendary_PokemonName_When_GetTranslatedPokemonInfoAsync_IsCalled_Returns_PokemonInfoDto_With_ShakespeareTranslation()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            pokemonInfo.IsLegendary = false;
            var shakespeareDescription = shakespeareTranslationsData[TranslationType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            mockTranslationsService.Setup(o => o.GetShakespheareTranslationAsync(It.IsAny<string>())).ReturnsAsync(shakespeareDescription);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(pokemonInfo.Habitat.Name, result.Habitat);
            Assert.NotEqual(pokemonInfo.FlavorTextEntries.First().FlavorText, result.Description);
            mockTranslationsService.Verify(o => o.GetShakespheareTranslationAsync(It.IsAny<string>()), Times.Once);
            mockTranslationsService.Verify(o => o.GetYodaTranslationAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Given_Rare_And_Legendary_PokemonName_When_GetTranslatedPokemonInfoAsync_IsCalled_Returns_PokemonInfoDto_With_YodaTranslation()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            var yodaDescription = yodaTranslationsData[TranslationType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            mockTranslationsService.Setup(o => o.GetYodaTranslationAsync(It.IsAny<string>())).ReturnsAsync(yodaDescription);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("rare", result.Habitat);
            Assert.NotEqual(pokemonInfo.FlavorTextEntries.First().FlavorText, result.Description);
            mockTranslationsService.Verify(o => o.GetShakespheareTranslationAsync(It.IsAny<string>()), Times.Never);
            mockTranslationsService.Verify(o => o.GetYodaTranslationAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Given_Cave_And_NotLegendary_PokemonName_When_GetTranslatedPokemonInfoAsync_IsCalled_Returns_PokemonInfoDto_With_YodaTranslation()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            pokemonInfo.Habitat.Name = "cave";
            pokemonInfo.IsLegendary = false;
            var yodaDescription = yodaTranslationsData[TranslationType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            mockTranslationsService.Setup(o => o.GetYodaTranslationAsync(It.IsAny<string>())).ReturnsAsync(yodaDescription);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            Assert.Equal("cave", result.Habitat);
            Assert.NotEqual(pokemonInfo.FlavorTextEntries.First().FlavorText, result.Description);
            mockTranslationsService.Verify(o => o.GetShakespheareTranslationAsync(It.IsAny<string>()), Times.Never);
            mockTranslationsService.Verify(o => o.GetYodaTranslationAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Given_EmptyOrNullPokemonName_When_GetTranslatedPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => translatedPokemonFeature.GetTranslatedPokemonInfoAsync(string.Empty));
            await Assert.ThrowsAsync<ArgumentNullException>(() => translatedPokemonFeature.GetTranslatedPokemonInfoAsync(default));
        }

        [Fact]
        public async Task Given_Invalid_PokemonInfo_When_GetTranslatedPokemonInfoAsync_IsCalled_Returns_Default_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Invalid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Missing_Description_In_PokemonInfo_When_GetTranslatedPokemonInfoAsync_Is_Called_Returns_Default_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.MissingDescription];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Missing_Property_In_PokemonInfo_When_GetTranslatedPokemonInfoAsync_Is_Called_Returns_Default_PokemonInfoDto()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.MissingProperty];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Default_PokemonInfo_When_GetTranslatedPokemonInfoAsync_IsCalled_Returns_DefaultValue()
        {
            //Arrange
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(default(PokemonInfo));
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act
            var result = await translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Given_Exception_For_GetBasicPokemonInfoAsync_When_GetTranslatedPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo")).ConfigureAwait(false);
        }

        [Fact]
        public async Task Given_Exception_For_GetYodaTranslationAsync_When_GetTranslatedPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            mockTranslationsService.Setup(o => o.GetYodaTranslationAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo")).ConfigureAwait(false);
        }

        [Fact]
        public async Task Given_Exception_For_GetShakespheareTranslationAsync_When_GetTranslatedPokemonInfoAsync_IsCalled_Throws_Exception()
        {
            //Arrange
            var pokemonInfo = pokemonData[DataType.Valid];
            pokemonInfo.IsLegendary = false;
            mockPokeService.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            mockTranslationsService.Setup(o => o.GetShakespheareTranslationAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            translatedPokemonFeature = new TranslatedPokemonFeature(mockPokeService.Object, mockTranslationsService.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => translatedPokemonFeature.GetTranslatedPokemonInfoAsync("mewtwo")).ConfigureAwait(false);
        }
    }
}
