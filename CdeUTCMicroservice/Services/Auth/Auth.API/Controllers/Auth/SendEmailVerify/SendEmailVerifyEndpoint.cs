using Auth.Application.Auth.SendEmailVerify;

namespace Auth.API.Controllers.Auth.SendEmailVerify
{
    [ApiController]
    [Route(NameRouter.AUTH_ROUTER)]
    public class SendEmailVerifyEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.SEND_EMAIL_VERIFY)]
        public async Task<IActionResult> SendEmailVerifyCode([FromBody] SendEmailVerifyRequest sendEmailVerifyRequest)
        {
            return Ok(await mediator.Send(sendEmailVerifyRequest));
        }
    }
}
