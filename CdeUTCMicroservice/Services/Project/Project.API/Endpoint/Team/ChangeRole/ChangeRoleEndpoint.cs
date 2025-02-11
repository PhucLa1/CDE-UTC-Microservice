using Project.Application.Features.Team.ChangeRole;

namespace Project.API.Endpoint.Team.ChangeRole
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class ChangeRoleEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.CHANGE_ROLE)]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleRequest changeRoleRequest)
        {
            return Ok(await mediator.Send(changeRoleRequest));
        }
    }
}
