using Project.Application.Features.Storage.UpdateFolder;

namespace Project.API.Endpoint.Storage.UpdateFolder
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class UpdateFolderEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateFolderRequest updateFolderRequest)
        {
            return Ok(await mediator.Send(updateFolderRequest));
        }
    }
}
