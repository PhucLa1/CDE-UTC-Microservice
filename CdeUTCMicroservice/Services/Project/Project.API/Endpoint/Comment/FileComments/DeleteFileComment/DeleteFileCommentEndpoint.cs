using Project.Application.Features.Comment.FileComments.DeleteFileComment;

namespace Project.API.Endpoint.Comment.FileComments.DeleteFileComment
{
    [ApiController]
    [Route(NameRouter.FILE_COMMENT_ROUTER)]
    public class DeleteFileCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteFileComment([FromBody] DeleteFileCommentRequest deleteFileCommentRequest)
        {
            return Ok(await mediator.Send(deleteFileCommentRequest));
        }
    }
}
