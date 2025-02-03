using Project.Application.Features.Project.UpdateProject;

namespace Project.API.Endpoint.Project.UpdateProject
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdateProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateProject(UpdateProjectRequest updateProjectRequest)
        {
            return Ok(await mediator.Send(updateProjectRequest));
        }
    }
}
