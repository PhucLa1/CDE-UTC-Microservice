using Project.Application.Features.Storage.GetFullPath;

namespace Project.API.Endpoint.Storage.GetFullPath
{
    [ApiController]
    [Route(NameRouter.STORAGE_ROUTER)]
    public class GetFullPathEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{folderId}/" + NameRouter.FULL_PATH)]
        public async Task<IActionResult> GetFullPath(int folderId)
        {
            return Ok(await mediator.Send(new GetFullPathRequest() { FolderId = folderId }));
        }
    }
}
