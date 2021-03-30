namespace MyPokedex.ApplicationServices.Features
{
    using MyPokedex.Core.DTOs;
    using System.Threading.Tasks;

    public interface IBasicPokemonFeature
    {
        Task<PokemonInfoDto> GetBasicPokemonInfoAsync(string name);
    }
}
