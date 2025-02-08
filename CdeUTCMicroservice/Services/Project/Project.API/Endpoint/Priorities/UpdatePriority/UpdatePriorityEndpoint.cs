using Project.Application.Features.Priorities.UpdatePriority;

namespace Project.API.Endpoint.Priorities.UpdatePriority
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdatePriorityEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.PRIORITY)]
        public async Task<IActionResult> UpdatePriority([FromBody] UpdatePriorityRequest updatePriorityRequest)
        {
            return Ok(await mediator.Send(updatePriorityRequest));  
        }
    }
}
