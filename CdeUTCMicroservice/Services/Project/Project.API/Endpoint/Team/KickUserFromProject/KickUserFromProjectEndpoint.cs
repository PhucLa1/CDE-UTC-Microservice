using Project.Application.Features.Team.KickUserFromProject;

namespace Project.API.Endpoint.Team.KickUserFromProject
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class KickUserFromProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.KICK_USER)]
        public async Task<IActionResult> KickUserFromProject([FromBody] KickUserFromProjectRequest kickUserFromProjectRequest)
        {
            return Ok(await mediator.Send(kickUserFromProjectRequest));
        }
    }
}
