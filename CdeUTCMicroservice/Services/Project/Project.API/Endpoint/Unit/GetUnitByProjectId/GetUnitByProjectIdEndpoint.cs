using Project.Application.Features.Unit.GetUnitByProjectId;

namespace Project.API.Endpoint.Unit.GetUnitByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class GetUnitByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{id}/" + NameRouter.UNIT)]
        public async Task<IActionResult> GetUnitByProjectId([AsParameters] int id)
        {
            return Ok(await mediator.Send(new GetUnitByProjectIdRequest() { ProjectId = id }));
        }
    }
    
}
