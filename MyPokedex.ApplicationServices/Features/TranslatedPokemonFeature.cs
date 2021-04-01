namespace MyPokedex.ApplicationServices.Features
{
    using MyPokedex.Core;
    using MyPokedex.Core.DTOs;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class TranslatedPokemonFeature : ITranslatedPokemonFeature
    {
        private readonly ITranslationsService translationsService;
        private readonly IPokeService pokeService;
        private const string caveText = "cave";

        public TranslatedPokemonFeature(IPokeService pokeService, ITranslationsService translationsService)
        {
            this.pokeService = pokeService;
            this.translationsService = translationsService;
        }

        public async Task<PokemonInfoDto> GetTranslatedPokemonInfoAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("param: name cannot be null or empty");

            string translatedValue = string.Empty;
            var pokemonInfo = await this.pokeService.GetBasicPokemonInfoAsync(name).ConfigureAwait(false);

            if (pokemonInfo != null && pokemonInfo.FlavorTextEntries != null && !string.IsNullOrEmpty(pokemonInfo.FlavorTextEntries.First().FlavorText)) {
                if (isCaveOrLegendary(pokemonInfo)) {
                    var result = await this.translationsService.GetYodaTranslationAsync(pokemonInfo.FlavorTextEntries.First().FlavorText).ConfigureAwait(false);
                    translatedValue = result.Content.TranslatedText ?? result.Content.OriginalText;
                }
                else {
                    var result = await this.translationsService.GetShakespheareTranslationAsync(pokemonInfo.FlavorTextEntries.First().FlavorText).ConfigureAwait(false);
                    translatedValue = result.Content.TranslatedText ?? result.Content.OriginalText;
                }

                return new PokemonInfoDto {
                    Name = pokemonInfo.Name,
                    Description = translatedValue,
                    Habitat = pokemonInfo.Habitat?.Name,
                    IsLegendary = pokemonInfo.IsLegendary
                };
            }

            return default;
        }

        private bool isCaveOrLegendary(PokemonInfo pokemonInfo) => 
            (pokemonInfo.Habitat != null && pokemonInfo.Habitat.Name.Equals(caveText)) ||
            pokemonInfo.IsLegendary;
    }
}
