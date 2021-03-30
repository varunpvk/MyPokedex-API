namespace MyPokedex.Infrastructure.PokeAPIClient
{
    using MyPokedex.Core;
    using System.Threading.Tasks;

    public interface IPokeService
    {
        Task<PokemonInfo> GetBasicPokemonInfoAsync(string name);
    }
}
