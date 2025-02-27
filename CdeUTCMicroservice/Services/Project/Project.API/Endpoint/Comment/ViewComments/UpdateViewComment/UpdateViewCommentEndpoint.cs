using Project.Application.Features.Comment.ViewComments.UpdateViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.UpdateViewComment
{
    [ApiController]
    [Route(NameRouter.VIEW_COMMENT_ROUTER)]
    public class UpdateViewCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateViewComment([FromBody] UpdateViewCommentRequest updateViewCommentRequest)
        {
            return Ok(await mediator.Send(updateViewCommentRequest));
        }
    }
}
