using Project.Application.Features.Types.GetTypes;

namespace Project.API.Endpoint.Types.GetTypes
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetTypesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.TYPE)]
        public async Task<IActionResult> GetTypes(Guid projectId)
        {
            return Ok(await mediator.Send(new GetTypesRequest() { ProjectId = projectId }));
        }
    }
}
