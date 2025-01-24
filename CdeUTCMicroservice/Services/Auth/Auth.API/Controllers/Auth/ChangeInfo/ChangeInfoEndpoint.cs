using Auth.Application.Auth.ChangeInfo;


namespace Auth.API.Controllers.Auth.ChangeInfo
{
    [Route(NameRouter.AUTH_ROUTER)]
    [ApiController]
    public class ChangeInfoEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.CHANGE_INFO)]
        public async Task<IActionResult> ChangeInfoUser([FromForm] ChangeInfoRequest changeInfoRequest)
        {
            return Ok(await mediator.Send(changeInfoRequest));
        }
    }
}
