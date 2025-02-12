using Project.Application.Features.Team.ApproveInvite;

namespace Project.API.Endpoint.Team.ApproveInvite
{
    [ApiController]
    [Route(NameRouter.TEAM_ROUTER)]
    public class ApproveInviteEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route(NameRouter.APPROVE_INVITE)]
        public async Task<IActionResult> ApproveInvite([FromQuery] ApproveInviteRequest approveInviteRequest)
        {
            var result = await mediator.Send(approveInviteRequest);
            return Redirect(Setting.FRONTEND_HOST + "/project/" + approveInviteRequest.ProjectId + "/team");
        }
    }
}
