
using Project.Application.Features.Project.CreateProject;

namespace Project.API.Endpoint.Project.CreateProject
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class CreateProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateProject([FromForm] CreateProjectRequest createProjectRequest)
        {
            return Ok(await mediator.Send(createProjectRequest));
        }
    }
}
