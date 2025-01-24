using Auth.Application.JobTitles.GetJobTitle;
using BuildingBlocks;

namespace Auth.API.Controllers.JobTitles.GetJobTitle
{
    [Route(NameRouter.JOB_TITLES_ROUTER)]
    [ApiController]
    public class GetJobTitleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> GetPaginationJobTitle([FromQuery] GetPaginationJobTitleRequest getPaginationJobTitleRequest)
        {
            return Ok(await mediator.Send(getPaginationJobTitleRequest));
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJobTitle()
        {
            return Ok(await mediator.Send(new GetJobTitleRequest()));
        }
    }
}
