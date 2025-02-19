using Project.Application.Features.Comment.FolderComments.UpdateFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.UpdateFolderComment
{
    [ApiController]
    [Route(NameRouter.FOLDER_COMMENT_ROUTER)]
    public class UpdateFolderCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateFolderComment([FromBody] UpdateFolderCommentRequest updateFolderCommentRequest)
        {
            return Ok(await mediator.Send(updateFolderCommentRequest));
        }
    }
}
