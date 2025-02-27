using Project.Application.Features.Comment.ViewComments.DeleteViewComment;

namespace Project.API.Endpoint.Comment.ViewComments.DeleteViewComment
{
    [ApiController]
    [Route(NameRouter.VIEW_COMMENT_ROUTER)]
    public class DeleteViewCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteViewComment([FromBody] DeleteViewCommentRequest deleteViewCommentRequest)
        {
            return Ok(await mediator.Send(deleteViewCommentRequest));
        }
    }
}
