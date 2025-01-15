using Auth.Application.JobTitles.DeletJobTitles;

namespace Auth.API.Controllers.JobTitles.DeleteJobTitles
{
    [Route("api/jobtitle")]
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
