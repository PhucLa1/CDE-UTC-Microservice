namespace Auth.API.Controllers.Auth.Login
{
    [Route("api")]
    [ApiController]
    public class LoginEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            return Ok(await mediator.Send(loginRequest));
        }
    }
}
