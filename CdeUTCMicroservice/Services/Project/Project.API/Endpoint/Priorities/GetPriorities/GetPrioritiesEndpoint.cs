using Project.Application.Features.Priorities.GetPriorities;

namespace Project.API.Endpoint.Priorities.GetPriorities
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetPrioritiesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.PRIORITY)]
        public async Task<IActionResult> GetPriorities(int projectId)
        {
            return Ok(await mediator.Send(new GetPrioritiesRequest() { ProjectId = projectId }));
        }
    }
}
