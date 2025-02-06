using Project.Application.Features.Statuses.UpdateStatus;

namespace Project.API.Endpoint.Statuses.UpdateStatus
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdateStatusEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.STATUS)]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest updateStatusRequest)
        {
            return Ok(await mediator.Send(updateStatusRequest));    
        }
    }
}
