namespace Event.Features.Features.ActivityTypes.UpdateActivityTypes
{
    [ApiController]
    [Route(NameRouter.ACTIVITY_TYPE_ROUTER)]
    public class UpdateActivityTypesEndpoint(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> UpdateActivityType([FromBody] UpdateActivityTypesRequest updateActivityTypesRequest)
        {
            return Ok(await mediator.Send(updateActivityTypesRequest));
        }
    }
}
