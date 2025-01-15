using Auth.Application.Auth.SignUp;

namespace Auth.API.Controllers.Auth.SignUp
{
    [Route("api")]
    [ApiController]
    public class SignUpEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest signUpRequest)
        {
            return Ok(await mediator.Send(signUpRequest));
        }
    }
}
