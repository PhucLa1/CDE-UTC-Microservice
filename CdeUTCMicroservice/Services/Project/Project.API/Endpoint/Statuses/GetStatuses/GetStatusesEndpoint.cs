using Project.Application.Features.Statuses.GetStatuses;

namespace Project.API.Endpoint.Statuses.GetStatuses
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetStatusesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.STATUS)]
        public async Task<IActionResult> GetStatuses(int projectId)
        {
            return Ok(await mediator.Send(new GetStatusesRequest() { ProjectId = projectId }));
        }
    }
}
