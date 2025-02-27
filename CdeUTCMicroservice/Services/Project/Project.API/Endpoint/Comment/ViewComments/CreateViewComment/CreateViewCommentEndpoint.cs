using Project.Application.Features.Comment.ViewComments.CreateViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.CreateViewComment
{
    [ApiController]
    [Route(NameRouter.VIEW_COMMENT_ROUTER)]
    public class CreateViewCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateViewComment([FromBody] CreateViewCommentRequest createViewCommentRequest)
        {
            return Ok(await mediator.Send(createViewCommentRequest));
        }
    }
}
