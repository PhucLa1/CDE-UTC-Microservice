using Project.Application.Features.Types.DeleteType;

namespace Project.API.Endpoint.Types.DeleteType
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class DeleteTypeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route(NameRouter.TYPE)]
        public async Task<IActionResult> DeleteType([FromBody] DeleteTypeRequest deleteTypeRequest)
        {
            return Ok(await mediator.Send(deleteTypeRequest));
        }
    }
}
