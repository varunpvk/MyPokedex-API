namespace MyPokedex.ApplicationServices.Features
{
    using MyPokedex.Core.DTOs;
    using System.Threading.Tasks;

    public interface ITranslatedPokemonFeature
    {
        Task<PokemonInfoDto> GetTranslatedPokemonInfoAsync(string name);
    }
}
