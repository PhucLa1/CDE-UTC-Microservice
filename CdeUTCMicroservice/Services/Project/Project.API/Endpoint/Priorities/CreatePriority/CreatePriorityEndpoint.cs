using Project.Application.Features.Priorities.CreatePriority;

namespace Project.API.Endpoint.Priorities.CreatePriority
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class CreatePriorityEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.PRIORITY)]
        public async Task<IActionResult> CreatePriority([FromBody] CreatePriorityRequest createPriorityRequest)
        {
            return Ok(await mediator.Send(createPriorityRequest));
        }
    }
}
