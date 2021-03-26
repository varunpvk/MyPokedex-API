namespace MyPokedexAPI.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using MyPokedex.Core;
    using MyPokedexAPI.Routes;
    using System.Threading.Tasks;

    [ApiController]
    [Route(PokedexRoutes.basePath)]
    public class MyPokedexController : Controller
    {
       
        [HttpGet]
        [Route(PokedexRoutes.basicInfo)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokedexResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PokedexResponse>> GetBasicInfo(string name)
        {
            return Ok(new PokedexResponse());
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
