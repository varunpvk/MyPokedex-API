namespace MyPokedexAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MyPokedex.ApplicationServices.Features;
    using MyPokedex.Core;
    using MyPokedexAPI.Routes;
    using System.Threading.Tasks;

    [ApiController]
    [Route(PokedexRoutes.basePath)]
    public class MyPokedexController : Controller
    {
        private readonly IBasicPokemonFeature basicPokemonFeature;
        private readonly ITranslatedPokemonFeature translatedPokemonFeature;

        public MyPokedexController(IBasicPokemonFeature basicPokemonFeature, ITranslatedPokemonFeature translatedPokemonFeature)
        {
            this.basicPokemonFeature = basicPokemonFeature;
            this.translatedPokemonFeature = translatedPokemonFeature;
        }

        [HttpGet]
        [Route(PokedexRoutes.basicInfo)]
        public async Task<ActionResult<PokedexResponse>> GetBasicInfo(string name)
        {
            var response = await this.basicPokemonFeature.GetBasicPokemonInfoAsync(name);
            if (response != null) {
                return new PokedexResponse {
                    Name = response.Name,
                    Description = response.Description,
                    Habitat = response.Habitat,
                    IsLegendary = response.IsLegendary
                };
            }

            return BadRequest();
        }

        [HttpGet]
        [Route(PokedexRoutes.translatedInfo)]
        public async Task<ActionResult<PokedexResponse>> GetTranslatedInfo(string name)
        {
            var response = await this.translatedPokemonFeature.GetTranslatedPokemonInfoAsync(name);
            if (response != null) {
                return new PokedexResponse {
                    Name = response.Name,
                    Description = response.Description,
                    Habitat = response.Habitat,
                    IsLegendary = response.IsLegendary
                };
            }

            return BadRequest();
        }

        [HttpGet]
        [Route(PokedexRoutes.testEndpoint)]
        public async Task<bool> IsEndpointTested()
        {
            return true;
        }
    }
}
