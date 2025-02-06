using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WrtWebSocketServer.Constants;

namespace WrtWebSocketServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaresController : ControllerBase
    {
        [HttpGet("getGares")]
        public IActionResult GetGares([FromQuery] string TrainRoutes)
        {

            if(string.IsNullOrEmpty(TrainRoutes))
            {
                return BadRequest("TrainRoutes is required");
            }
            List<string> gares;
            switch (TrainRoutes.ToLower())
            {
                case "algerthenia":
                    gares = new List<string>(Routes.AlgerThenia.AllGares);
                    break;
                case "algerelaffroun":
                    gares = new List<string>(Routes.AlgerElAffroun.AllGares);
                    break;
                case "algerouedaissi":
                    gares = new List<string>(Routes.AlgerOuedAissi.AllGares);
                    break;
                case "aghaaeroport":
                    gares = new List<string>(Routes.AghaAeroport.AllGares);
                    break;
                default:
                    return NotFound(" not found");
            }

            return Ok(gares);
        }

    }
}
