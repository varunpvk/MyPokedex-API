namespace MyPokedex.Infrastructure.FunTranslationsClient
{
    using Microsoft.AspNetCore.WebUtilities;
    using MyPokedex.Core;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Net.Http;
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

        public async Task<TranslatedShakespheareInfo> GetShakespheareTranslationAsync(string inputText)
        {
            var query = new Dictionary<string, string>() {
                ["text"] = inputText
            };

            var response = await this.httpClient.GetAsync(QueryHelpers.AddQueryString(GetBaseUri(shakespeareApiUrl), query)).ConfigureAwait(false);


            if (response.IsSuccessStatusCode) {
                var shakespeareInfoResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TranslatedShakespheareInfo>(shakespeareInfoResponse);
            }
            else {
                return new TranslatedShakespheareInfo {
                    Content = new Contents {
                        OriginalText = inputText,
                    }
                };
            }
        }

        public async Task<TranslatedYodaInfo> GetYodaTranslationAsync(string inputText)
        {
            var query = new Dictionary<string, string>() {
                ["text"] = inputText
            };

            var response = await this.httpClient.GetAsync(QueryHelpers.AddQueryString(GetBaseUri(yodaApiUrl), query)).ConfigureAwait(false);

            if (response.IsSuccessStatusCode) {
                var shakespeareInfoResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<TranslatedYodaInfo>(shakespeareInfoResponse);
            }
            else {
                return new TranslatedYodaInfo {
                    Content = new Contents {
                        OriginalText = inputText,
                    }
                };
            }
        }

        private string GetBaseUri(string apiUrl) => $"{this.httpClient.BaseAddress.AbsoluteUri}/{apiUrl}";
    }
}
