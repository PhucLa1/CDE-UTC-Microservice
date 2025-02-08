using Project.Application.Features.Priorities.DeletePriority;

namespace Project.API.Endpoint.Priorities.DeletePriority
{
    [Route(NameRouter.PROJECT_ROUTER)]
    [ApiController]
    public class DeletePriorityEndpoint(IMediator mediator) : ControllerBase
    {
        [Route(NameRouter.PRIORITY)]
        [HttpDelete]
        public async Task<IActionResult> DeletePriority([FromBody] DeletePriorityRequest deletePriorityRequest)
        {
            return Ok(await mediator.Send(deletePriorityRequest));  
        }
    }
}
