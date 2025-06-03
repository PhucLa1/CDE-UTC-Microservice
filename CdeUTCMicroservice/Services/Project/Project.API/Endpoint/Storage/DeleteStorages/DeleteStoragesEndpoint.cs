using Project.Application.Features.Storage.DeleteStorages;

namespace Project.API.Endpoint.Storage.DeleteStorages
{
    [ApiController]
    [Route(NameRouter.STORAGE_ROUTER)]
    public class DeleteStoragesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpDelete]
        [Route("delete-srorages")]
        public async Task<IActionResult> DeleteStorages([FromBody] DeleteStoragesRequest deleteStoragesRequest)
        {
            return Ok(await mediator.Send(deleteStoragesRequest));
        }
    }  
} 
/*
 lananh xinh gai*/
