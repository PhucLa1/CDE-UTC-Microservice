using Project.Application.Features.Storage.UpdateFile;

namespace Project.API.Endpoint.Storage.UpdateFile
{
    [ApiController]
    [Route(NameRouter.FILE_ROUTER)]
    public class UpdateFileEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateFile([FromBody] UpdateFileRequest updateFileRequest)
        {
            return Ok(await mediator.Send(updateFileRequest));
        }
    }
}
