using Auth.Application.JobTitles.DeletJobTitles;
using BuildingBlocks;

namespace Auth.API.Controllers.JobTitles.DeleteJobTitles
{
    [Route(NameRouter.JOB_TITLES_ROUTER)]
    [ApiController]
    public class DeleteJobTitleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteJobTitles([FromBody] DeleteJobTitleRequest deleteJobTitleRequest)
        {
            return Ok(await mediator.Send(deleteJobTitleRequest));
        }
    }
}
