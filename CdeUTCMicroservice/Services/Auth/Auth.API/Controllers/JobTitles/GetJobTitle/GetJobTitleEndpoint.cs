using Auth.Application.JobTitles.GetJobTitle;

namespace Auth.API.Controllers.JobTitles.GetJobTitle
{
    [Route("api/jobtitle")]
    [ApiController]
    public class GetJobTitleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJobTitle([AsParameters] GetJobTitleRequest getJobTitleRequest)
        {
            return Ok(await mediator.Send(getJobTitleRequest));
        }
    }
}
