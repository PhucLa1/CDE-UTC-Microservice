

using Project.Application.Features.Views.GetViewById;

namespace Project.API.Endpoint.View.GetViewWById
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class GetViewByIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetViewById(int id)
        {
            return Ok(await mediator.Send(new GetViewByIdRequest() { Id = id }));
        }
    }
}
