namespace MyPokedex.ApplicationServices.Features
{
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
                throw new ArgumentNullException("param: name cannot be null or empty");

            var pokemonInfo = await this.pokeService.GetBasicPokemonInfoAsync(name).ConfigureAwait(false);

            if (pokemonInfo != null) {

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
