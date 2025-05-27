using Project.Application.Features.Views.DeleteView;

namespace Project.API.Endpoint.Views.DeleteView
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class DeleteViewEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteView(int id)
        {
            return Ok(await mediator.Send(new DeleteViewRequest() { Id = id }));
        }
    }
}
