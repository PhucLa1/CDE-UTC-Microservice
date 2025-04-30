using Project.Application.Features.Views.AddAnnotation;

namespace Project.API.Endpoint.Views.AddAnnotation
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class AddAnnotationEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route(NameRouter.ADD_ANNOTATION)]
        public async Task<IActionResult> AddAnnotaion([FromBody] AddAnnotationRequest addAnnotationRequest)
        {
            return Ok(await mediator.Send(addAnnotationRequest));
        }
    }
}
