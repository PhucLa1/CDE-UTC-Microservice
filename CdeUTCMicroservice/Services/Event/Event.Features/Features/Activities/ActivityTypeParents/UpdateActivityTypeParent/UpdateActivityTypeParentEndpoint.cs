namespace Event.Features.Features.Activities.ActivityTypeParents.UpdateActivityTypeParent
{
    [Route("api/activity-type-parent")]
    [ApiController]
    public class UpdateActivityTypeParentEndpoint(IMediator mediator) : ControllerBase
    {
        public async Task<IActionResult> UpdateActivityTypeParent(UpdateActivityTypeParentRequest updateActivityTypeParentRequest)
        {
            return Ok(await mediator.Send(updateActivityTypeParentRequest));
        }
    }
}
