
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

            await activityRepository.AddAsync(new Activity()
            {
                Action = message.Action,
                ResourceId = message.ResourceId,
                Content = message.Content,
                TypeActivity = message.TypeActivity,
                ProjectId = message.ProjectId
            }, CancellationToken.None);
            await activityRepository.SaveChangeAsync(CancellationToken.None);
        }
    }
}
