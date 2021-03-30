namespace MyPokedex.Infrastructure.PokeAPIClient
{
    using MyPokedex.Core;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class PokeService : IPokeService
    {
        private readonly HttpClient httpClient;
        private const string urlString = "pokemon-species";

        public PokeService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PokemonInfo> GetBasicPokemonInfoAsync(string name)
        {
            var response = await this.httpClient.GetAsync($"{urlString}/{name}");

            if (response.IsSuccessStatusCode) {
                var pokemonInfo = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PokemonInfo>(pokemonInfo);
            }
            else {
                throw new HttpResponseException { Value = response.ReasonPhrase, Status = response.StatusCode };
            }
        }
    }
}
