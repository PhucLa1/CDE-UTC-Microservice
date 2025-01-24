using Auth.Application.Auth.SignUp;

namespace Auth.API.Controllers.Auth.SignUp
{
    [Route(NameRouter.AUTH_ROUTER)]
    [ApiController]
    public class SignUpEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.SIGN_UP)]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            return Ok(await mediator.Send(signUpRequest));
        }
    }
}
