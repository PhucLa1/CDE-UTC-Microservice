using Project.Application.Features.Tags.GetTags;

namespace Project.API.Endpoint.Tags.GetTags
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetTagsEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.TAG)]
        public async Task<IActionResult> GetTags(int projectId)
        {
            return Ok(await mediator.Send(new GetTagsRequest() { ProjectId = projectId }));
        }
    }
}
