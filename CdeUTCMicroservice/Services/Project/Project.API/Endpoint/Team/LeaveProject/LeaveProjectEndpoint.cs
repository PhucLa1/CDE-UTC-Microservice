using Project.Application.Features.Team.LeaveProject;

namespace Project.API.Endpoint.Team.LeaveProject
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class LeaveProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.LEAVE_PROJECT + "/{projectId}")]
        public async Task<IActionResult> LeaveProject(int projectId)
        {
            return Ok(await mediator.Send(new LeaveProjectRequest(){ ProjectId = projectId}));
        }
    }
}
