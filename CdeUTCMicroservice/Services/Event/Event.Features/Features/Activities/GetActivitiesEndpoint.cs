using BuildingBlocks.Enums;

namespace Event.Features.Features.Activities
{
    [ApiController]
    [Route(NameRouter.ACTIVITY_ROUTER)]
    public class GetActivitiesEndpoint(IMediator mediator, IBaseRepository<ActivityType> activityTypeRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] GetActivitiesRequest getActivitiesRequest)
        {
            return Ok(await mediator.Send(getActivitiesRequest));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivities([FromBody] int ProjectId)
        {
            var activityType = new List<ActivityType>();
            foreach (var typeActivity in Enum.GetValues(typeof(TypeActivity)))
            {
                activityType.Add(new ActivityType()
                {
                    ProjectId = ProjectId,
                    TypeActivity = (TypeActivity)typeActivity,
                    TimeSend = TimeSpan.FromHours(17), // ⏰ 05:00 sáng
                    IsAcceptAll = true,
                });
            }

            await activityTypeRepository.AddRangeAsync(activityType, CancellationToken.None);
            await activityTypeRepository.SaveChangeAsync(CancellationToken.None);
            return Ok(activityType);
        }
    }
}
