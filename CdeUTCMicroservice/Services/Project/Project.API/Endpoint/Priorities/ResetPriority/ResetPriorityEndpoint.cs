using Project.Application.Features.Priorities.ResetPriority;

namespace Project.API.Endpoint.Priorities.ResetPriority
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class ResetPriorityEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{projectId}/" + NameRouter.PRIORITY)]
        public async Task<IActionResult> ResetPriority(int projectId)
        {
            return Ok(await mediator.Send(new ResetPriorityRequest() { ProjectId = projectId }));
        }
    }
}
