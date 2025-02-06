using Project.Application.Features.Statuses.ResetStatus;

namespace Project.API.Endpoint.Statuses.ResetStatus
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class ResetStatusEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("{projectId}/" + NameRouter.STATUS)]
        public async Task<IActionResult> ResetStatus(Guid projectId)
        {
            return Ok(await mediator.Send(new ResetStatusRequest() { ProjectId = projectId }));
        }
    }
}
