using Project.Application.Features.Comment.TodoComments.GetTodoCommentByTodoId;

namespace Project.API.Endpoint.Comment.TodoComments.GetTodoCommentByTodoId
{
    [ApiController]
    [Route(NameRouter.TODO_COMMENT_ROUTER)]
    public class GetTodoCommentByTodoIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{todoId}")]
        public async Task<IActionResult> GetTodoCommentByTodoId(int todoId)
        {
            return Ok(await mediator.Send(new GetTodoCommentByTodoIdRequest() { TodoId = todoId }));
        }
    }
}
