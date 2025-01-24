using Auth.Application.JobTitles.CreateJobTitle;
using BuildingBlocks;

namespace Auth.API.Controllers.JobTitles.CreateJobTitle
{
    [Route(NameRouter.JOB_TITLES_ROUTER)]
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
