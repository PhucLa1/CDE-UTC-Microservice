using Project.Application.Features.Groups.DeleteUserGroup;

namespace Project.API.Endpoint.Groups.DeleteUserGroup
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class DeleteUserGroupEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.USERS)]
        public async Task<IActionResult> DeleteUserGroup([FromBody] DeleteUserGroupRequest deleteUserGroupRequest)
        {
            return Ok(await mediator.Send(deleteUserGroupRequest));
        }
    }
}
