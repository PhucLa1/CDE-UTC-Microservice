using Project.Application.Features.Types.ResetType;

namespace Project.API.Endpoint.Types.ResetType
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class ResetTypeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("{projectId}/" + NameRouter.TYPE)]
        public async Task<IActionResult> ResetType(Guid projectId)
        {
            return Ok(await mediator.Send(new ResetTypeRequest() { ProjectId = projectId }));
        }
    }
}
