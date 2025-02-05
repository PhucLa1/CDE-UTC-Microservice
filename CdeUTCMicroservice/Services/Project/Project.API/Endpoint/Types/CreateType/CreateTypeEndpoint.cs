using Project.Application.Features.Types.CreateType;

namespace Project.API.Endpoint.Types.CreateType
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class CreateTypeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.TYPE)]
        public async Task<IActionResult> CreateType([FromForm] CreateTypeRequest createTypeRequest)
        {
            return Ok(await mediator.Send(createTypeRequest));
        }
    }
}
