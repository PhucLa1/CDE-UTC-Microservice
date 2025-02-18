using Project.Application.Features.Groups.AddUserGroup;

namespace Project.API.Endpoint.Groups.AddUserGroup
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class AddUserGroupEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.USERS)]
        public async Task<IActionResult> AddUserGroup([FromBody] AddUserGroupRequest addUserGroupRequest)
        {
            return Ok(await mediator.Send(addUserGroupRequest));
        }
    }
}
