using Project.Application.Features.Comment.FileComments.UpdateFileComment;

namespace Project.API.Endpoint.Comment.FileComments.UpdateFileComment
{
    [ApiController]
    [Route(NameRouter.FILE_COMMENT_ROUTER)]
    public class UpdateFileCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateFileComment([FromBody] UpdateFileCommentRequest updateFileCommentRequest)
        {
            return Ok(await mediator.Send(updateFileCommentRequest));
        }
    }
}
