using Auth.Application.JobTitles.GetJobTitle;

namespace Auth.API.Controllers.JobTitles.GetJobTitle
{
    [Route("api/jobtitle")]
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
