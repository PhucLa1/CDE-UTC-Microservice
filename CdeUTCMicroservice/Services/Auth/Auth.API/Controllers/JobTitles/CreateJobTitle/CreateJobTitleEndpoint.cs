using Auth.Application.JobTitles.CreateJobTitle;

namespace Auth.API.Controllers.JobTitles.CreateJobTitle
{
    [Route("api/jobtitle")]
    [ApiController]
    public class CreateJobTitleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateJobTile([FromBody] CreateJobTitleRequest createJobTitleRequest)
        {
            return Ok(await mediator.Send(createJobTitleRequest));
        }
    }
}
