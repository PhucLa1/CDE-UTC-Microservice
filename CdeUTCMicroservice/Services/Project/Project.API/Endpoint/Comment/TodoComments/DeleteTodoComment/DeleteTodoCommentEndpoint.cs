using Project.Application.Features.Comment.TodoComments.DeleteTodoComment;

namespace Project.API.Endpoint.Comment.TodoComments.DeleteTodoComment
{
    [ApiController]
    [Route(NameRouter.TODO_COMMENT_ROUTER)]
    public class DeleteTodoCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodoComment([FromRoute] int id)
        {
            var request = new DeleteTodoCommentRequest { Id = id };
            return Ok(await mediator.Send(request));
        }
    }
} 