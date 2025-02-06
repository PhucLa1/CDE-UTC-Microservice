using Project.Application.Features.Statuses.DeleteStatus;

namespace Project.API.Endpoint.Statuses.DeleteStatus
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class DeleteStatusEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.STATUS)]
        public async Task<IActionResult> DeleteStatus([FromBody] DeleteStatusRequest deleteStatusRequest)
        {
            return Ok(await mediator.Send(deleteStatusRequest));
        }
    }
}
