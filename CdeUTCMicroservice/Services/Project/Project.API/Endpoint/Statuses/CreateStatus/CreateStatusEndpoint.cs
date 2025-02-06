using Project.Application.Features.Statuses.CreateStatus;

namespace Project.API.Endpoint.Statuses.CreateStatus
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class CreateStatusEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.STATUS)]
        public async Task<IActionResult> CreateStatus([FromBody] CreateStatusRequest createStatusRequest)
        {
            return Ok(await mediator.Send(createStatusRequest));
        }
    }
}
