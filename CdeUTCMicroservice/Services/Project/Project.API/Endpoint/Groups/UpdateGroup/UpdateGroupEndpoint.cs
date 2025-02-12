using Project.Application.Features.Groups.UpdateGroup;

namespace Project.API.Endpoint.Groups.UpdateGroup
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class DeleteGroupEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupRequest updateGroupRequest)
        {
            return Ok(await mediator.Send(updateGroupRequest));
        }
    }
}
