using Auth.Application.Languages.DeleteLanguages;

namespace Auth.API.Controllers.Languages.DeleteLanguages
{
    [Route("api/languages")]
    [ApiController]
    public class DeleteLanguagesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteLanguages([FromBody] DeleteLanguagesRequest deleteLanguagesRequest)
        {
            return Ok(await mediator.Send(deleteLanguagesRequest));
        }
    }
}
