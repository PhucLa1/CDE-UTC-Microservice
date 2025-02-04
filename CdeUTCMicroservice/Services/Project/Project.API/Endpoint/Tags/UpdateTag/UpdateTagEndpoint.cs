using Project.Application.Features.Tags.UpdateTag;

namespace Project.API.Endpoint.Tags.UpdateTag
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdateTagEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.TAG)]
        public async Task<IActionResult> UpdateTag([FromBody] UpdateTagRequest updateTagRequest)
        {
            return Ok(await mediator.Send(updateTagRequest));
        }
    }
}
