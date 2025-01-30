using Project.Application.Features.Unit.UpdateUnitByProjectId;

namespace Project.API.Endpoint.Unit.UpdateUnitByProjectId
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdateUnitByProjectIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.UNIT)]
        public async Task<IActionResult> UpdateUnitByProjectId([FromBody] UpdateUnitByProjectIdRequest updateUnitByProjectIdRequest)
        {
            return Ok(await mediator.Send(updateUnitByProjectIdRequest));
        }
    }
}
