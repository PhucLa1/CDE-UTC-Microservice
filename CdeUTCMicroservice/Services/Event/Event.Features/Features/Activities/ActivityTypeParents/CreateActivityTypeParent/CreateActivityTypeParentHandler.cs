

namespace Event.Features.Features.Activities.ActivityTypeParents.CreateActivityTypeParent
{
    public class CreateActivityTypeParentHandler
        (IBaseRepository<ActivityTypeParent> activityTypeParentRepository)
        : ICommandHandler<CreateActivityTypeParentRequest, CreateActivityTypeParentResponse>
    {
        public async Task<CreateActivityTypeParentResponse> Handle(CreateActivityTypeParentRequest request, CancellationToken cancellationToken)
        {
            var activityTypeParent = new ActivityTypeParent()
            {
                Name = request.Name
            };
            if(request.IconImage is not null)
                activityTypeParent.IconImageUrl = HandleFile.UPLOAD("Project", request.IconImage);
            await activityTypeParentRepository.AddAsync(activityTypeParent, cancellationToken);
            await activityTypeParentRepository.SaveChangeAsync(cancellationToken);
            return new CreateActivityTypeParentResponse() { Data = true, Message = Message.CREATE_SUCCESSFULLY };
        }
    }
}
