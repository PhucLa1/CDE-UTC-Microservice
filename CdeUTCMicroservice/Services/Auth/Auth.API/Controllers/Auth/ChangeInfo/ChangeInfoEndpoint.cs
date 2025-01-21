using Auth.Application.Auth.ChangeInfo;

namespace Auth.API.Controllers.Auth.ChangeInfo
{
    [Route("api")]
    [ApiController]
    public class ChangeInfoEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> ChangeInfoUser([FromBody] ChangeInfoRequest changeInfoRequest)
        {
            return Ok(await mediator.Send(changeInfoRequest));
        }
    }
}
