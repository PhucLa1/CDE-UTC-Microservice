using Project.Application.Features.Storage.DeleteFolder;

namespace Project.API.Endpoint.Storage.DeleteFolder
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class DeleteFolderEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteFolder([FromBody] DeleteFolderRequest deleteFolderRequest)
        {
            return Ok(await mediator.Send(deleteFolderRequest));
        }
    }
}
