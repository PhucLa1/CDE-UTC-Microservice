using Project.Application.Features.Storage.GetFolderTree;

namespace Project.API.Endpoint.Storage.GetFolderTree
{
    [ApiController]
    [Route(NameRouter.STORAGE_ROUTER)]  
    public class GetFolderTreeEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route(NameRouter.TREE_STORAGE + "/{id}")]
        public async Task<IActionResult> GetTreeFolder(int id)
        {
            return Ok(await mediator.Send(new GetFolderTreeRequest { Id = id }));
        }
        
    }
}
