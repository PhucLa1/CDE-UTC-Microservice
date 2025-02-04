using Project.Application.Features.Tags.CreateTag;

namespace Project.API.Endpoint.Tags.CreateTag
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class CreateTagEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.TAG)]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagRequest createTagRequest)
        {
            return Ok(await mediator.Send(createTagRequest));
        }
    }
}
