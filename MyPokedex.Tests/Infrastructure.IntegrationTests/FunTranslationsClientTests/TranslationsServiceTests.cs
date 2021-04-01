namespace MyPokedex.Tests.Infrastructure.IntegrationTests.FunTranslationsClientTests
{
    using Microsoft.Extensions.Configuration;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Tests.Helper;
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    /// <summary>
    /// There is a limit of 5 requests per hour for fun translations api, so the integration tests are bound to fail if the limit has crossed. To mitigate the problem, 
    /// we can implement a fallback value so as to not interrupt the execution in TranslationsService.
    /// </summary>
    public class TranslationsServiceTests
    {
        public class ShakespeareTests
        {
            #region private members
            private readonly IConfiguration config = ConfigBuilder.InitConfiguration();
            #endregion

            [Fact]
            public async Task Given_ValidRequest_When_GetShakespheareTranslationAsync_IsCalled_Returns_TranslatedShakespheareInfo()
            {
                //Arrange
                var httpClient = new HttpClient() { BaseAddress = new Uri(config["TranslationsService:BaseUri"]) };
                var translationsService = new TranslationsService(httpClient);
                var inputText = "It can freely recombine its own cellular structure totransform into other life-forms.";

                //Act
                var result = await translationsService.GetShakespheareTranslationAsync(inputText).ConfigureAwait(false);

                //Assert
                Assert.NotNull(result);
            }

            [Fact]
            public async Task Given_InValidQueryParameterValue_When_GetShakespheareTranslationAsync_IsCalled_Returns_TranslatedShakespheareInfo()
            {
                //Arrange
                var httpClient = new HttpClient() { BaseAddress = new Uri(config["TranslationsService:BaseUri"]) };
                var translationsService = new TranslationsService(httpClient);
                var inputText = "#$&$%#";

                //Act
                var result = await translationsService.GetShakespheareTranslationAsync(inputText).ConfigureAwait(false);

                //Assert
                Assert.NotNull(result);
            }
        }

        public class YodaTests
        {
            #region private members
            private readonly IConfiguration config = ConfigBuilder.InitConfiguration();
            #endregion

            [Fact]
            public async Task Given_ValidRequest_When_GetYodaTranslationAsync_IsCalled_Returns_TranslatedYodaInfo()
            {
                //Arrange
                var httpClient = new HttpClient() { BaseAddress = new Uri(config["TranslationsService:BaseUri"]) };
                var translationsService = new TranslationsService(httpClient);
                var inputText = "It can freely recombine its own cellular structure totransform into other life-forms.";

                //Act
                var result = await translationsService.GetYodaTranslationAsync(inputText).ConfigureAwait(false);

                //Assert
                Assert.NotNull(result);
            }

            [Fact]
            public async Task Given_InValidQueryParameterValue_When_GetYodaTranslationAsync_IsCalled_Returns_TranslatedYodaInfo()
            {
                //Arrange
                var httpClient = new HttpClient() { BaseAddress = new Uri(config["TranslationsService:BaseUri"]) };
                var translationsService = new TranslationsService(httpClient);
                var inputText = "#$&$%#";

                //Act
                var result = await translationsService.GetYodaTranslationAsync(inputText).ConfigureAwait(false);

                //Assert
                Assert.NotNull(result);
            }
        }
    }
}
