using Project.Application.Features.Types.UpdateType;

namespace Project.API.Endpoint.Types.UpdateType
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class UpdateTypeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.TYPE)]
        public async Task<IActionResult> UpdateType([FromForm] UpdateTypeRequest updateTypeRequest)
        {
            return Ok(await mediator.Send(updateTypeRequest));
        }
    }
}
