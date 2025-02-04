using Project.Application.Features.Tags.DeleteTag;

namespace Project.API.Endpoint.Tags.DeleteTag
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class DeleteTagEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.TAG)]
        public async Task<IActionResult> DeleteTag([FromBody] DeleteTagRequest deleteTagRequest)
        {
            return Ok(await mediator.Send(deleteTagRequest));
        }
    }
}
