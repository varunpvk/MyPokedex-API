using Moq;
using MyPokedex.ApplicationServices.Features;
using MyPokedex.Core;
using MyPokedex.Core.DTOs;
using MyPokedexAPI.Controllers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyPokedex.Tests.API.Tests
{
    public class MyPokedexControllerTests
    {
        private readonly Mock<IBasicPokemonFeature> mockBasicPokemonFeature = new Mock<IBasicPokemonFeature>();
        private readonly Mock<ITranslatedPokemonFeature> mockTranslatedPokemonFeature = new Mock<ITranslatedPokemonFeature>();
        private MyPokedexController myPokedexController;

        [Fact]
        public async Task Given_Valid_Pokemon_Name_When_GetBasicInfo_IsCalled_Then_Returns_PokedexResponse()
        {
            //Arrange
            var pokemonInfo = new PokemonInfoDto {
                Name = "mewtwo",
                Habitat = "rare",
                Description = "Hello World",
                IsLegendary = true
            };
            mockBasicPokemonFeature.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act
            var response = await myPokedexController.GetBasicInfo("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.IsType<PokedexResponse>(response.Value);
            Assert.NotNull(response);
            mockBasicPokemonFeature.Verify(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Given_ArgumentNullException_From_BasicPokemonFeature_When_GetBasicInfo_IsCalled_Throws_Exception()
        {
            //Arrange
            mockBasicPokemonFeature.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).Throws<ArgumentNullException>();
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => myPokedexController.GetBasicInfo(string.Empty));
            await Assert.ThrowsAsync<ArgumentNullException>(() => myPokedexController.GetBasicInfo(null));
        }

        [Fact]
        public async Task Given_HttpResponseException_From_BasicPokemonFeature_When_GetBasicInfo_IsCalled_Throws_NotFound_Exception()
        {
            //Arrange
            mockBasicPokemonFeature.Setup(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => myPokedexController.GetBasicInfo("hello")).ConfigureAwait(false);
            mockBasicPokemonFeature.Verify(o => o.GetBasicPokemonInfoAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Given_Valid_Pokemon_Name_When_GetTranslatedInfo_IsCalled_Then_Returns_PokedexResponse()
        {
            //Arrange
            var pokemonInfo = new PokemonInfoDto {
                Name = "mewtwo",
                Habitat = "rare",
                Description = "Hello World",
                IsLegendary = true
            };
            mockTranslatedPokemonFeature.Setup(o => o.GetTranslatedPokemonInfoAsync(It.IsAny<string>())).ReturnsAsync(pokemonInfo);
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act
            var response = await myPokedexController.GetTranslatedInfo("mewtwo").ConfigureAwait(false);

            //Assert
            Assert.IsType<PokedexResponse>(response.Value);
            Assert.NotNull(response);
            mockTranslatedPokemonFeature.Verify(o => o.GetTranslatedPokemonInfoAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Given_ArgumentNullException_From_BasicPokemonFeature_When_GetTranslatedInfo_IsCalled_Throws_Exception()
        {
            //Arrange
            mockTranslatedPokemonFeature.Setup(o => o.GetTranslatedPokemonInfoAsync(It.IsAny<string>())).Throws<ArgumentNullException>();
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => myPokedexController.GetTranslatedInfo(string.Empty));
            await Assert.ThrowsAsync<ArgumentNullException>(() => myPokedexController.GetTranslatedInfo(null));
        }

        [Fact]
        public async Task Given_HttpResponseException_From_BasicPokemonFeature_When_GetTranslatedInfo_IsCalled_Throws_NotFound_Exception()
        {
            //Arrange
            mockTranslatedPokemonFeature.Setup(o => o.GetTranslatedPokemonInfoAsync(It.IsAny<string>())).Throws<HttpResponseException>();
            myPokedexController = new MyPokedexController(mockBasicPokemonFeature.Object, mockTranslatedPokemonFeature.Object);

            //Act & Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => myPokedexController.GetTranslatedInfo("hello")).ConfigureAwait(false);
            mockTranslatedPokemonFeature.Verify(o => o.GetTranslatedPokemonInfoAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
