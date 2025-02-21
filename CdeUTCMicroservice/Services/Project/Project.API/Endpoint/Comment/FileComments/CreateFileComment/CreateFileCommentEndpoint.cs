using Project.Application.Features.Comment.FileComments.CreateFileComment;

namespace Project.API.Endpoint.Comment.FileComments.CreateFileComment
{
    [ApiController]
    [Route(NameRouter.FILE_COMMENT_ROUTER)]
    public class CreateFileCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFileComment([FromBody] CreateFileCommentRequest createFileCommentRequest)
        {
            return Ok(await mediator.Send(createFileCommentRequest));
        }
    }
}
