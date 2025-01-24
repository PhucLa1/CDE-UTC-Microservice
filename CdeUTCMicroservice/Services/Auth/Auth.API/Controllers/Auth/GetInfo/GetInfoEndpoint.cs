using Auth.Application.Auth.GetInfo;

namespace Auth.API.Controllers.Auth.GetInfo
{
    [ApiController]
    [Route(NameRouter.AUTH_ROUTER)]
    public class GetInfoEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route(NameRouter.GET_INFO)]
        public async Task<IActionResult> GetInfo()
        {
            return Ok(await mediator.Send(new GetInfoRequest()));
        }
    }
}
