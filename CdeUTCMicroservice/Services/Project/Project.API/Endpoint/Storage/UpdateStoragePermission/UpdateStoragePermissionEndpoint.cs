using Project.Application.Features.Storage.UpdateStoragePermission;

namespace Project.API.Endpoint.Storage.UpdateStoragePermission
{
    [ApiController]
    [Route(NameRouter.STORAGE_ROUTER)] 
    
    public class UpdateStoragePermissionEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route("update-permission")]
        public async Task<IActionResult> UpdateStoragePermission([FromBody] UpdateStoragePermissionRequest updateStoragePermissionRequest)
        {
            return Ok(await mediator.Send(updateStoragePermissionRequest));
        }
    }
}
