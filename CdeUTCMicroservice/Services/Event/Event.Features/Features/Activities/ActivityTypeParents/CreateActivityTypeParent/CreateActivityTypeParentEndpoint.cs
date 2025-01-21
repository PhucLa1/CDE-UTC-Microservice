


namespace Event.Features.Features.Activities.ActivityTypeParents.CreateActivityTypeParent
{
    [ApiController]
    [Route("api/activity-type-parent")]
    public class ActivityTypeParentEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateActivityTypeParent([FromBody] CreateActivityTypeParentRequest createActivityTypeParentRequest)
        {
            return Ok(await mediator.Send(createActivityTypeParentRequest));
        }
    }
}
