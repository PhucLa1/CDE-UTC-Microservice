using Auth.Application.Auth.ChangePassword;

namespace Auth.API.Controllers.Auth.ChangePassword
{
    [ApiController]
    [Route(NameRouter.AUTH_ROUTER)]
    public class ChangePasswordEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.CHANGE_PASSWORD)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePasswordRequest)
        {
            return Ok(await mediator.Send(changePasswordRequest));
        }
    }
}
