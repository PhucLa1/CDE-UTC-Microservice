using Project.Application.Features.Comment.FolderComments.CreateFolderComment;

namespace Project.API.Endpoint.Comment.FolderComments.CreateFolderComment
{
    [ApiController]
    [Route(NameRouter.FOLDER_COMMENT_ROUTER)]
    public class CreateFolderCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFolderComment([FromBody] CreateFolderCommentRequest createFolderCommentRequest)
        {
            return Ok(await mediator.Send(createFolderCommentRequest)); 
        } 
    }
}
