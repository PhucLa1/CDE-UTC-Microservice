using Project.Application.Features.Todos.DeleteTodo;

namespace Project.API.Endpoint.Todos.DeleteTodo
{
    [ApiController]
    [Route(NameRouter.TODO_ROUTER)]
    public class DeleteTodoEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            return Ok(await mediator.Send(new DeleteTodoRequest { Id = id }));
        }
    }
}
