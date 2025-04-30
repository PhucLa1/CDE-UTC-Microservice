using Project.Application.Features.Views.GetAnnotationByViewId;

namespace Project.API.Endpoint.Views.GetAnnotationByViewId
{
    [ApiController]
    [Route(NameRouter.VIEW)]
    public class GetAnnotationByViewIdEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{viewId}/"+NameRouter.GET_ANNOTATIONS)]
        public async Task<IActionResult> GetAnnotationByViewId(int viewId)
        {
            return Ok(await mediator.Send(new GetAnnotationByViewIdRequest() { ViewId = viewId }));
        }
    }
}
