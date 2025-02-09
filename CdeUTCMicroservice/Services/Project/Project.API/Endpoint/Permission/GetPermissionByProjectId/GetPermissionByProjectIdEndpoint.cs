using Project.Application.Features.Permission.GetPermissionByProjectId;

namespace Project.API.Endpoint.Permission.GetPermissionByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetPermissionByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [Route("{projectId}/" + NameRouter.PERMISSION)]
        [HttpGet]
        public async Task<IActionResult> GetPermissionByProjectId(int projectId)
        {
            return Ok(await mediator.Send(new GetPermissionByProjectIdRequest() { ProjectId = projectId }));
        }
    }
}
