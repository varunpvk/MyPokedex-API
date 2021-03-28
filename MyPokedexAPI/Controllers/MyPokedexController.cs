namespace MyPokedexAPI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyPokedex.Core;
    using MyPokedex.Infrastructure.PokeAPIClient;
    using MyPokedexAPI.Routes;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route(PokedexRoutes.basePath)]
    public class MyPokedexController : Controller
    {
        private readonly IPokeService pokeService;

        public MyPokedexController(IPokeService pokeService)
        {
            this.pokeService = pokeService;
        }

        [HttpGet]
        [Route(PokedexRoutes.basicInfo)]
        public async Task<ActionResult<PokedexResponse>> GetBasicInfo(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var response = await this.pokeService.GetBasicPokemonInfoAsync(name, new System.Threading.CancellationToken());
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokedexResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PokedexResponse>> GetTranslatedInfo(string name)
        {
            return Ok(new PokedexResponse());
        }
    }
}
