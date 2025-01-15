using Auth.Application.JobTitles.UpdateJobTitle;
using BuildingBlocks.Extensions;

namespace Auth.API.Controllers.JobTitles.UpdateJobTitle
{
    [Route("api/jobtitle")]
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
