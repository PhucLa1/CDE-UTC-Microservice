using Project.Application.Features.Storage.MoveFile;

namespace Project.API.Endpoint.Storage.MoveFile
{
    [ApiController]
    [Route(NameRouter.FILE_ROUTER)]
    public class MoveFileEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.MOVE_FILE)]
        public async Task<IActionResult> MoveFile([FromBody] MoveFileRequest moveFileRequest)
        {
            return Ok(await mediator.Send(moveFileRequest));
        }
    }
}
