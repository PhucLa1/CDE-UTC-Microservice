using Project.Application.Features.Project.GetProject;

namespace Project.API.Endpoint.Project.GetProject
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetProjectEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetProject()
        {
            return Ok(await mediator.Send(new GetProjectRequest()));
        }
    }
}
