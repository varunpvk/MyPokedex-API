namespace MyPokedex.ApplicationServices.Features
{
    using MyPokedex.Core.DTOs;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using System;
    using System.Threading.Tasks;

    public class TranslatedPokemonFeature : ITranslatedPokemonFeature
    {
        private readonly ITranslationsService translationsService;
        private readonly IBasicPokemonFeature basicPokemonFeature;
        private const string caveText = "cave";

        public TranslatedPokemonFeature(IBasicPokemonFeature basicPokemonFeature, ITranslationsService translationsService)
        {
            this.translationsService = translationsService;
            this.basicPokemonFeature = basicPokemonFeature;
        }

        public async Task<PokemonInfoDto> GetTranslatedPokemonInfoAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("param: name cannot be null or empty");

            string translatedValue = string.Empty;
            var pokemonInfoDto = await this.basicPokemonFeature.GetBasicPokemonInfoAsync(name).ConfigureAwait(false);

            if (isCaveOrLegendary(pokemonInfoDto)) {
                var result = await this.translationsService.GetYodaTranslationAsync(pokemonInfoDto.Description).ConfigureAwait(false);
                translatedValue = result.Content.TranslatedText ?? result.Content.OriginalText;
            }
            else {
                var result = await this.translationsService.GetShakespheareTranslationAsync(pokemonInfoDto.Description).ConfigureAwait(false);
                translatedValue = result.Content.TranslatedText ?? result.Content.OriginalText;
            }

            pokemonInfoDto.Description = translatedValue;
            return pokemonInfoDto;
        }

        private bool isCaveOrLegendary(PokemonInfoDto pokemonInfo) => pokemonInfo.Habitat.Equals(caveText) || pokemonInfo.IsLegendary;
    }
}
