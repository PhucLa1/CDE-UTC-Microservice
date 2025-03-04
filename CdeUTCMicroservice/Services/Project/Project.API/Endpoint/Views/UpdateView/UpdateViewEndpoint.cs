using Project.Application.Features.Views.UpdateView;

namespace Project.API.Endpoint.Views.UpdateView
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class UpdateViewEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateView([FromBody] UpdateViewRequest updateViewRequest)
        {
            return Ok(await mediator.Send(updateViewRequest));
        }
    }
}
