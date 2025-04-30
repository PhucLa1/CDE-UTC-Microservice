using Project.Application.Features.Todos.CreateTodo;

namespace Project.API.Endpoint.Todos.CreateTodo
{
    [ApiController]
    [Route(NameRouter.TODO_ROUTER)]
    public class CreateTodoEndpoint(IMediator mediator): ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest createTodoRequest)
        {
            return Ok(await mediator.Send(createTodoRequest));
        }
    }
}
