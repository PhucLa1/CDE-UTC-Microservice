using Project.Application.Features.Permission.ChangePermission;

namespace Project.API.Endpoint.Permission.ChangePermission
{
    [ApiController]
    [Route(NameRouter.PROJECT_ROUTER)]
    public class ChangePermissionEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        [Route(NameRouter.PERMISSION)]
        public async Task<IActionResult> ChangePermission([FromBody] ChangePermissionRequest changePermissionRequest)
        {
            return Ok(await mediator.Send(changePermissionRequest));
        }
    }
}
