using Project.Application.Features.Storage.DeleteFile;

namespace Project.API.Endpoint.Storage.DeleteFile
{
    [ApiController]
    [Route(NameRouter.FILE_ROUTER)]
    public class DeleteFileEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest deleteFileRequest)
        {
            return Ok(await mediator.Send(deleteFileRequest));
        }
    }
}
