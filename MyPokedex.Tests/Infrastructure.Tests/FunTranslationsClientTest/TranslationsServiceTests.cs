namespace MyPokedex.Tests.Infrastructure.Tests.FunTranslationsClientTest
{
    using AutoFixture;
    using Moq;
    using Moq.Protected;
    using MyPokedex.Core;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Tests.Data;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class TranslationsServiceTests
    {
        public class ShakespeareTests
        {
            #region private members
            private readonly Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
            private readonly Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            private readonly Fixture fixture = new Fixture();
            #endregion

            [Fact]
            public async Task Given_ValidInput_When_GetShakespheareTranslationAsync_IsCalled_Returns_ValidResponse()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_Valid_Shakespeare) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetShakespheareTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Equal("It can freely recombine its own cellular structure totransform into other life-forms.", response.Content.OriginalText);
                Assert.Equal("'t can freely recombine its own cellular structure totransform into other life-forms.", response.Content.TranslatedText);
            }

            [Fact]
            public async Task Given_NotFoundResponse_When_GetShakespheareTranslationAsync_IsCalled_Throws_Exception()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("") };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act & Assert
                var translationsService = new TranslationsService(client);
                await Assert.ThrowsAsync<HttpResponseException>(() => translationsService.GetShakespheareTranslationAsync("abcd"));
            }

            [Fact]
            public async Task Given_IncorrectResponse_When_GetShakespheareTranslationAsync_IsCalled_Returns_DefaultValues()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_Invalid) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetShakespheareTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Null(response.Content);
            }

            [Fact]
            public async Task Given_ValidInput_WithMissingProperty_When_GetShakespheareTranslationAsync_IsCalled_Returns_ValidResponse()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_MissingProperty) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetShakespheareTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Equal("It can freely recombine its own cellular structure totransform into other life-forms.", response.Content.OriginalText);
                Assert.Null(response.Content.TranslatedText);
            }
        }

        public class YodaTests
        {
            #region private members
            private readonly Mock<IHttpClientFactory> mockHttpClientFactory = new Mock<IHttpClientFactory>();
            private readonly Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            private readonly Fixture fixture = new Fixture();
            #endregion

            [Fact]
            public async Task Given_ValidInput_When_GetYodaTranslationAsync_IsCalled_Returns_ValidResponse()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_Valid_Yoda) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetYodaTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Equal("It can freely recombine its own cellular structure totransform into other life-forms.", response.Content.OriginalText);
                Assert.Equal("freely recombining on its own cellular structure, \nit can transform into other life-forms.", response.Content.TranslatedText);
            }

            [Fact]
            public async Task Given_NotFoundResponse_When_GetYodaTranslationAsync_IsCalled_Throws_Exception()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.NotFound) { Content = new StringContent("") };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act & Assert
                var translationsService = new TranslationsService(client);
                await Assert.ThrowsAsync<HttpResponseException>(() => translationsService.GetYodaTranslationAsync("abcd"));
            }

            [Fact]
            public async Task Given_IncorrectResponse_When_GetYodaTranslationAsync_IsCalled_Returns_DefaultValues()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_Invalid) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetYodaTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Null(response.Content);
            }

            [Fact]
            public async Task Given_ValidInput_WithMissingProperty_When_GetYodaTranslationAsync_IsCalled_Returns_ValidResponse()
            {
                //Arrange
                var mockResponse = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(FunTranslationsClientData.jsonData_MissingProperty) };
                mockResponse.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(mockResponse);
                var client = new HttpClient(mockHttpMessageHandler.Object);
                client.BaseAddress = fixture.Create<Uri>();

                mockHttpClientFactory.Setup(o => o.CreateClient(It.IsAny<string>())).Returns(client);

                //Act
                var translationsService = new TranslationsService(client);
                var response = await translationsService.GetYodaTranslationAsync("abcd");

                //Assert
                Assert.NotNull(response);
                Assert.Equal("It can freely recombine its own cellular structure totransform into other life-forms.", response.Content.OriginalText);
                Assert.Null(response.Content.TranslatedText);
            }
        }
    }
}
