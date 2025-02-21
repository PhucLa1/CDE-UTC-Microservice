using Project.Application.Features.Storage.CreateFile;

namespace Project.API.Endpoint.Storage.CreateFile
{
    [ApiController]
    [Route(NameRouter.FILE_ROUTER)]
    public class CreateFileEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileRequest createFileRequest)
        {
            return Ok(await mediator.Send(createFileRequest));
        }
    }
}
