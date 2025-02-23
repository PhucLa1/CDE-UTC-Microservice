using Project.Application.Features.Storage.GetAllFolderDestination;

namespace Project.API.Endpoint.Storage.GetAllFolderDestination
{
    [ApiController]
    [Route(NameRouter.FOLDER_ROUTER)]
    public class GetAllFolderDestinationEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.FOLDER_DESTINATION)]
        public async Task<IActionResult> GetAllFolderDestination([FromBody] GetAllFolderDestinationRequest getAllFolderDestinationRequest)
        {
            return Ok(await mediator.Send(getAllFolderDestinationRequest));   
        }
    }
}
