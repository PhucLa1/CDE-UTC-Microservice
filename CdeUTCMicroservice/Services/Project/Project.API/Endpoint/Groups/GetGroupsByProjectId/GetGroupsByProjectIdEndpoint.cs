using Project.Application.Features.Groups.GetGroupsByProjectId;

namespace Project.API.Endpoint.Groups.GetGroupsByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetGroupsByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.GROUP_ROUTER)]
        public async Task<IActionResult> GetGroupsByProjecId(int projectId)
        {
            return Ok(await mediator.Send(new GetGroupsByProjectIdRequest() {ProjectId = projectId }));
        }
    }
}
