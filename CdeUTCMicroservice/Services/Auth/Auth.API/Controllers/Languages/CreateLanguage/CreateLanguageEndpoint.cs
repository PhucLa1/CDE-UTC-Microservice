using Auth.Application.Languages.CreateLanguages;

namespace Auth.API.Controllers.Languages.CreateLanguage
{
    [Route("api/languages")]
    [ApiController]
    public class CreateLanguageEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateLanguage(CreateLanguageRequest createLanguageRequest)
        {
            return Ok(await mediator.Send(createLanguageRequest));
        }
    }
}
