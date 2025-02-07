using Project.Application.Features.Project.DeleteProject;

namespace Project.API.Endpoint.Project.DeleteProject
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class DeleteProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return Ok(await mediator.Send(new DeleteProjectRequest { Id = id }));
        }
    }
}
