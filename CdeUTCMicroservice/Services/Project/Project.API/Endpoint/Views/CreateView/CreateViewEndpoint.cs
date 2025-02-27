using Project.Application.Features.Views.CreateView;

namespace Project.API.Endpoint.Views.CreateView
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class CreateViewEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateView([FromBody] CreateViewRequest createViewRequest)
        {
            return Ok(await mediator.Send(createViewRequest));
        }
    }
}
