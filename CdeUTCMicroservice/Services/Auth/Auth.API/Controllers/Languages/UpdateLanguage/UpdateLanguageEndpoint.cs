using Auth.Application.Languages.UpdateLanguage;

namespace Auth.API.Controllers.Languages.UpdateLanguage
{
    [Route("api/languages")]
    [ApiController]
    public class UpdateLanguageEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateLanguage(UpdateLanguageRequest updateLanguageRequest)
        {
            return Ok(await mediator.Send(updateLanguageRequest));
        }
    }
}
