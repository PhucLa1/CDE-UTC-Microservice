using Auth.Application.JobTitles.UpdateJobTitle;
using BuildingBlocks;

namespace Auth.API.Controllers.JobTitles.UpdateJobTitle
{
    [Route(NameRouter.JOB_TITLES_ROUTER)]
    [ApiController]
    public class UpdateJobTitleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateJobTitle([FromBody] UpdateJobTitleRequest updateJobTitleRequest)
        {
            return Ok(await mediator.Send(updateJobTitleRequest));
        }
    }
}
