using Project.Application.Features.Views.GetAllViews;

namespace Project.API.Endpoint.Views.GetAllViews
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetAllViewsEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/" + NameRouter.VIEW)]
        public async Task<IActionResult> GetAllViews(int projectId)
        {
            return Ok(await mediator.Send(new GetAllViewsRequest() { ProjectId = projectId }));
        }
    }
}
