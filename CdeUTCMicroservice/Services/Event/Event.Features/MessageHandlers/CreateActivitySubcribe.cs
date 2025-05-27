
namespace Event.Features.MessageHandlers
{
    public class CreateActivitySubcribe
        (IBaseRepository<Activity> activityRepository)
        : IConsumer<CreateActivityEvent>
    {
        public async Task Consume(ConsumeContext<CreateActivityEvent> context)
        {
            var message = context.Message;
            Console.WriteLine(message);
            var userId = context.Headers.Get<int>("UserId");

            await activityRepository.AddAsync(new Activity()
            {
                Action = message.Action,
                ResourceId = message.ResourceId,
                Content = message.Content,
                TypeActivity = message.TypeActivity,
                ProjectId = message.ProjectId,
                CreatedBy = (int)userId.Value,
                UpdatedBy = (int)userId,
            }, CancellationToken.None);
            await activityRepository.SaveChangeAsync(CancellationToken.None);
        }
    }
}
