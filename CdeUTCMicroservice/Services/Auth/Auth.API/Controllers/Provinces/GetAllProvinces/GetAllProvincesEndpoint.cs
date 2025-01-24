using Auth.Application.Provinces.GetAllProvinces;
using BuildingBlocks;

namespace Auth.API.Controllers.Provinces.GetAllProvinces
{
    [ApiController]
    [Route(NameRouter.PROVINCES_ROUTER)]
    public class GetAllProvincesEndpoint(IMediator mediator): ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllProvinces([FromQuery] GetAllProvincesRequest getAllProvincesRequest)
        {
            return Ok(await mediator.Send(getAllProvincesRequest));
        }
    }
}
