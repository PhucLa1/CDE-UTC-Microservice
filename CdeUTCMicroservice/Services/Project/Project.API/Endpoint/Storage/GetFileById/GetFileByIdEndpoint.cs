using Project.Application.Storage.GetFileById;

namespace Project.API.Endpoint.Storage.GetFileById
{
    [ApiController]
    [Route(NameRouter.FILE_ROUTER)]
    public class GetFileByIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFileById(int id)
        {
            return Ok(await mediator.Send(new GetFileByIdRequest() { Id = id }));
        }
    }
}
