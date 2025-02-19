using Project.Application.Features.Comment.FolderComments.DeleteFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.DeleteFolderComment
{
    [ApiController]
    [Route(NameRouter.FOLDER_COMMENT_ROUTER)]
    public class DeleteFolderCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteFolderComment([FromBody] DeleteFolderCommentRequest deleteFolderCommentRequest)
        {
            return Ok(await mediator.Send(deleteFolderCommentRequest));
        }
    }
}
