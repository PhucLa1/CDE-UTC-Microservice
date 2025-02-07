using Project.Application.Features.Team.GetRoleByProjectId;

namespace Project.API.Endpoint.Team.GetRoleByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetRoleByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.GET_ROLE)]
        public async Task<IActionResult> GetRoleByProjectId(int projectId)
        {
            return Ok(await mediator.Send(new GetRoleByProjectIdRequest() {ProjectId = projectId}));
        }
    }
}
