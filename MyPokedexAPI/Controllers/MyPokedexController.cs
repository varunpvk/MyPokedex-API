namespace MyPokedexAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyPokedex.Core;
    using MyPokedex.Infrastructure.FunTranslationsClient;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using MyPokedexAPI.Routes;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route(PokedexRoutes.basePath)]
    public class MyPokedexController : Controller
    {
        private readonly IPokeService pokeService;
        private readonly ITranslationsService translationsService;

        public MyPokedexController(IPokeService pokeService, ITranslationsService translationsService)
        {
            this.pokeService = pokeService;
            this.translationsService = translationsService;
        }

        [HttpGet]
        [Route(PokedexRoutes.basicInfo)]
        public async Task<ActionResult<PokedexResponse>> GetBasicInfo(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var response = await this.pokeService.GetBasicPokemonInfoAsync(name);
            var PokedexResponse = new PokedexResponse {
                Name = response.Name,
                Description = response.FlavorTextEntries.First(o => o.Language.Name == "en").FlavorText.Replace("\n", "").Replace("\f", ""),
                Habitat = response.Habitat.Name,
                IsLegendary = response.IsLegendary
            };
            return Ok(PokedexResponse);
        }

        [HttpGet]
        [Route(PokedexRoutes.translatedInfo)]
        public async Task<ActionResult<PokedexResponse>> GetTranslatedInfo(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var response = await this.translationsService.GetShakespheareTranslationAsync(name);

            return Ok(new PokedexResponse());
        }
    }
}
