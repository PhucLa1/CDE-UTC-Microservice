
using BuildingBlocks.Enums;

namespace Event.Features.MessageHandlers
{
    public class CreateProjectSubcribe
        (IBaseRepository<ActivityType> activityTypeRepository)
        : IConsumer<CreateProjectEvent>
    {
        public async Task Consume(ConsumeContext<CreateProjectEvent> context)
        {
            Console.WriteLine(context);
            var activityType = new List<ActivityType>();
            foreach(var typeActivity in Enum.GetValues(typeof(TypeActivity)))
            {
                activityType.Add(new ActivityType()
                {
                    ProjectId = context.Message.ProjectId,
                    TypeActivity = (TypeActivity)typeActivity,
                    TimeSend = TimeSpan.FromHours(17), // ⏰ 05:00 sáng
                    IsAcceptAll = true,
                });
            }

            await activityTypeRepository.AddRangeAsync(activityType, CancellationToken.None);
            await activityTypeRepository.SaveChangeAsync(CancellationToken.None);
        }
    }
}
