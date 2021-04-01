namespace MyPokedex.ApplicationServices.Features
{
    using Microsoft.AspNetCore.Http;
    using MyPokedex.ApplicationServices.Helper;
    using MyPokedex.Core;
    using MyPokedex.Core.DTOs;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using System;
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;
    using System.Web;

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
                throw new ArgumentNullException(nameof(name));

            string translatedValue = string.Empty;
            var pokemonInfo = await this.pokeService.GetBasicPokemonInfoAsync(name).ConfigureAwait(false);

            if (pokemonInfo != null && pokemonInfo.FlavorTextEntries != null && !string.IsNullOrEmpty(pokemonInfo.FlavorTextEntries.First().FlavorText)) {

                var description = pokemonInfo.FlavorTextEntries.First().FlavorText.RemoveEscapeSequenceIfAny();

                if (isCaveOrLegendary(pokemonInfo)) {

                    var result = await this.translationsService.GetYodaTranslationAsync(description).ConfigureAwait(false);
                    translatedValue = result.Content.TranslatedText ?? result.Content.OriginalText;
                }
                else {
                    var result = await this.translationsService.GetShakespheareTranslationAsync(description).ConfigureAwait(false);
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
