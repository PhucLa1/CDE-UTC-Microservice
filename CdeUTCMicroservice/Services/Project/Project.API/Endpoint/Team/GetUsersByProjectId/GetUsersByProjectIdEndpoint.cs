using Project.Application.Features.Team.GetUsersByProjectId;

namespace Project.API.Endpoint.Team.GetUsersByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetUsersByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.TEAM_ROUTER)]
        public async Task<IActionResult> GetUsersByProjectId(int projectId)
        {
            return Ok(await mediator.Send(new GetUsersByProjectIdRequest() { ProjectId = projectId }));
        }
    }
}
