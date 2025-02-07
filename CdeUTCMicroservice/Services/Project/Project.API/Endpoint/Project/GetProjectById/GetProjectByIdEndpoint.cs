using Project.Application.Features.Project.GetProjectById;

namespace Project.API.Endpoint.Project.GetProjectById
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetProjectByIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return Ok(await mediator.Send(new GetProjectByIdRequest() { Id = id }));
        }
    }
}
