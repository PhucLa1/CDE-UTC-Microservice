using Project.Application.Features.Todos.GetTodos;

namespace Project.API.Endpoint.Todos.GetTodos
{
    [ApiController]
    [Route(NameRouter.TODO_ROUTER)]
    public class GetTodosEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}")]
        public async Task<IActionResult> GetTodos(int projectId)
        {
            return Ok(await mediator.Send(new GetTodosRequest() { ProjectId = projectId }));
        }
    }
}
