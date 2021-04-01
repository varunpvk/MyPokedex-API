namespace MyPokedex.ApplicationServices.Helper
{
    using MyPokedex.Core;

    public static class PokemonInfoExtension
    {
        public static bool IsDefault(this PokemonInfo pokemonInfo)
        {
            return string.IsNullOrEmpty(pokemonInfo.Name) ||
                pokemonInfo.Habitat == null ||
                pokemonInfo.FlavorTextEntries == null;
        }
    }
}
