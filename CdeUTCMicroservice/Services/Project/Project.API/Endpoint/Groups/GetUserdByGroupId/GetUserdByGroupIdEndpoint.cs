using Project.Application.Features.Groups.GetUserdByGroupId;

namespace Project.API.Endpoint.Groups.GetUserdByGroupId
{
    [ApiController]
    [Route(NameRouter.GROUP_ROUTER)]
    public class GetUserdByGroupId(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{groupId}/" + NameRouter.USERS)]
        public async Task<IActionResult> GetGroupsByProjecId(int groupId)
        {
            return Ok(await mediator.Send(new GetUserdByGroupIdRequest() { GroupId = groupId }));
        }
    }
}
