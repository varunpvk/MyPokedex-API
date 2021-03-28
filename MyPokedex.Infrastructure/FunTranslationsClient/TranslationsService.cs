namespace MyPokedex.Infrastructure.FunTranslationsClient
{
    using Microsoft.AspNetCore.WebUtilities;
    using MyPokedex.Core;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class TranslationsService : ITranslationsService
    {
        private readonly HttpClient httpClient;
        private const string shakespeareApiUrl = "shakespeare";
        private const string yodaApiUrl = "yoda";

        public TranslationsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<TranslatedShakespheareInfo> GetShakespheareTranslationAsync(string inputText, CancellationToken cancellationToken)
        {
            var query = new Dictionary<string, string>() {
                ["text"] = inputText
            };

            var response = await this.httpClient.GetAsync(QueryHelpers.AddQueryString(GetBaseUri(shakespeareApiUrl), query), cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                var shakespeareInfoResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TranslatedShakespheareInfo>(shakespeareInfoResponse);
            }
            else {
                throw new HttpResponseException { Value = response.ReasonPhrase, Status = response.StatusCode };
            }
        }

        public async Task<TranslatedYodaInfo> GetYodaTranslationAsync(string inputText, CancellationToken cancellationToken)
        {
            var query = new Dictionary<string, string>() {
                ["text"] = inputText
            };

            var response = await this.httpClient.GetAsync(QueryHelpers.AddQueryString(GetBaseUri(yodaApiUrl), query), cancellationToken).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                var shakespeareInfoResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TranslatedYodaInfo>(shakespeareInfoResponse);
            }
            else {
                throw new HttpResponseException { Value = response.ReasonPhrase, Status = response.StatusCode };
            }
        }

        private string GetBaseUri(string apiUrl) => $"{this.httpClient.BaseAddress.AbsoluteUri}/{apiUrl}";
    }
}
