using Auth.Application.Languages.GetLanguages;

namespace Auth.API.Controllers.Languages.GetLanguages
{
    [Route("api/languages")]
    [ApiController]
    public class GetLanguageEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> GetPaginationLanguages([FromQuery] GetPaginationLanguagesRequest getPaginationLanguagesRequest)
        {
            return Ok(await mediator.Send(getPaginationLanguagesRequest));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetLanguages()
        {
            return Ok(await mediator.Send(new GetLanguagesRequest()));
        }
    }
}
