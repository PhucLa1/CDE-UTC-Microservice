using Project.Application.Features.Storage.GetAllStorages;

namespace Project.API.Endpoint.Storage.GetAllStorages
{
    [ApiController]
    [Route(NameRouter.STORAGE_ROUTER)]
    public class GetAllStoragesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{projectId}/{parentId}")]
        public async Task<IActionResult> GetAllStorages(int projectId, int parentId)
        {
            return Ok(await mediator.Send(new GetAllStoragesRequest() { ParentId = parentId, ProjectId = projectId }));
        }
    }
}
