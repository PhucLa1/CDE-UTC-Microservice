using Project.Application.Features.Team.GetAllTarget;

namespace Project.API.Endpoint.Team.GetAllTarget
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class GetAllTargetEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.TARGETS)]
        public async Task<IActionResult> GetAllTarget(int projectId)
        {
            return Ok(await mediator.Send(new GetAllTargetRequest() { ProjectId = projectId }));
        }
    }
}
