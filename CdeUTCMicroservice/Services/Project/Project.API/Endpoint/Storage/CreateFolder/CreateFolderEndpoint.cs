using Project.Application.Features.Storage.CreateFolder;

namespace Project.API.Endpoint.Storage.CreateFolder
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class CreateFolderEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFolder([FromBody] CreateFolderRequest createFolderRequest)
        {
            return Ok(await mediator.Send(createFolderRequest));
        }
    }
}
