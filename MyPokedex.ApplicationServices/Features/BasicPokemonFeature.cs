namespace MyPokedex.ApplicationServices.Features
{
    using MyPokedex.ApplicationServices.Helper;
    using MyPokedex.Core.DTOs;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class BasicPokemonFeature : IBasicPokemonFeature
    {
        private readonly IPokeService pokeService;

        public BasicPokemonFeature(IPokeService pokeService)
        {
            this.pokeService = pokeService;
        }

        public async Task<PokemonInfoDto> GetBasicPokemonInfoAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var pokemonInfo = await this.pokeService.GetBasicPokemonInfoAsync(name).ConfigureAwait(false);

            if (pokemonInfo != null && !pokemonInfo.IsDefault()) {

                return new PokemonInfoDto {
                    Name = pokemonInfo.Name,
                    Description = pokemonInfo.FlavorTextEntries.Any() ? pokemonInfo.FlavorTextEntries.First().FlavorText : string.Empty,
                    Habitat = pokemonInfo.Habitat.Name,
                    IsLegendary = pokemonInfo.IsLegendary
                };
            }

            return default;
        }
    }
}
