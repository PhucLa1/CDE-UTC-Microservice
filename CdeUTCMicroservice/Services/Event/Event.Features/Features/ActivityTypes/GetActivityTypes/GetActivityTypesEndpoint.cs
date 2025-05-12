namespace Event.Features.Features.ActivityTypes.GetActivityTypes
{
    [ApiController]
    [Route(NameRouter.ACTIVITY_TYPE_ROUTER)]
    public class GetActivityTypesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] GetActivityTypesRequest getActivityTypesRequest)
        {
            return Ok(await mediator.Send(getActivityTypesRequest));
        }
    }
}
