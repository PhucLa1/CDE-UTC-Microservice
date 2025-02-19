using Project.Application.Features.Storage.GetFolderById;

namespace Project.API.Endpoint.Storage.GetFolderById
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class GetFolderByIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetFolderByIdGetFolderById(int id)
        {
            return Ok(await mediator.Send(new GetFolderByIdRequest() { Id = id }));
        }
    }
}
