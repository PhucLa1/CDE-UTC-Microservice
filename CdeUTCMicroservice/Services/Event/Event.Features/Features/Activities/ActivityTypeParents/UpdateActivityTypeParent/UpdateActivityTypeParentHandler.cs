


namespace Event.Features.Features.Activities.ActivityTypeParents.UpdateActivityTypeParent
{
    public class UpdateActivityTypeParentHandler
        (IBaseRepository<ActivityTypeParent> activityTypeParentRepository)
        : ICommandHandler<UpdateActivityTypeParentRequest, UpdateActivityTypeParentResponse>
    {
        public async Task<UpdateActivityTypeParentResponse> Handle(UpdateActivityTypeParentRequest request, CancellationToken cancellationToken)
        {
            var activity = await activityTypeParentRepository.GetAllQueryAble()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

            if (activity is null)
                throw new NotFoundException(Message.NOT_FOUND);

            activity.Name = request.Name;
            activityTypeParentRepository.Update(activity);
            await activityTypeParentRepository.SaveChangeAsync(cancellationToken);
            return new UpdateActivityTypeParentResponse() { Data = true, Message = Message.UPDATE_SUCCESSFULLY };
        }
    }
}
