using Project.Application.Features.Comment.TodoComments.UpdateTodoComment;

namespace Project.API.Endpoint.Comment.TodoComments.UpdateTodoComment
{
    [ApiController]
    [Route(NameRouter.TODO_COMMENT_ROUTER)]
    public class UpdateTodoCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTodoComment([FromRoute] int id, [FromBody] UpdateTodoCommentRequest updateTodoCommentRequest)
        {
            updateTodoCommentRequest.Id = id;
            return Ok(await mediator.Send(updateTodoCommentRequest));
        }
    }
} 