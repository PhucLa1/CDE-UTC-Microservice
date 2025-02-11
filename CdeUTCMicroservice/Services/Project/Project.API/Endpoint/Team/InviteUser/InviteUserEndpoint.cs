using Project.Application.Features.Team.InviteUser;

namespace Project.API.Endpoint.Team.InviteUser
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class InviteUserEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.INVITE_USER)]
        public async Task<IActionResult> InviteUser([FromBody] InviteUserRequest inviteUserRequest)
        {
            return Ok(await mediator.Send(inviteUserRequest));
        }
    }
}
