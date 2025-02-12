using Project.Application.Features.Groups.CreateGroup;

namespace Project.API.Endpoint.Groups.CreateGroup
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class CreateGroupEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest createGroupRequest)
        {
            return Ok(await mediator.Send(createGroupRequest)); 
        }
    }
}
