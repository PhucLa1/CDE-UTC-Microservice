using Project.Application.Features.Comment.TodoComments.CreateTodoComment;

namespace Project.API.Endpoint.Comment.TodoComments.CreateTodoComment
{
    [ApiController]
    [Route(NameRouter.TODO_COMMENT_ROUTER)]
    public class CreateTodoCommentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTodoComment([FromBody] CreateTodoCommentRequest createTodoCommentRequest)
        {
            return Ok(await mediator.Send(createTodoCommentRequest));
        }
    }
} 