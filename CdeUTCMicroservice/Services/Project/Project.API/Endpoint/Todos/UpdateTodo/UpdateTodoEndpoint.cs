using Project.Application.Features.Todos.UpdateTodo;

namespace Project.API.Endpoint.Todos.UpdateTodo
{
    [ApiController]
    [Route(NameRouter.TODO_ROUTER)]
    public class UpdateTodoEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoRequest updateTodoRequest)
        {
            updateTodoRequest.Id = id;
            return Ok(await mediator.Send(updateTodoRequest));
        }
    }
}
