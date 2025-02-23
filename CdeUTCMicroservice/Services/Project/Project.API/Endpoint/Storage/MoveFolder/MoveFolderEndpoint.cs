using Project.Application.Features.Storage.MoveFolder;

namespace Project.API.Endpoint.Storage.MoveFolder
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class MoveFolderEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.MOVE_FOLDER)]
        public async Task<IActionResult> MoveFolder([FromBody] MoveFolderRequest moveFolderRequest)
        {
            return Ok(await mediator.Send(moveFolderRequest));
        }
    }
}
