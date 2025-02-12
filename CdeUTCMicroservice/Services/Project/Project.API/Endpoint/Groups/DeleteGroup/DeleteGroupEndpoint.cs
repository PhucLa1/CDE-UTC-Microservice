using Project.Application.Features.Groups.DeleteGroup;

namespace Project.API.Endpoint.Groups.DeleteGroup
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class DeleteGroupEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteGroup([FromBody] DeleteGroupRequest deleteGroupRequest)
        {
            return Ok(await mediator.Send(deleteGroupRequest));
        }
    }
}
