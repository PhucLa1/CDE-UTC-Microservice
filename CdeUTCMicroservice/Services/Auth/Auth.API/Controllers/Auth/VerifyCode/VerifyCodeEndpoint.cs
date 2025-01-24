using Auth.Application.Auth.VerifyCode;

namespace Auth.API.Controllers.Auth.VerifyCode
{
    [ApiController]
    [Route(NameRouter.AUTH_ROUTER)]
    public class VerifyCodeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.VERIFY_CODE)]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest verifyCodeRequest)
        {
            return Ok(await mediator.Send(verifyCodeRequest));
        } 
    }
}
